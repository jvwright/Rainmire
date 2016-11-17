using UnityEngine;
using System.Collections;
using Pathfinding;

public class MobControl : MonoBehaviour
{
    private GameObject Player;
    private Seeker seeker;
    public Path path;                           //The current path being followed
    float nextWaypointDistance = .2f;      //The max distance from the AI to a waypoint for it to continue to the next waypoint
    private int currentWaypoint = 0;            //The waypoint we are currently moving towards
    public PlayerHealth PH;   //Player health
    float attackRange = 1.5f;    //Mobs will attempt to attack the player within this range
    public float strength = 1;           //damage done by an attack, will probably want to change this for different enemies
	public float attackTimer = 5;        //For changing how often an enemy can attack

    //not used currently
    float aggroRange = 5;     //Distance when the unit will start tracking the player  
    float deaggroRange = 10;  //Distance when the unit will stop tracking the player

    bool aggro = false;       //If the unit is tracking the player
    double minDist = 1.25;    //If the player is less than minDist away the unit will stop moving closer
    float baseSpeed = 2;      //The unit's base speed
    float speed = 2;          //Value that changes when buffed or debuffed
    float health = 10;        //The unit's current health
    float dmgCD = 0.5f;
    float dmgTimer = 0;


    // Use this for initialization
    void Start()
    {
		Player = GameObject.FindWithTag ("Player");
        seeker = GetComponent<Seeker>();
        //Start a new path to the targetPosition, return the result to the OnPathComplete function
        seeker.StartPath(transform.position, Player.transform.position, OnPathComplete);
        PH = GameObject.FindObjectOfType(typeof(PlayerHealth)) as PlayerHealth;
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("did the path have an error?" + p.error);
        if (!p.error)
        {
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }
        //Increase attackTimer to make enemies attack less often
        if (attackTimer > 0)
        {
            attackTimer -= 1;
        }
        if ((Vector2.Distance(transform.position, Player.transform.position) < aggroRange) && attackTimer == 0)
        {
            attackTimer = 5;
            aggro = true;
        }
        if (Vector2.Distance(transform.position, Player.transform.position) < attackRange)
        {
            Attack();
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            //Debug.Log("End Of Path Reached");
            return;
        }

        //Direction to the next waypoint
        Vector2 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;
		Rigidbody2D rg = this.gameObject.GetComponent<Rigidbody2D>();
		rg.MovePosition(rg.position + dir);

        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }

    }

    //how enemies inflict damage on the player
    public void Attack()
    {
		if (Player.GetComponent("PlayerHealth") != null && Player.active == true)
        {
            PH.Strike(strength);
        }
    }

    public void damage(int dmg)
    {
        if (dmgTimer > 0)
        {
            dmgTimer -= dmgCD;
        }
        else
        {
            health = health - dmg;
            Debug.Log(health);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            dmgTimer = dmgCD;
        }

    }
}

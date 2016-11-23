using UnityEngine;
using System.Collections;
using Pathfinding;
using System.Collections.Generic;

public class MobControl : MonoBehaviour
{

    // Pathfinding variables
    public GameObject Player;
    private Seeker seeker;
    public Path path;                                       //The current path being followed
    private Vector3 target;                                 //The list of points making up the unit's current path
    float nextWaypointDistance = .15f;                       //The max distance from the AI to a waypoint for it to continue to the next waypoint
    private int currentWaypoint = 0;                        //The waypoint we are currently moving towards
    public List<Vector3> patrol = new List<Vector3>();       //list of points that make up the units area to patrol
    private int patrolPoint = 0;                            //The point on the list that the unit it moving towards
    int repathDelay = 90;                                   //Minimum delay between repathing

    // General movement
    Rigidbody2D rg;                                         //The rigid body attached to the unit
    bool aggro = false;                                     //If the unit is tracking the player
    public float aggroRange;                            //Distance when the unit will start tracking the player  
    public float deaggroRange;                         //Distance when the unit will stop tracking the player
    public float baseSpeed;                             //The unit's base speed
    float speed = 2;                                        //Value that changes when buffed or debuffed
    public float health;                               //The unit's current health
    double minDist = 1.25;                                  //If the player is less than minDist away the unit will stop moving closer

    // Combat variables
    public PlayerHealth PH;                                 //Player health
    public float attackRange;                        //Mobs will attempt to attack the player within this range
    public float strength;                              //Damage done by an attack, will probably want to change this for different enemies
	public float attackTimer;                           //For changing how often an enemy can attack
    float dmgCD = 0.5f;                                     //The amount of time  the unit is invincible after being hit
    float dmgTimer = 0;                                     //the timer keeping track of invincibility




    // Use this for initialization
    void Start()
    {
        target = FindTarget();
        seeker = GetComponent<Seeker>();
        //Start a new path to the targetPosition, return the result to the OnPathComplete function
        seeker.StartPath(transform.position, target, OnPathComplete);
        PH = GameObject.FindObjectOfType(typeof(PlayerHealth)) as PlayerHealth;
        rg = this.gameObject.GetComponent<Rigidbody2D>();
        //attackTimer = 5;
        //strength = 10;
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
        repathDelay--;
        if (path == null)
        {
            return;
        }
        //Increase attackTimer to make enemies attack less often
        if (attackTimer > 0)
        {
            attackTimer -= 1;
        }

        /*
        if ((Vector2.Distance(transform.position, Player.transform.position) < aggroRange) && attackTimer == 0)
        {
            attackTimer = 5;
            aggro = true;
        }*/
        if(Vector2.Distance(transform.position, Player.transform.position) < aggroRange&& !aggro)
        {
            aggro = true;
        }
        if(Vector2.Distance(transform.position, Player.transform.position) > deaggroRange && aggro)
        {
            aggro = false;
            Repath();
        }

        if (Vector2.Distance(transform.position, Player.transform.position) < attackRange)
        {
            Attack();
            attackTimer = 5;
        }
        if (aggro && Vector2.Distance(target, Player.transform.position) > 3)
        {
            Repath();
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            //Debug.Log("End Of Path Reached");
            Repath();
        }

        //Direction to the next waypoint
        Vector2 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;
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

    public Vector3 FindTarget()
    {
        if (aggro)
        {
            return Player.transform.position;
        }
        else
        {
            Vector3 temp = patrol[patrolPoint];
            if (patrolPoint == patrol.Count-1)
            {
                patrolPoint = 0;
            }
            else
            {
                patrolPoint++;
            }
            return temp;
        }
    }

    public void Repath()
    {
        if (repathDelay <= 0)
        {
            target = FindTarget();
            seeker.StartPath(transform.position, target, OnPathComplete);
            repathDelay = 90;
        }
    }
}

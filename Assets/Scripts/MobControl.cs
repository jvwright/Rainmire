using UnityEngine;
using System.Collections;
using Pathfinding;

public class MobControl : MonoBehaviour
{
    public GameObject Player;
    private Seeker seeker;
    //The current path being followed
    public Path path;
    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 2;
    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;
/* not currently used
    float aggroRange = 5;     //Distance when the unit will start tracking the player  
    float deaggroRange = 10;  //Distance when the unit will stop tracking the player
    bool aggro = false;       //If the unit is tracking the player
    double minDist = 1.25;    //If the player is less than minDist away the unit will stop moving closer
    */
    float baseSpeed = 2;      //The unit's base speed
    float speed = 2;          //Value that changes when buffed or debuffed
    float health = 10;        //The unit's current health
    float dmgCD = 0.5f;
    float dmgTimer = 0;


    // Use this for initialization
    void Start()
    {
        seeker = GetComponent<Seeker>();

        //Start a new path to the targetPosition, return the result to the OnPathComplete function
        seeker.StartPath(transform.position, Player.transform.position, OnPathComplete);
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
        if (currentWaypoint >= path.vectorPath.Count)
        {
            Debug.Log("End Of Path Reached");
            return;
        }

        //Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;
        this.gameObject.transform.Translate(dir);

        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }


    /* Old player tracking
    if (Vector2.Distance(transform.position, Player.transform.position) < aggroRange)
    {
        aggro = true;
    }
    if (Vector2.Distance(transform.position, Player.transform.position) > deaggroRange)
    {
        aggro = false;
    }
    if (aggro)
    {
        // determine's if the mob is close enough to stop moving along the x-axis
        if (Vector2.Distance(transform.position, Player.transform.position) > minDist)
        {
            if (transform.position.x > Player.transform.position.x)
            {
                transform.Translate(Vector2.left * Time.deltaTime * speed);
            }
            if (transform.position.x < Player.transform.position.x)
            {
                transform.Translate(Vector2.right * Time.deltaTime * speed);
            }

            //determines if the mob is close enough to stop moving along the y-axis
            if (transform.position.y < Player.transform.position.y)
            {
                transform.Translate(Vector2.up * Time.deltaTime * speed);
            }
            if (transform.position.y > Player.transform.position.y)
            {
                transform.Translate(Vector2.down * Time.deltaTime * speed);
            }
        }
    }*/
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

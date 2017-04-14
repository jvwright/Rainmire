using UnityEngine;
using System.Collections;
using Pathfinding;
using System.Collections.Generic;
using System;

public class RangedMobControl : MonoBehaviour
{

    // Pathfinding variables
    public GameObject Player;                               //The player objest
    private Seeker seeker;                                  //Used to find the best path
    public Path path;                                       //The current path being followed
    private Animator anim;									// Animator for the enemy
    private Vector3 target;                                 //The list of points making up the unit's current path
    float nextWaypointDistance = .15f;                      //The max distance from the AI to a waypoint for it to continue to the next waypoint
    private int currentWaypoint = 0;                        //The waypoint we are currently moving towards
    public List<Vector3> patrol = new List<Vector3>();      //list of points that make up the units area to patrol
    private int patrolPoint = 0;                            //The point on the list that the unit it moving towards
    int repathDelay = 90;                                   //Minimum delay between repathing

    // General movement
    Rigidbody2D rg;                                         //The rigid body attached to the unit
    bool aggro = false;                                     //If the unit is tracking the player
    public float aggroRange;                                //Distance when the unit will start tracking the player  
    public float deaggroRange;                              //Distance when the unit will stop tracking the player
    public float baseSpeed;                                 //The unit's base speed
    float speed;                                            //Value that changes when buffed or debuffed
    public float health;                                    //The unit's current health
    public float minDist;                                   //If the player is less than minDist away the unit will stop moving closer
    Vector2 dir;                                            // Movement variable

    // Combat variables
    PlayerHealth PH;                                        //Player health
    public float attackRange;                               //Mobs will attempt to attack the player within this range
    public float strength;                                  //Damage done by an attack, will probably want to change this for different enemies
    public float attackTimer;                               //For changing how often an enemy can attack
    float attacktime;
    float dmgCD = 0.5f;                                     //The amount of time  the unit is invincible after being hit
    float dmgTimer = 15;                                     //the timer keeping track of invincibility

    //Other variables
    string unitName;                                        //Just for debugging with multiple units
    public bool animationsOff;                              //turns off animations

    //Sound
    GameObject soundplayer;
    SoundManager SM;
    public AudioClip playOnHurt;
    public AudioClip playOnDeath;
    public AudioClip playOnAttack;
    public AudioClip bossDeath;
    SpriteRenderer sr;
    bool isBoss;
    public Sprite bossSprite;

    // Use this for initialization
    void Start()
    {
        target = FindTarget();
        seeker = GetComponent<Seeker>();
        //Start a new path to the targetPosition, return the result to the OnPathComplete function
        seeker.StartPath(transform.position, target, OnPathComplete);
        PH = GameObject.FindObjectOfType(typeof(PlayerHealth)) as PlayerHealth;
        rg = this.gameObject.GetComponent<Rigidbody2D>();
        speed = baseSpeed;
        unitName = this.name;
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        if (bossSprite != null &&  sr.sprite == bossSprite)
        {
            isBoss = true;
        }
        else
        {
            isBoss = false;
        }
        
        soundplayer = GameObject.FindGameObjectWithTag("enemysounds");
        SM = soundplayer.GetComponent<SoundManager>();
    }

    public void OnPathComplete(Path p)
    {
        //Debug.Log("did the path have an error?" + p.error);
        if (!p.error)
        {
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 0;
        }
        else 
        {
            Debug.Log(unitName + "there was an error with the path" + p.error); 
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
        
        /*//Increase attackTimer to make enemies attack less often
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

        //If the player is close enough set aggro to true
        if(Vector2.Distance(transform.position, Player.transform.position) < aggroRange&& !aggro)
        {
            aggro = true;
            Debug.Log(unitName + "Now tracking the player");
            Repath();
        }

        //If the player is too far away set aggro to false and return to the patrol
        if(Vector2.Distance(transform.position, Player.transform.position) > deaggroRange && aggro)
        {
            aggro = false;
            Debug.Log(unitName + "No longer tracking the player");
            Repath();
        }

        //If the player is close enough attack
        if (Vector2.Distance(transform.position, Player.transform.position) < attackRange)
        {
            if (attackTimer == 0)
            {
				animate_attack();
                Attack();
                attackTimer = 50;
            }
            else
            {
                attackTimer--;
            }
        }


        //If the unit is tracking the player and the player moves far enough away from the current target, repath
        if (aggro && Vector2.Distance(target, Player.transform.position) > 5)
        {
            Repath();
        }

        //If at the end of the current path, repath
        if (currentWaypoint >= path.vectorPath.Count)
        {
            //Debug.Log(name + "End Of Path Reached");
            Repath();
            return;
        }

        //Stop moving if too close to the player
        //Debug.Log(unitname + " dist" + Vector2.Distance(transform.position, Player.transform.position));
        if (Vector2.Distance(transform.position, Player.transform.position) > minDist)
        {
            //Debug.Log(unitname+" current waypoint "+currentWaypoint);
            //Move to the next waypoint
            dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            dir *= speed * Time.fixedDeltaTime;
            rg.MovePosition(rg.position + dir);
            if (!animationsOff)
            {
                if (Math.Abs(dir.x) > Math.Abs(dir.y))
                {
                    if (dir.x > 0) anim.CrossFade("RangeRunRight", 0);
					else anim.CrossFade("RangeRunLeft", 0);
                }
                else
                {
					if (dir.y > 0) anim.CrossFade("RangeRunUp", 0);
					else anim.CrossFade("RangeRunDown", 0);
                }
            }
        }

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
            if(playOnAttack != null)
            {
                SM.loadSound(playOnAttack);
                SM.playSound();
            }
           
        }
    }
    //Keeps track of the unit's health
    public void damage(int dmg)
    {
        if (dmgTimer > 0)
        {
            dmgTimer -= dmgCD;
        }
        else
        {
            health = health - dmg;
            if(playOnAttack != null)
            {
                SM.loadSound(playOnHurt);
                SM.playSound();
            }
           
            Debug.Log(unitName + health);
            if (health <= 0)
            {
                if( isBoss && bossDeath != null)
                {
                    SM.loadSound(bossDeath);
                    SM.playSound();
                }
                else if(playOnDeath != null)
                {
                    SM.loadSound(playOnDeath);
                    SM.playSound();
                }
                
                Destroy(gameObject);
            }
            dmgTimer = dmgCD;
        }

    }
    
    //Finds the target the unit is pathing towards
    public Vector3 FindTarget()
    {
        //If tracking the player return the player's current position
        if (aggro)
        {
            return Player.transform.position;
        }
        //else return the next point in the unit's patrol
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
            repathDelay = 15;
        }

    }
	
	public void animate_attack()
	{
        if (!animationsOff)
        {
			if (anim.GetCurrentAnimatorStateInfo(0).IsName("RangeIdleLeft"))
            {
				anim.CrossFade("RangeAttackLeft", 0);
            }
			else if (anim.GetCurrentAnimatorStateInfo(0).IsName("RangeIdleRight"))
            {
				anim.CrossFade("RangeAttackRight", 0);
            }
			else if (anim.GetCurrentAnimatorStateInfo(0).IsName("RangeIdleUp"))
            {
				anim.CrossFade("RangeAttackUp", 0);
            }
			else if (anim.GetCurrentAnimatorStateInfo(0).IsName("RangeIdleDown"))
            {
				anim.CrossFade("RangeAttackDown", 0);
            }
        }
	}
}

using UnityEngine;
using System.Collections;

public class MobControl : MonoBehaviour
{
    public GameObject Player;
    float aggroRange = 5;    //Distance when the unit will start tracking the player  
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

    }

    // Update is called once per frame
    void Update()
    {
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

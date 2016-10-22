﻿using UnityEngine;
using System.Collections;

public class MobControl : MonoBehaviour
{
    public GameObject Player;
    public PlayerHealth PH;
    float attackRange = 1;    //Mobs will attempt to attack the player within this range
    float strength;           //damage done by an attack, will probably want to change this for different enemies
    float attackTimer;        //For changing how often an enemy can attack
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
        attackTimer = 5;
        strength = 10;
        PH = GameObject.FindObjectOfType(typeof(PlayerHealth)) as PlayerHealth;
    }

    // Update is called once per frame
    void Update()
    {
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
    //how enemies inflict damage on the player
    public void Attack() {
        if(Player.GetComponent("PlayerHealth") != null)
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

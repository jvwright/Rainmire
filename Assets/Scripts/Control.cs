using UnityEngine;
using System;
using System.Collections;

public class Control : MonoBehaviour
{

    public float speed;
    private Animator anim;
    private Rigidbody2D rb;
    public PlayerAttack attackL;
    public PlayerAttack attackR;
    public PlayerAttack attackU;
    public PlayerAttack attackD;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        /*
         * Reading input for movement. Moving the sprite as well as animating it. The constant being multiplied in the 
         * translate function determines the speed will need to be adjust as the game develops. The second argument in the 
         * CrossFade function is the delay in the animation. Zero seems to work fine. With the current method of 
         * moving the character, I'm not sure how to achieve diagonal movement. 
         */

        const float movementSpeed = 10;

        if (Input.GetKey(KeyCode.LeftArrow)) { rb.velocity = Vector2.left * movementSpeed; }
        else if (Input.GetKey(KeyCode.RightArrow)) { rb.velocity = Vector2.right * movementSpeed; }
        else if (Input.GetKey(KeyCode.UpArrow)) { rb.velocity = Vector2.up * movementSpeed; }
        else if (Input.GetKey(KeyCode.DownArrow)) { rb.velocity = Vector2.down * movementSpeed; }


        /*
         * Reading input for attack and animating it. 
         */
        if (Input.GetKey(KeyCode.Z))
        {
            // anim.GetCurrentAnimatorStateInfo(0).IsName("RunLeft")
            if (Input.GetKey(KeyCode.LeftArrow) || anim.GetCurrentAnimatorStateInfo(0).IsName("IdleLeft"))
            {
                attackL.setEnabled(true);
                anim.CrossFade("AttackLeft", 0);
            }
            else if (Input.GetKey(KeyCode.RightArrow) || anim.GetCurrentAnimatorStateInfo(0).IsName("IdleRight"))
            {
                attackR.setEnabled(true);
                anim.CrossFade("AttackRight", 0);
            }
            else if (Input.GetKey(KeyCode.UpArrow) || anim.GetCurrentAnimatorStateInfo(0).IsName("IdleForward"))
            {
                attackU.setEnabled(true);
                anim.CrossFade("AttackForward", 0);
            }
            else if (Input.GetKey(KeyCode.DownArrow) || anim.GetCurrentAnimatorStateInfo(0).IsName("IdleBack"))
            {
                attackD.setEnabled(true);
                anim.CrossFade("AttackBack", 0);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.CrossFade("RunLeft", 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            anim.CrossFade("RunRight", 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.CrossFade("RunForward", 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            anim.CrossFade("RunBack", 0);
        }
    }
    /*
     * Called at the end of the attack animation to disable the sword hitbox  
     * There is probably a much better way to do this
     */
    void setEnabled(string dir)
    {
        if (dir == "L")
        {
            attackL.setEnabled(false);
        }
        if (dir == "R")
        {
            attackR.setEnabled(false);
        }
        if (dir == "U")
        {
            attackU.setEnabled(false);
        }
        if (dir == "D")
        {
            attackD.setEnabled(false);
        }
    }
}

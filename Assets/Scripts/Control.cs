using UnityEngine;
using System;
using System.Collections;

public class Control : MonoBehaviour {

    public float speed;
    private Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate() {

        /*
         * Reading input for movement. Moving the sprite as well as animating it. The constant being multiplied in the 
         * translate function determines the speed will need to be adjust as the game develops. The second argument in the 
         * CrossFade function is the delay in the animation. Zero seems to work fine. 
         */

        const float movementSpeed = 4;

        if (Input.GetKey(KeyCode.LeftArrow)){

            transform.Translate(Vector2.left * Time.deltaTime * movementSpeed);
            anim.CrossFade("RunLeft", 0);

        } else if (Input.GetKey(KeyCode.RightArrow)){

            transform.Translate(Vector2.right * Time.deltaTime * movementSpeed);
            anim.CrossFade("RunRight", 0);

        } else if (Input.GetKey(KeyCode.UpArrow)) {

            transform.Translate(Vector2.up * Time.deltaTime * movementSpeed);
            anim.CrossFade("RunForward", 0);

        } else if (Input.GetKey(KeyCode.DownArrow)) {

            transform.Translate(Vector2.down * Time.deltaTime * movementSpeed);
            anim.CrossFade("RunBack", 0);

        }

        /*
         * Reading input for attack and animating it. 
         */
        if (Input.GetKey(KeyCode.Z)) {
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("RunLeft") || anim.GetCurrentAnimatorStateInfo(0).IsName("IdleLeft")) {
                anim.CrossFade("AttackLeft", 0);
            } else if (anim.GetCurrentAnimatorStateInfo(0).IsName("RunRight") || anim.GetCurrentAnimatorStateInfo(0).IsName("IdleRight")) {
                anim.CrossFade("AttackRight", 0);
            } else if (anim.GetCurrentAnimatorStateInfo(0).IsName("RunForward") || anim.GetCurrentAnimatorStateInfo(0).IsName("IdleForward"))  {
                anim.CrossFade("AttackForward", 0);
            } else if (anim.GetCurrentAnimatorStateInfo(0).IsName("RunBack") || anim.GetCurrentAnimatorStateInfo(0).IsName("IdleBack")) {
                anim.CrossFade("AttackBack", 0);
            }
        }
    }
}

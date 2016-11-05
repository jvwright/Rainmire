using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{

    //public GameObject player;
    // Use this for initialization
    void Start()
    {
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (enabled)
        {
            if (col.gameObject.tag == "Enemy")
            {
                //this calls the damage method in the mobControl class
                col.SendMessageUpwards("damage", 3);
            }
        }
    }
    /*
     * Enables and disables the sword hitbox
     * called to enable the hitbox by the control class before the animation is startes
     * called to disable when the animation ends
     */
    public void setEnabled(bool isEnable)
    {
        enabled = isEnable;
    }
}

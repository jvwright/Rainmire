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
        if (Input.GetKey(KeyCode.Z))
        {
            //enabled = true;
        }
        if (enabled)
        {
            if (col.gameObject.tag == "Enemy")
            {
                //Destroy(col.gameObject);
                col.SendMessageUpwards("damage", 3);
            }
        }
    }
    /*
     * Enables and disables the sword hitbox
     */
    public void setEnabled(bool isEnable)
    {
        enabled = isEnable;
    }
}

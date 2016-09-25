using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	// Use this for initialization
	void Start () {
        enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

	}
    void OnTriggerStay2D(Collider2D col)
    {
        if (Input.GetKey(KeyCode.Z))
        {
            enabled = true;
        }
        if (enabled)
        {
            if (col.gameObject.tag == "Enemy")
            {
                //Debug.Log("test");
                Destroy(col.gameObject);
            }
        }
    }
}

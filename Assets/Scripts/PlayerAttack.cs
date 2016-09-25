using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("test");
           // Destroy(col.gameObject);
        }
    }
}

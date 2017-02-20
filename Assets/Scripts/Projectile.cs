using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    float timer = 60;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer -= 1;
        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {

        }
    }

    
}

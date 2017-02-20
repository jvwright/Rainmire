using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    float timer = 60;
    GameObject Player;
    PlayerHealth health;
    public float damage;
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

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            print("Collision detected");
            Player = coll.gameObject;
            health = Player.GetComponent<PlayerHealth>();
            health.Strike(20);
            if (Player.GetComponent("PlayerHealth") != null )
            {
                health = Player.GetComponent<PlayerHealth>();
                health.Strike(damage);
                Destroy(this.gameObject);
            }
        }
    }

    
}

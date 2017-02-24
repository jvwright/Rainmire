using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    float timer;
    float triggerTimer;
    GameObject Player;
    PlayerHealth health;
    public float damage;
    bool triggered;  //stops multiple instances of OnTriggerEnter being called
    // Use this for initialization
    void Start () {
        timer = 60;
        triggerTimer = 10;
        triggered = false;
    }
	
	// Update is called once per frame
	void Update () {
        timer -= 1;
        triggerTimer -= 1;
        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
        if(triggerTimer <= 0 && triggered)
        {
            triggered = !triggered;
            triggerTimer = 50;
        }

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (triggered)
        {
            Destroy(this.gameObject);
            Destroy(this);
            return; 

        }
        else if(coll.gameObject.tag == "Player")
        {
            print("Collision detected");
            Player = coll.gameObject;
            health = Player.GetComponent<PlayerHealth>();
            health.Strike(20);
            if (Player.GetComponent("PlayerHealth") != null )
            {
                health = Player.GetComponent<PlayerHealth>();
                health.Strike(damage);
                health.cantTouchThis = true;
                triggered = true;
                triggerTimer = 50;
                Destroy(this.gameObject);
                Destroy(this);
            }
        }
    }

    
}

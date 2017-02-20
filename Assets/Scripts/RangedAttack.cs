using UnityEngine;
using System.Collections;



public class RangedAttack : MonoBehaviour {

    public GameObject target;
    public GameObject projectile;
    GameObject source;
    Rigidbody2D rb;
    float timer = 25;
    // Use this for initialization
    void Start () {
        source = this.gameObject;

	}
	
	// Update is called once per frame
	void Update () {
        if (timer == 0)
        {
            Vector3 worldPos = (source.transform.position);
            Vector3 pos = target.transform.position;
            Vector3 shoot = pos - worldPos;
            Attack(shoot);
            timer = 25;
        }
        else { timer -= 1; };
	}

    void Attack(Vector3 s)
    {
        GameObject attack = (GameObject)Instantiate(projectile, transform.position, transform.rotation);
        rb = attack.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector3.ClampMagnitude(s*1000,500));
    }
}

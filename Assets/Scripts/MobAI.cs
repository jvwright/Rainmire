using UnityEngine;
using System.Collections;

public class MobAI : MonoBehaviour {
    public GameObject Control;
    int MinDist = 5;
    float speed = 3;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        
        Debug.Log(Control.transform.position);
        if (transform.position.x > Control.transform.position.x)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        if (transform.position.x < Control.transform.position.x)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        if (transform.position.y < Control.transform.position.y)
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed);
        }
        if (transform.position.y > Control.transform.position.y)
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed);
        }
    }
}

using UnityEngine;
using System.Collections;

public class MobAI : MonoBehaviour {
    public GameObject Player;
    int MinDist = 5;
    float speed = 2;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log((transform.position.x) - (Player.transform.position.x));
        Debug.Log((Player.transform.position.x) - (transform.position.x));
        if (Mathf.Abs((transform.position.x) - (Player.transform.position.x)) > 1.25|| Mathf.Abs((Player.transform.position.x) -(transform.position.x)) > 1.25)
        {
            if (transform.position.x > Player.transform.position.x)
            {
                transform.Translate(Vector2.left * Time.deltaTime * speed);
            }
            if (transform.position.x < Player.transform.position.x)
            {
                transform.Translate(Vector2.right * Time.deltaTime * speed);
            }
        }

        if (Mathf.Abs((transform.position.y) - (Player.transform.position.y)) > 1.25 || Mathf.Abs((Player.transform.position.y) - (transform.position.y)) > 1.25)
        {
            if (transform.position.y < Player.transform.position.y)
            {
                transform.Translate(Vector2.up * Time.deltaTime * speed);
            }
            if (transform.position.y > Player.transform.position.y)
            {
                transform.Translate(Vector2.down * Time.deltaTime * speed);
            }
        }
    }
}

using UnityEngine;
using System;
using System.Collections;

public class Control : MonoBehaviour {

    public float speed = 2;
    // SpriteRenderer rend;
   //  public Sprite LeftBird;

	// Use this for initialization
	void Start () {
	   // rend = GetComponent<SpriteRenderer>();
        // LeftBird = (Sprite) Resources.Load("LeftBird", typeof(Sprite));
    }

    // Update is called once per frame
    void Update() {

        float moveX = Input.GetAxis("Horizontal");
        // Debug.Log(moveX);
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, moveX * speed);


        float moveY = Input.GetAxis("Vertical");

        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.y, moveY * speed);

        // if (Input.GetKeyDown(KeyCode.Space)){
           // Debug.Log("HAHAH");
          //  rend.sprite = LeftBird;
        //}

    }
}

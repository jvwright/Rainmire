using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {
    float timer = 60;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer -= 1;
        if(timer <= 0)
        {
            Destroy(this.gameObject);
        }
	}
}

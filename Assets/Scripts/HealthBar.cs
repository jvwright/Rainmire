using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void decreaseHealth(float HP){
		transform.localScale = new Vector3((HP/100), 1, 1);
	}
}

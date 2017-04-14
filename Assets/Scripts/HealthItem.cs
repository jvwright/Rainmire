using UnityEngine;
using System.Collections;

public class HealthItem : MonoBehaviour {

	PlayerHealth PH;

	// Use this for initialization
	void Start () {
		PH = GameObject.FindObjectOfType(typeof(PlayerHealth)) as PlayerHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collision) {
		// Destroy(collision.gameObject);
		Destroy(this.gameObject);
		if (PH.HP < 75) {
			PH.HP += 25;
			GameObject.Find ("GreenHealthBar").transform.GetComponent ("HealthBar").SendMessage ("decreaseHealth", PH.HP);
		} else {
			PH.HP = 100;
			GameObject.Find ("GreenHealthBar").transform.GetComponent ("HealthBar").SendMessage ("decreaseHealth", PH.HP);
		}
	}
}

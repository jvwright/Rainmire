using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    /*
     * This class does nothing at the most but will need to be used to control the camera. 
     */ 

    //public GameObject player;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
       // offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        //transform.position = player.transform.position + offset;
	}
}

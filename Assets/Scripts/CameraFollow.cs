using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject PC;
    private Vector3 vec;

	// Use this for initialization
	void Start () {
        vec = transform.position - PC.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = PC.transform.position + vec;
	}
}

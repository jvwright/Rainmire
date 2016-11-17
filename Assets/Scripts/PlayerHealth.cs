﻿using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

    public float HP;
    GameObject player;
    public GameObject image;

    // Use this for initialization
    void Start()
    {
        HP = 100;
    }

    // Update is called once per frame
    void Update()
    {
        player = this.gameObject;
    }

    //Use this to do damage to the character
    public void Strike(float damage)
    {
        //Debug.Log("player hp" + HP);
        HP = HP - damage;
        if (HP <= 0)
        {
			HP = 0;
			player.gameObject.SetActive (false);
        }
		GameObject.Find("GreenHealthBar").transform.GetComponent("HealthBar").SendMessage("decreaseHealth", HP);
    }
}
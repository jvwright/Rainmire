﻿using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

    float HP;
    GameObject player;
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
        HP = HP - damage;
        Debug.Log(HP);
        if (HP <= 0)
        {
            Destroy(player);
        }
    }
}
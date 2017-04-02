using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

    public float HP;
    GameObject player;
    public GameObject image;
    public bool cantTouchThis = false;     //attempting to prevent character from taking so much projectile damage
    float InvinciTimer = 5;
    public AudioClip sound;
    private AudioSource hurt;

    // Use this for initialization
    void Start()
    {
        HP = 100;
        player = this.gameObject;
        if(player != null)
        {
            hurt = player.AddComponent<AudioSource>();
            hurt.clip = sound;
            hurt.loop = false;
            hurt.playOnAwake = false;
            hurt.volume = 1;

        }
    }

    // Update is called once per frame
    void Update()
    {
        player = this.gameObject;
        if (cantTouchThis)
        {
            InvinciTimer -= 1;
        }
        if(InvinciTimer <=0 && cantTouchThis)
        {
            cantTouchThis = false;
            
        }
    }

    //Use this to do damage to the character
    public void Strike(float damage)
    {
        if (!cantTouchThis)
        {
            //Debug.Log("player hp" + HP);
            HP = HP - damage;
            if (HP <= 0)
            {
                HP = 0;
                player.gameObject.SetActive(false);
            }
            GameObject.Find("GreenHealthBar").transform.GetComponent("HealthBar").SendMessage("decreaseHealth", HP);
            
                hurt.Play();
            
        }
    }
}
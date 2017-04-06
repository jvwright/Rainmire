using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

    public float HP;
    GameObject player;
    public GameObject image;
    public bool cantTouchThis = false;     //attempting to prevent character from taking so much projectile damage
    float InvinciTimer = 5;
    public AudioClip playOnHurt;
    public AudioClip playOnDeath;
    private AudioSource hurt;
    //private AudioSource death;
    GameObject soundplayer;
    SoundManager SM;
    
    


    // Use this for initialization
    void Start()
    {
        HP = 100;
        player = this.gameObject;
        hurt = player.GetComponent<AudioSource>();
        soundplayer = GameObject.FindGameObjectWithTag("playersounds");
        SM = soundplayer.GetComponent<SoundManager>();
        /*
        if(player != null)
        {
            AudioSource[] list = player.GetComponents<AudioSource>();
            for(int i = 0; i < list.Length; i++)
            {
                AudioSource temp = list[i];
                if(temp.clip = playOnHurt)
                {
                    hurt = temp;
                    hurt.clip = playOnHurt;
                }
                if(temp.clip = playOnDeath)
                {
                    death = temp;
                    death.clip = playOnDeath;
                }
            }
           

        }
        */
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
            //hurt.Play();
            //Debug.Log("player hp" + HP);
            HP = HP - damage;
            SM.loadSound(playOnHurt);
            SM.playSound();
            //hurt.Play();
            //AudioSource.PlayClipAtPoint(playOnHurt, player.GetComponent<Transform>().position);
            if (HP <= 0)
            {
                HP = 0;
                SM.loadSound(playOnDeath);
                SM.playSound();
            // AudioSource.PlayClipAtPoint(playOnDeath, player.GetComponent<Transform>().position);
            player.gameObject.SetActive(false);
                
            }
            GameObject.Find("GreenHealthBar").transform.GetComponent("HealthBar").SendMessage("decreaseHealth", HP);
            
                
            
        
    }
}
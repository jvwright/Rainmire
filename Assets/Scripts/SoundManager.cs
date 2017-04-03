using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    private AudioSource source;
    GameObject sounds;


    void Awake()
    {
        sounds = this.gameObject;
        source = sounds.GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void loadSound(AudioClip clip)
    {
        source.clip = clip;
    }

    public void playSound()
    {
        source.Play();
    }
}

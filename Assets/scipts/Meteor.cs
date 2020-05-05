using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    //this is to allow this script to call functions from gamemanager so can play sounds
    public GameObject gamemanager;
    private float lifetime = 10.0f;

    [Header("Sound Settings")]
    AudioSource Audio;
    public AudioClip meteorTravel;
    public float meteorTravelVolume = 1.0f;


    // Start is called before the first frame update
    private void Awake()
    {
        Audio = gameObject.GetComponent<AudioSource>();
    }
    void Start()
    {
        //gamemanager.GetComponent<GameManager>();
        Audio = gameObject.GetComponent<AudioSource>();
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
        //if(game)
    }
    private void OnCollisionEnter(Collision collision)
    {
        Audio.Stop();
        Destroy(gameObject);
    }
    public void playsound()
    {
        Audio.PlayOneShot(meteorTravel, meteorTravelVolume);
    }
}

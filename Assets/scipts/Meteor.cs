﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    //this is to allow this script to call functions from gamemanager so can play sounds
    //public GameObject gamemanager;
    private float lifetime = 10.0f;

    [Header("Sound Settings")]
    GameManager gameManager;
    AudioSource Audio;
    public AudioClip meteorTravel;
    public float meteorTravelVolume = 1.0f;

    [Header("Particle Settings")]
    public GameObject particle;


    // Start is called before the first frame update
    private void Awake()
    {
        Audio = gameObject.GetComponent<AudioSource>();
    }
    void Start()
    {
        
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
        Instantiate(particle, gameObject.transform);
        Audio.Stop();
        //gameManager.GetComponent<GameManager>().playMeteorImpact();
        
        Destroy(gameObject);
    }
    public void playsound()
    {
        Audio.PlayOneShot(meteorTravel, meteorTravelVolume);
    }
}

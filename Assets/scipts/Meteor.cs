using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    //this is to allow this script to call functions from gamemanager so can play sounds
    public GameObject gamemanager;
    private float lifetime = 10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
       //gamemanager.GetComponent<GameManager>();
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
        //if(game)
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private GameObject[] players;
    public float blackHolePullSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var player in players)
        {
            player.gameObject.GetComponent<Rigidbody>().AddForce((gameObject.transform.position - player.GetComponent<Transform>().position) * blackHolePullSpeed);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
       
        if(collidedObject.CompareTag("player"))
        {
            Destroy(collidedObject);
        }
    }
}

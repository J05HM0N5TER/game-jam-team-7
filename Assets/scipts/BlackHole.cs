using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
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
        if(players != null)
        {
            foreach (var player in players)
            {
                player.gameObject.GetComponent<Rigidbody>().AddForce((gameObject.transform.position - player.GetComponent<Transform>().position) * blackHolePullSpeed);
            }
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
       
        if(collidedObject.CompareTag("Player"))
        {
            // Add win 
            FindObjectOfType<GameManager>().AddRoundLossToPlayer(collidedObject.GetComponent<Player_controller>().controller);
        }
    }
}

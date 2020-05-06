using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    public int punchForce = 5;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            float xRotation = -Vector3.Normalize(transform.parent.position - transform.position).x;
            other.GetComponent<Rigidbody>().AddForce(new Vector3(xRotation * punchForce, 0, 0), ForceMode.Impulse);
            

            other.GetComponent<Player_controller>().playHurtSounds();
        }
    }
}

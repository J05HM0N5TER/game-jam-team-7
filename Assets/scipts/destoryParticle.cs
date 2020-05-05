using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoryParticle : MonoBehaviour
{
    public int lifeSpan = 2;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

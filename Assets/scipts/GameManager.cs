using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject meteorPrefab;
    public float meteorSpawnDelay = 2.0f;
    private bool canSpawnMeteor = true;
    public float sizeOfSpawnRadius = 20.0f;
    public float minDistanceFromCentr = 20.0f;
    public float meteorSpeed = 5.0f;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawnMeteor)
        {
            Vector3 randomPos = Random.insideUnitCircle * sizeOfSpawnRadius;
            randomPos += randomPos.normalized * minDistanceFromCentr;
            GameObject newMeteor =  Instantiate(meteorPrefab, randomPos, new Quaternion(0, 0, 0, 0));
            newMeteor.gameObject.GetComponent<Rigidbody>().AddForce((player.transform.position - newMeteor.GetComponent<Transform>().position) * meteorSpeed, ForceMode.Impulse);
            StartCoroutine(spawnMeteor());
        }
        

    }
    IEnumerator spawnMeteor()
    {
        canSpawnMeteor = false;
        yield return new WaitForSeconds(meteorSpawnDelay);
        canSpawnMeteor = true;

    }
    void playsounds()
    {

    }
    void playanothersound()
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Meteor settings")]
    public GameObject meteorPrefab;
    public float meteorSpawnDelay = 2.0f;
    private bool canSpawnMeteor = true;
    public float sizeOfSpawnRadius = 20.0f;
    public float minDistanceFromCentr = 20.0f;
    public float meteorSpeed = 5.0f;
    //player to point at
    [Header("Player to point at")]
    public GameObject player;

    [Header("Sound Settings")]
    AudioSource Audio;
    public AudioClip meteorTravel;
    public float meteorTravelVolume = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        Audio = gameObject.GetComponent<AudioSource>();
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
            newMeteor.GetComponent<Meteor>().playsound();
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
        
        //Audio.PlayOneShot(meteorTravel, meteorTravelVolume);
        
    }
    void playanothersound()
    {

    }

}

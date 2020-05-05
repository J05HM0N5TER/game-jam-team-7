using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

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
    [Header("Death settings")]
    public float killBarrierY = -10f;
    private GameObject[] players;

    [Header("Sound Settings")]
    public AudioSource Audio;
    public AudioClip MeteorImpactSound;
    public float MeteorImpactVolume = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        //Audio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawnMeteor)
        {
            // Get random player to aim for
            GameObject player = players[Random.Range(0, players.Length)];
            // Create random position to spawn meteor
            Vector3 randomPos = Random.insideUnitCircle * sizeOfSpawnRadius;
            randomPos += randomPos.normalized * minDistanceFromCentr;
            // Create meteor at new random position
            GameObject newMeteor =  Instantiate(meteorPrefab, randomPos, Quaternion.identity);
            // Launch new meteor at selected player
            newMeteor.gameObject.GetComponent<Rigidbody>().AddForce((player.transform.position - newMeteor.GetComponent<Transform>().position) * meteorSpeed, ForceMode.Impulse);
            newMeteor.GetComponent<Meteor>().playsound();
            StartCoroutine(spawnMeteor());
        }
        foreach (var player in players)
        {
            if (player.transform.position.y < killBarrierY)
            {
                SceneController sceneController = FindObjectOfType<SceneController>();
                XboxController playerController = player.GetComponent<Player_controller>().controller;
                // Add win to player
                sceneController.AddWinToPlayer(playerController);
                // Reload game
                sceneController.loadGame();
            }
        }

    }
    IEnumerator spawnMeteor()
    {
        canSpawnMeteor = false;
        yield return new WaitForSeconds(meteorSpawnDelay);
        canSpawnMeteor = true;

    }
    public void playMeteorImpact()
    {
        
        Audio.PlayOneShot(MeteorImpactSound, MeteorImpactVolume);
        
    }
    void playanothersound()
    {

    }

}

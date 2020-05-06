using System;
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
    public GameObject DemonUI;
    public GameObject HumanUI;

    [Header("Sound Settings")]
    public AudioSource Audio;
    public AudioClip MeteorImpactSound;
    public float MeteorImpactVolume = 1.0f;
    public List<int> playerRoundScores;
    public int defaultLeves = 3;
    private SceneController sceneController;


    // Start is called before the first frame update
    void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
        players = GameObject.FindGameObjectsWithTag("Player");
        playerRoundScores = new List<int>();
        for (int i = 0; i < players.Length; i++)
        {
            playerRoundScores.Add(defaultLeves);
        }
        Audio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawnMeteor)
        {
            // Get random player to aim for
            GameObject player = players[UnityEngine.Random.Range(0, players.Length)];
            // Create random position to spawn meteor
            Vector3 randomPos = UnityEngine.Random.insideUnitCircle * sizeOfSpawnRadius;
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
                XboxController playerController = player.GetComponent<Player_controller>().controller;
                // Add win to player
                AddRoundLossToPlayer(playerController);
                // Reload game
                //sceneController.loadGame();
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

    public void AddRoundLossToPlayer(XboxController player)
    {
        UpdateUI();
        int playerNumber = (int)player - 1;
        playerRoundScores[playerNumber]--;
        if (playerRoundScores[playerNumber] <= 0)
        {
            sceneController.AddMatchLossToPlayer(player);
        }
        else
        {
            foreach (var currectPlayer in players)
            {

                Player_controller playerController = currectPlayer.GetComponent<Player_controller>();
                if (playerController.controller == player)
                {
                    playerController.playerRespawn();
                }
            }
        }
    }

    public void UpdateUI()
    {
        Transform[] demonLives = DemonUI.GetComponentsInChildren<Transform>();
        for (int i = 0; i < demonLives.Length; i++)
        {
            GameObject currentUI = demonLives[i].gameObject;
            currentUI.SetActive(i < playerRoundScores[0]);
        }
        Transform[] humanLives = DemonUI.GetComponentsInChildren<Transform>();
        for (int i = 0; i < humanLives.Length; i++)
        {
            GameObject currentUI = humanLives[i].gameObject;
            currentUI.SetActive(i < playerRoundScores[1]);
        }
    }
}

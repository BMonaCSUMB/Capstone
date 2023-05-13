using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossSpawner : MonoBehaviour
{
    // List of prefabs to serve as enemy units
    public GameObject EnemyBossUnitPrefab;
    public float spawnTimer=4f;
    public float countDown=4f;
    public float timeBetweenEachWave=1f;
    public float amountOfEnemiesSpawnedPerWave=1f;
    public float amountOfEnemiesSpawned=0f;
    public float amountOfEnemiesForThisLevel=1f;
    public bool endoftheBossLevel;
    private PlayerController playerController;

    void Start() {
        // Grab PlayerController Script from Game Object that has the tag "Player"
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (countDown <= 0f )
        {
            // Call Coroutine function to start spawning enemy waves if the countdown reaches 0
            StartCoroutine(spawnEnemyWave());
            // The countdown is then set to the spawnTimer
            countDown = spawnTimer;
        }
        // Subtract Class time float of deltatime from the countDown
        countDown -= Time.deltaTime;
        // If the level is over
        if (endoftheBossLevel == true)
        {
            // Turn off this update function
            enabled = false;
        }
        // If thePlayerisDead bool is set to true from PlayerController Script
        if (playerController.thePlayerisDead == true)
        {
            // Turn off this update function
            enabled = false;
            //Debug.Log("Enemy Spawner Stopped Because Player Is Dead");
        }
    }

    IEnumerator spawnEnemyWave()
    {
        // For loop int i is equal to 0, i is less than "amountOfEnemiesPerWave" increment I
        for (int i = 0; i< amountOfEnemiesSpawnedPerWave; i++)
        {
            // Since I is less than the amountOfEnemiesPerWave I want, spawn enemy units
            spawnEnemyBossUnit();
            // Yield a return that waits for seconds (variable timeBetweenEachWave)
            yield return new WaitForSeconds(timeBetweenEachWave);
        }
    }

    // Function for Spawning Enemy Units 
    void spawnEnemyBossUnit()
    {
        // Check if the level is over or not first
        endoftheBossLevel = false;
        // if the amount of enemies spawned is less than the amount of enemies I selected for this level then
        if (amountOfEnemiesSpawned < amountOfEnemiesForThisLevel )
        {
            //Debug.Log("Spawning Boss Unit");
            // Create an enemy unit clone using the Prefab that I selected in the Unity Editor for this script
            GameObject enemyBossUnit = Instantiate(EnemyBossUnitPrefab);
            // Increment variable amount of enemies spawned
            amountOfEnemiesSpawned++;
        } else {
            // If the amount of enemies spawned is equal to or greater than 
            // amount of enemies I selected for this level then set end of level true
            endoftheBossLevel = true;
            //Debug.Log("No More Units Will Spawn");
        }
    }
}


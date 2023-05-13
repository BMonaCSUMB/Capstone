using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // List of prefabs to serve as enemy units
    public GameObject EnemyUnitPrefab;
    public float spawnTimer=4f;
    public float countDown=4f;
    public float timeBetweenEachWave=1f;
    public float amountOfEnemiesPerWave=1f;
    public float amountOfEnemiesSpawned=0f;
    public float amountOfEnemiesForThisLevel=10f;
    public bool endoftheLevel;
    public bool hasPlayerWon;
    private PlayerController playerController;
    private WinMenu winMenu;
    private EnemyBossSpawner enemyBossSpawner;

    void Start()
    {
        // Grab PlayerController Script from Game Object that has the tag "Player"
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        // Grab WinMenu Script from Game Object that has the tag "Player"
        winMenu = GameObject.FindGameObjectWithTag("Player").GetComponent<WinMenu>();
        // Grab EnemyBossSpawner Script from Game Object that has the tag "Player"
        enemyBossSpawner = GameObject.FindGameObjectWithTag("Player").GetComponent<EnemyBossSpawner>();
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
        // if both the enemy spawner script and enemy boss spawner script booleans are set to true then
        if (endoftheLevel && enemyBossSpawner.endoftheBossLevel == true)
        {
            // Find Game objects with tag Enemy and store them in an array
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            // Find Game objects with the tag Enemy Boss and store them in an array
            GameObject[] bosses = GameObject.FindGameObjectsWithTag("EnemyBoss");
            // If the enemies array length is 0 proceed to next step
            if (enemies.Length == 0)
            {
                // If the bosses array length is 0 do
                if (bosses.Length == 0 && playerController.thePlayerisDead == false)
                {
                    // Set hasPlayerWon bool to True
                    hasPlayerWon = true;
                    // Call the Win Menu
                    winMenu.Win();
                }
            }
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
        for (int i = 0; i < amountOfEnemiesPerWave; i++)
        {
            // Since I is less than the amountOfEnemiesPerWave I want, spawn enemy units
            spawnEnemyUnit();
            // Yield a return that waits for seconds (variable timeBetweenEachWave)
            yield return new WaitForSeconds(timeBetweenEachWave);
        }
    }

    // Function for Spawning Enemy Units 
    void spawnEnemyUnit()
    {
        // Check if the level is over or not first
        endoftheLevel = false;
        // if the amount of enemies spawned is less than the amount of enemies I selected for this level then
        if (amountOfEnemiesSpawned < amountOfEnemiesForThisLevel )
        {
            //Debug.Log("Spawning Unit");
            // Create an enemy unit clone using the Prefab that I selected in the Unity Editor for this script
            GameObject enemyUnit = Instantiate(EnemyUnitPrefab);
            // Increment variable amount of enemies spawned
            amountOfEnemiesSpawned++;
        } else {
            // If the amount of enemies spawned is equal to or greater than 
            // amount of enemies I selected for this level then set end of level true
            endoftheLevel = true;
            //Debug.Log("No More Units Will Spawn");
        }
    }
}

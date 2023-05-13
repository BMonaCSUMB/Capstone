using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;
    private PlayerController playerController;
    private EnemySpawner enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        // Hide pause menu when game starts
        pauseMenu.SetActive(false);
        // Grab PlayerController Script from Game Object that has the tag "Player"
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        // Grab EnemySpawner Script from Game Object that has the tag "Player"
        enemySpawner = GameObject.FindGameObjectWithTag("Player").GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the hasPlayerWon bool from Enemy Spawner script is set to true then game is over
        // therefore disable this update function and don't allow the player to pause
        if (enemySpawner.hasPlayerWon == true)
        {
            //Debug.Log("Pause Menu Stopped Because Player has Won");
            enabled = false;
        }
        // If Player is Dead don't allow for Pause Menu to work
        if (playerController.thePlayerisDead == true)
        {
            //Debug.Log("Pause Menu Stopped Because Player Is Dead");
            enabled = false;
        } else {
            // If player presses ESC key
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                // This allows player to close pause menu by pressing ESC key again
                // When PauseGame is called it sets isPaused to true, when the player presses ESC again
                // isPaused will be set to True and will call ResumeGame, which sets isPaused to false 
                if(isPaused == false)
                {
                    PauseGame();
                } else {
                    ResumeGame();
                }
            }   
        }
    }

    // Pause Game Function for Pause Menu
    public void PauseGame()
    {
        // Turn on the Pause Menu, Set Active state to true
        pauseMenu.SetActive(true);
        // Set timescale to 0, pauses the game
        Time.timeScale = 0f;
        // Set isPaused to true
        isPaused = true;
    }

    // Resume Game Function for Pause Menu
    public void ResumeGame()
    {
        // Turn off the Pause Menu, Set Active state to false
        pauseMenu.SetActive(false);
        // Return timescale to 1f (normal timescale)
        Time.timeScale = 1f;
        // Set isPaused to False
        isPaused = false;
    }

    // Quit Game Function for Pause Menu
    public void QuitGame()
    {
        // Completely close out of the application
        Application.Quit();
    }
}

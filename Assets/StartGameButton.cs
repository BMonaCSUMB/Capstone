using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    // Create the scene when start button is pressed
    public int gameStartingScene;

    // Start Game Function Main Menu
    public void StartGame()
    {
        SceneManager.LoadScene(gameStartingScene);
    }
}

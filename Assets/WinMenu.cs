using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    public GameObject winMenu;
 
    // Start is called before the first frame update
    void Start()
    {
        // Set menu state to false
        winMenu.SetActive(false);
    }

    public void Win()
    {
        //Debug.Log("Win Menu Is Active");
        
        // Set menu state to true
        winMenu.SetActive(true);
        // This is essentially a pause to the game via timeScale
        Time.timeScale = 0f;
    }

    //Quit Game Function for Win Menu
    public void QuitGame()
    {
        // Completely close out of the application
        Application.Quit();
     }
}
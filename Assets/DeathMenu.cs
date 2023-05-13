using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenu;
    private PlayerController playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        // Set Death Menu as inactive at Start
        deathMenu.SetActive(false);
        // Grab PlayerController Script from Game Object that has the tag "Player"
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    //Update is called once per frame
    void Update()
    {
        // If Player is Dead don't allow for Pause Menu to work
        if (playerController.thePlayerisDead == true)
        {
            //Debug.Log("Death Menu Is Active");
            // Set Dead Menu as active
            deathMenu.SetActive(true);
        }
    }

    // Retry Function Death Menu
    public void Retry()
    {
        // Reload Scene 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    //Quit Game Function Death Menu
    public void QuitGame()
    {
        // Completely close out of the application
        Application.Quit();
     }
}

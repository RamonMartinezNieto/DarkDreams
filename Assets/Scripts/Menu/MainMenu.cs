using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    
    public void StartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

    public void ExitGame()
    {
        Application.Quit();
        // TODO: Quit this when compile game. 
        Debug.Log("Exit the game.");
    }



}

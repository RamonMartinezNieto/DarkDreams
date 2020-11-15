/**
 * Department: Game Developer
 * File: Pause.cs
 * Objective: Pause the game.
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;

/**
 * Class with all functions to pause and restart the game.
 * @version 1.0.0
 * @author Ramón Martínez Nieto
 */
public class Pause : MonoBehaviour
{
    /**
     * Static variable to know if the game is paused.  
     */
    public static bool GameIsPaused = false;

    /**
     * GameObject that represent the escape menu, to know if the script need pause the game.
     */
    [Tooltip("GameObject that contains the escape menu")]
    public GameObject MenuEsc;

    /**
     * GameObject that represent the GameOver menu, to know if the script need pause the game.
     */
    [Tooltip("GameObject that contains the Game Over menu")]
    public GameObject MenuGameOver;

    private void Update()
    {
        if (MenuEsc.activeSelf || MenuGameOver.activeSelf)
        {
            PauseGame();
        }
        else 
        {
            ResumeGame();
        }
    }

    /**
     * Method to pause the game.
     */
    public void PauseGame()
    {
        GameIsPaused = true;
        Time.timeScale = 0f; 
    }

    /**
    * Method to restart the game.
    */
    public void ResumeGame()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
    }
}

using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject MenuEsc;
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

    public void PauseGame()
    {
        GameIsPaused = true;
        Time.timeScale = 0f; 
    }

    public void ResumeGame()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
    }
}

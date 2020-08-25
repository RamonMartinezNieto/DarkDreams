using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator [] buttonsAnimator;

    private void Start()
    {
        //New instance to work to change options
       // HandlerOptions ho = HandlerOptions.Instance; 

    }

    public void StartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

    public void ExitGame()
    {
        Application.Quit();
        // TODO: Quit this when compile game. 
        Debug.Log("Exit the game.");
    }

    public void DisableButtons() 
    {
        foreach (Animator a in buttonsAnimator)
        {
            //Note: Before of the launch triggert set state of the button. If not set bool it causes error
            a.SetBool("buttonActive", false);
            a.SetTrigger("start");
        }
    }

    public void ActiveButtons()
    {
        foreach (Animator a in buttonsAnimator)
        {
            a.SetBool("buttonActive", true);
            a.SetTrigger("start");
        }
    }

}

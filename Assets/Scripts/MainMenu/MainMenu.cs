﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Animator [] buttonsAnimator;

    public GameObject namePanel;
    public Animator panelAnimator;

    public GameObject panelShadow;

    public TMP_InputField firstUserName;

    public TMP_Text labelName;
    public TMP_Text labelNameOptions;

    public GameObject BackGround;
    private float currentPosX;
    private Vector3 endPosBack;
    private bool movementBack; 

    private void Start()
    {
        //TODO: PlayerPrefs.DeleteAll();

        ShowNameMenu();
        
        //TODO: Music
        SoundManager.Instance.PlayMusic("menuMusic");
        SoundManager.Instance.LoopMusic(true);

        Debug.Log(BackGround.transform.position);
        endPosBack = BackGround.transform.position + new Vector3(-1f,0,0);
        Debug.Log(endPosBack);
        movementBack = true; 
    }

    private void Update()
    {
        currentPosX = BackGround.transform.position.x;
        
        if (movementBack) {
            BackGround.transform.Translate(new Vector3(-.2f, 0, 0) * Time.deltaTime, Camera.main.transform);
            Debug.Log(endPosBack.x);
            if (currentPosX <= endPosBack.x)
            {
                movementBack = false;
                endPosBack *= -1;
            }

        }
        else if (!movementBack) {
            BackGround.transform.Translate(new Vector3(.2f, 0, 0) * Time.deltaTime, Camera.main.transform);
            if (currentPosX >= endPosBack.x)
            {
                movementBack = true;
                endPosBack *= 1;
            }
        }
     }

    public void ShowNameMenu() 
    {
        //TODO: need to change FIRSTIMEOPENING ¿User name in PlayerPrefs?
        if (PlayerPrefs.GetInt("FIRSTIMEOPENING", 1) == 1)
        {
            //Set first parameters
            HandlerOptions ho = HandlerOptions.Instance;
            ho.MusicOn = true;
            ho.MusicVolumen = 0.5f;
            ho.LanguageInt = 2;
            ho.CrossHairString = "white";

            //Ask for name
            namePanel.SetActive(true);
            panelAnimator.SetTrigger("smallBox");
            panelShadow.SetActive(true);

            PlayerPrefs.SetInt("FIRSTIMEOPENING", 0);
        }
    }

    public void AskCorrectName(TMP_InputField field) 
    {
        labelName.text = field.text; 
    }
    public void AskCorrectNameOptions(TMP_InputField field)
    {
        labelNameOptions.text = field.text;
    }

    public void SetFirstName() 
    {
        //PlayerPrefs.SetString("userName", firstUserName.text); 
        HandlerOptions.Instance.UserName = firstUserName.text;
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
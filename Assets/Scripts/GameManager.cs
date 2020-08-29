using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : PlayerConf
{
    public static GameManager Instance = null; 

    public TMP_Text labelName;
    public TMP_Text labelScore;

    public GameObject CanvasGamerOver; 

    public PlayerStats playerStats;

    private bool writeBD = true;  

    private int _currentScore;
    public int CurrentScore {
        get { 
            return _currentScore;  
        } 
        private set {
            _currentScore = value;
            labelScore.text = Convert.ToString(_currentScore);
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this) {
            Destroy(this);
        }
    }

    private void Start()
    {
        writeBD = true;
        labelName.text = UserName;
        //labelName.text = "Test Change";
        labelScore.text = "0000";
    }

    private void Update()
    {
        if (playerStats.CurrentHealt <= 0 && writeBD) 
        {
        //    FirebaseConnection.Instance.WriteNewScore(UserName, CurrentScore); 
            //Write Score in laderBoard
            CanvasGamerOver.SetActive(true);
            writeBD = false; 
        }
    }

    public void UpScore(int upScor)
    {
        //Todo: Score
        CurrentScore += upScor;
    }



}

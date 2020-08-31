using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : PlayerConf
{
    public static GameManager Instance = null; 

    public TMP_Text labelName;
    public TMP_Text labelScore;
    public TMP_Text labelTimer; 

    public GameObject CanvasGamerOver; 

    public PlayerStats playerStats;

    private bool writeBD = true;

    private int seconds = 0;
    private int minutes = 0;

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
        EnemyGenerator.Instance.GenerateEnemies(40); 

        writeBD = true;
        labelName.text = UserName;
        //labelName.text = "Test Change";
        labelScore.text = "0000";
    }

    private void FixedUpdate()
    {
        labelTimer.text = timerController();
    }

    private void Update()
    {
        if (playerStats.CurrentHealt <= 0 && writeBD) 
        {
            CanvasGamerOver.SetActive(true);

            //Write Score, only one time, be carefull with the writeBD variable
            if (CurrentScore > 0) FirebaseConnection.Instance.WriteNewScore(UserName, CurrentScore);
            writeBD = false; 
        }
    }

    public void UpScore(int upScor)
    {
        //Todo: Score
        CurrentScore += upScor;
    }

    private string timerController() 
    {
        int currentTimer = Convert.ToInt32(Time.time);

        if (seconds == 60)
        {
            seconds = 0;
            minutes++;
        }
        else 
        {
            seconds = currentTimer - (minutes*60);
        }

        return minutes + ":" + seconds; 
    }
}
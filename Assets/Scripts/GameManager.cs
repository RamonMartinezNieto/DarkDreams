using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : PlayerConf
{
    public static GameManager Instance = null;
    public static bool IsNewScore { get; private set; } = false; 

    public TMP_Text labelName;
    public TMP_Text labelScore;
    public TMP_Text labelTimer;

    public RectTransform barsTransform; 

    public GameObject CanvasGamerOver;
    public GameObject CanvasMenuEsc;
    public PlayerStats playerStats;

    private TimeController timeController; 

    private bool writeBD = true;

    //In minutes
    private int timeToShowNewEnemies = 1; 

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

        timeController = new TimeController();
    }

    private void Start()
    {
        EnemyGenerator.Instance.GenerateEnemies(25,3);
        writeBD = true;
        labelName.text = UserName;
        labelScore.text = "0000";

        SoundManager.Instance.PlayMusic("game1");
    }

    private void FixedUpdate()
    {
        if (playerStats.CurrentHealt >= 0f)
        {
            timeController.updateTime();

            labelTimer.text = timeController.getFormatTimer();
        }
    }

    private void Update()
    {
        if (!CanvasGamerOver.activeSelf)
        {
            if (playerStats.CurrentHealt <= 0 && writeBD)
            {
                IsNewScore = false;

                CanvasGamerOver.SetActive(true);
                
                SaveScoreAndTime(CurrentScore, timeController.getFormatTimer());

                WriteScoreInBBDD();

                timeController.restartTimer();
                writeBD = false;
            }

            GenerateEnemies();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CanvasMenuEsc.SetActive(true);
            }
        }
    }

    private void GenerateEnemies() 
    {
        //Only generate enemies if there are less 100
        if (EnemyRecovery.Instance.GetEnemiesAlive() < 100)
        {
            //Generate more enemies
            if (timeController.minutes >= timeToShowNewEnemies)
            {
                var minutes = timeController.minutes;
                //generate new enemies and update timeToShowNewEnemies
                EnemyGenerator.Instance.GenerateEnemies(UnityEngine.Random.Range(5 * minutes, 20 * minutes), UnityEngine.Random.Range(1 * minutes, 3 * minutes));
                timeToShowNewEnemies++;
            }
        }
    }


    private void WriteScoreInBBDD() 
    {
        List<UserScore> tenBestScores = FirebaseConnection.Instance.GetListUsers();

        try
        {
            if (tenBestScores.Count != 0)
            {
                //Write Score, only one time, be carefull with the writeBD variable
                if (tenBestScores.Count < 10)
                {
                    FirebaseConnection.Instance.WriteNewScore(UserName, CurrentScore, timeController.getFormatTimer());
                    IsNewScore = true;
                }
                else if (tenBestScores[tenBestScores.Count - 1].score < CurrentScore)
                {
                    FirebaseConnection.Instance.WriteNewScore(UserName, CurrentScore, timeController.getFormatTimer());
                    IsNewScore = true;
                }
                
            }
            else
            {
                FirebaseConnection.Instance.WriteNewScore(UserName, CurrentScore, timeController.getFormatTimer());
                IsNewScore = true;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("DataBase don't run currently");
        }
    }

    public void UpScore(int upScor)
    {
        //Todo: Score
        CurrentScore += upScor;
    }
    
    public string GetCurrentTime() { return labelTimer.text; }
}
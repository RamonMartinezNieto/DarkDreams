using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : PlayerConf
{
    public static GameManager Instance = null;
    public bool IsNewScore { get; set; } = false; 

    public TMP_Text labelName;
    public TMP_Text labelScore;
    public TMP_Text labelTimer;

    public TMP_Text roundLabel;
    public Animator roundAnimator; 

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

        StartCoroutine(ShowRound($"Wave {timeController.minutes+1}")); 

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
            GenerateEnemies();

            CallAnimationRoundTimer();

            if (playerStats.CurrentHealt <= 0 && writeBD)
            {
                IsNewScore = false;

                CanvasGamerOver.SetActive(true);

                SaveScoreAndTime(CurrentScore, timeController.getFormatTimer());

                FirebaseConnection.Instance.WriteScoreInBBDD(UserName, CurrentScore, timeController.getFormatTimer());

                timeController.restartTimer();
                writeBD = false;
            }

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

    public void UpScore(int upScor)
    {
        //Todo: Score
        CurrentScore += upScor;
    }
    
    public string GetCurrentTime() { return labelTimer.text; }

    private void CallAnimationRoundTimer() 
    {
        if (timeController.seconds == 48)
        {
            roundLabel.text = $"Wave {timeController.minutes + 1}";
            roundAnimator.SetBool("visible", true);

        }
        else if (timeController.seconds >= 50 && timeController.seconds <= 60)
        {
            roundLabel.text = (60 - timeController.seconds).ToString();
        }
        else if (timeController.seconds == 0) 
        {
            roundAnimator.SetBool("visible", false);
        }
    }

    private IEnumerator ShowRound(string text) 
    {
        roundLabel.text = text; 
        roundAnimator.SetBool("visible",true);
        yield return new WaitForSeconds(2f);
        roundAnimator.SetBool("visible", false);
    }


}
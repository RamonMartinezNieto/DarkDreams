using System;
using TMPro;
using UnityEngine;

public class GameManager : PlayerConf
{
    public static GameManager Instance = null; 

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
        if (playerStats.CurrentHealt <= 0 && writeBD) 
        {
            CanvasGamerOver.SetActive(true);

            timeController.restartTimer();

            //Write Score, only one time, be carefull with the writeBD variable
            if (CurrentScore > 0) FirebaseConnection.Instance.WriteNewScore(UserName, CurrentScore);
            writeBD = false; 
        }

        //Generate more enemies
        if (timeController.minutes >= timeToShowNewEnemies) 
        {
            var minutes = timeController.minutes;
            //generate new enemies and update timeToShowNewEnemies
            EnemyGenerator.Instance.GenerateEnemies(UnityEngine.Random.Range(5* minutes, 25 * minutes), UnityEngine.Random.Range(1*minutes, 3* minutes));
            timeToShowNewEnemies++;
        }
        
        if (EnemyRecovery.Instance.GetEnemiesAlive() <= 2) 
        {
            Debug.Log(EnemyRecovery.Instance.GetEnemiesAlive());
            var minutes = timeController.minutes;
            EnemyGenerator.Instance.GenerateEnemies(UnityEngine.Random.Range(2 * minutes, 10 * minutes), 0);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            CanvasMenuEsc.SetActive(true);
        }
    }

    public void UpScore(int upScor)
    {
        //Todo: Score
        CurrentScore += upScor;
    }
}
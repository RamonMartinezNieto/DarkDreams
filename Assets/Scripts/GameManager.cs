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
      //  EnemyGenerator.Instance.GenerateEnemies(40,3);

        writeBD = true;
        labelName.text = UserName;
        labelScore.text = "0000";

        //Set initial sizeBars
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        SetSizeBars((int)screenSize.x, (int)screenSize.y);
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
        WindowManager.Instance.ScreenSizeChangeEvent += Instance_ScreenSizeChangeEvent;

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
            //generate new enemies and update timeToShowNewEnemies
            EnemyGenerator.Instance.GenerateEnemies(UnityEngine.Random.Range(10,100), UnityEngine.Random.Range(3, 20));
            timeToShowNewEnemies++;
        }
    }

    public void UpScore(int upScor)
    {
        //Todo: Score
        CurrentScore += upScor;
    }

    //Event Handler when ScreenSize change 
    private void Instance_ScreenSizeChangeEvent(int Width, int Height)
    {
        SetSizeBars(Width, Height);
    }

    private void SetSizeBars(int Width, int Height) 
    {
        Vector3 newScale = new Vector3(1f, 1f, 0);
        float posY = -110f;

        if (Width < 480 || Height < 640)
        {
            newScale = new Vector3(.5f, .5f, 0);
            posY = -80f;
        }
        else if (Width < 600 || Height < 800)
        {
            newScale = new Vector3(.75f, .75f, 0);
            posY = -90f;
        }
        else
        {
            newScale = new Vector3(1f, 1f, 0);
        }

        barsTransform.anchoredPosition = new Vector2(0f, posY);
        barsTransform.localScale = newScale;
    }

}
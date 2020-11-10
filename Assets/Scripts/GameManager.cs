/**
 * Department: Game Developer
 * File: GameManager.cs
 * Objective: Control the flow of the game
 * Employee: Ramón Martínez Nieto
 */

using System;
using System.Collections;
using TMPro;
using UnityEngine;

/**
 * GameManager class to control the flow of the game. 
 * Inherit of the PlayerConf to know the configuration. 
 * 
 * @see PlayerConf
 * @author Ramón Martínez Nieto
 */
public class GameManager : PlayerConf
{
    /**
     * Static variable Instance to Singleton pattern
     */
    public static GameManager Instance = null;

    /**
     * To establish if is a new score or not.
     */
    public bool IsNewScore { get; set; } = false;

    /**
     * To establish if is a new score or not.
     */
    [Tooltip("Label with the player's name.")]
    public TMP_Text labelName;

    /**
     * Label with the player's score.
     */
    [Tooltip("Label with the player's score.")]
    public TMP_Text labelScore;

    /**
     * TextMesh Pro Label with the current time.
     */
    [Tooltip("Label with the current time.")]
    public TMP_Text labelTimer;

    /**
     * TextMesh Pro Label with the current round
     */
    [Tooltip("label with the current round.")]
    public TMP_Text roundLabel;

    /**
     * Animator of the Round Count 
     */
    [Tooltip("Animator of the round label.")]
    public Animator roundAnimator;

    /**
     * GameObject with the GameOver menu
     */
    [Tooltip("GameObject with the GameOver menu")]
    public GameObject CanvasGamerOver;

    /**
     * GameObject with the escape menu
     */
    [Tooltip("GameObject with the escape menu")]
    public GameObject CanvasMenuEsc;

    /**
     * To establish the Script PlayerStats
     */
    [Tooltip("Script PlayerStats")]
    public PlayerStats playerStats;

    /**
     * Script EnemyRecovery
     */
    [Tooltip("Script EnemyRecovery")]
    public EnemyRecovery enemeyRecovery;

    
    private TimeController timeController; 
    private bool writeBD = true;
    private int randMin = 10;
    private int randMax = 25; 

    //In minutes
    private int timeToShowNewEnemies = 1;
    private bool canScroll = true;
    private float timePassBewtweenWheels;

    private int _currentScore;
    /**
     * Get to know the current Score.
     * Set to establish a score.
     */
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

        timeController = GetComponent<TimeController>(); 
    }

    private void Start()
    {
        EnemyGenerator.Instance.GenerateEnemies(45,3);
        writeBD = true;
        labelName.text = UserName;
        labelScore.text = "0000";

        StartCoroutine(ShowRound("Wave 0")); 

        SoundManager.Instance.PlayMusic("game1");

        //suscribe methods to event. 
        TimeController.OnMinutesChanged += ControlRandMinAndMax;
        TimeController.OnMinutesChanged += GenerateEnemies;

        ChangeLabelTimer(); 
        TimeController.OnSecondsChanged += ChangeLabelTimer;
    }
    
    private void Update()
    {

        /*
        if (Input.GetKeyDown(KeyCode.T))
        {
            timeController.Seconds += 1; 
        }
        */


        if (!CanvasGamerOver.activeSelf)
        {

            CallAnimationRoundTimer();


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (CanvasMenuEsc.activeSelf)
                    CanvasMenuEsc.SetActive(false);
                else
                    CanvasMenuEsc.SetActive(true);
            }

            //Player die || game over if the time event is 0
            if (playerStats.CurrentHealt <= 0 && writeBD)
            {
                IsNewScore = false;
                timeToShowNewEnemies = 0;

                CanvasGamerOver.SetActive(true);

                SaveScoreAndTime(CurrentScore, timeController.getFormatTimer());

                FirebaseConnection.Instance.WriteScoreInBBDD(UserName, CurrentScore, timeController.getFormatTimer());

                //Reset to control UI
                StaticListWeapons.ResetListWeapons();
                StaticExclusionArea.ResetListExclusionArea(); 

                timeController.restartTimer();
                writeBD = false;
            }
        }

        CanWheelTiming();

        if (Input.mouseScrollDelta.y == 1.0f && canScroll) 
        {
            int totalWeapons = ControlWeapons.Instance.TotalWeaponsInPossesion();
            int activeWeapon = ControlWeapons.Instance.WhereIsTheActiveWeapon();
            int nextWeapon = 0;

            if (totalWeapons == activeWeapon) nextWeapon = 1;
            else nextWeapon = activeWeapon + 1;

            if (totalWeapons != 1)
            {
                StaticListWeapons.GetListAllWeapons().ForEach(w =>
                {
                    if (w.IsActive)
                    {
                        nextWeapon = w.NumberThisWeapon + 1;

                        w.IsActive = false;
                        w.gameObject.SetActive(false);
                    }

                    if (w.NumberThisWeapon == nextWeapon)
                    {
                        w.gameObject.SetActive(true);
                        w.IsActive = true;
                        ControlWeapons.Instance.UpdateActiveWeapon(w.NumberThisWeapon);
                    }
                });
            }
        }

        if (Input.mouseScrollDelta.y == -1.0f && canScroll)
        {
            int totalWeapons = ControlWeapons.Instance.TotalWeaponsInPossesion();
            int activeWeapon = ControlWeapons.Instance.WhereIsTheActiveWeapon();
            int nextWeapon = 0;

            if (totalWeapons == activeWeapon) nextWeapon = activeWeapon - 1;
            else nextWeapon = totalWeapons - 1;

            if (nextWeapon == 0) nextWeapon = totalWeapons; 

            if (totalWeapons != 1)
            {
                StaticListWeapons.GetListAllWeapons().ForEach(w =>
                {
                    if (w.IsActive)
                    {
                        nextWeapon = w.NumberThisWeapon - 1;
                        if (nextWeapon == 0) nextWeapon = totalWeapons;

                        w.IsActive = false;
                        w.gameObject.SetActive(false);
                    }

                    if (w.NumberThisWeapon == nextWeapon)
                    {
                        w.gameObject.SetActive(true);
                        w.IsActive = true;
                        ControlWeapons.Instance.UpdateActiveWeapon(w.NumberThisWeapon);
                    }
                });
            }
        }



        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StaticListWeapons.GetListAllWeapons().ForEach(w =>
            {
                if (w.IsInPossesion)
                {
                    //Active Weapon nº1
                    if (w.NumberThisWeapon == 1)
                    {
                        w.gameObject.SetActive(true);
                        w.IsActive = true;
                        ControlWeapons.Instance.UpdateActiveWeapon(w.NumberThisWeapon);
                    }
                    else
                    {
                        //Desactive all weapons
                        w.IsActive = false;
                        w.gameObject.SetActive(false);
                    }
                }
            });


        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Weapons.TotalWeapons > 1)
            {
                StaticListWeapons.GetListAllWeapons().ForEach(w =>
                {
                    if (w.IsInPossesion)
                    {
                        if (w.NumberThisWeapon == 2)
                        {
                            w.gameObject.SetActive(true);
                            w.IsActive = true;
                            ControlWeapons.Instance.UpdateActiveWeapon(w.NumberThisWeapon);
                        }
                        else
                        {
                        //Desactive all weapons
                        w.IsActive = false;
                            w.gameObject.SetActive(false);
                        }
                    }
                });
            }
        }
    }

    private void ChangeLabelTimer() => labelTimer.text = timeController.getFormatTimer(); 

    private void GenerateEnemies() 
    {
            //Generate more enemies
        if (timeController.Minutes >= timeToShowNewEnemies) 
        {
            var minutes = timeController.Minutes;
            
            var randomQuantity = UnityEngine.Random.Range(randMin * minutes, randMax * minutes);
            //generate new enemies and update timeToShowNewEnemies
            EnemyGenerator.Instance.GenerateEnemies(randomQuantity, minutes);

            timeToShowNewEnemies = timeController.Minutes + 1;
       }

    }

    private void ControlRandMinAndMax() 
    {
        if (timeController.Minutes > 4) randMax -= 1;
        else if (timeController.Minutes > 9) randMax -= 1;
        else if (timeController.Minutes > 14) randMax -= 1;
        else if (timeController.Minutes > 19) randMax -= 1;
        else if (timeController.Minutes > 24) randMax -= 1;
        else if (timeController.Minutes > 29) randMax -= 1;
        else if (timeController.Minutes > 34) randMax -= 1;

    }

    /**
     * Method to increase the score.
     * 
     * @param int score to increase
     */
    public void UpScore(int upScor)
    {
        //Todo: Score
        CurrentScore += upScor;
    }

    /**
     * Method to know the current Time (depens of the laberTimer, is not the best form to know). 
     * TODO: I ned change to use timeController to know the time.
     * 
     * @return String The time, format HH:mm
     */
    public string GetCurrentTime() { return labelTimer.text; }

    /**
     * Control of the round label, start the animation, and change the seconds of the start.
     * The time shows and go from 10 to 1 
     */
    private void CallAnimationRoundTimer() 
    {
        if (timeController.Seconds == 48)
        {
            roundLabel.text = $"Wave {timeController.Minutes + 1}";
            roundAnimator.SetBool("visible", true);

        }
        else if (timeController.Seconds >= 50 && timeController.Seconds <= 60)
        {
            roundLabel.text = (60 - timeController.Seconds).ToString();
        }
        else if (timeController.Seconds == 0) 
        {
            roundAnimator.SetBool("visible", false);
        }
    }

    
    /**
     * Method to Show Lable Round and stay 2 seconds visible. 
     * 
     * @return IEnumerator stay 2f seconds.
     */
    private IEnumerator ShowRound(string text) 
    {
        roundLabel.text = text; 
        roundAnimator.SetBool("visible",true);
        yield return new WaitForSeconds(2f);
        roundAnimator.SetBool("visible", false);
    }

    /**
     * Time cooldwon between change weapons with the weel in the mouse. 
     */
    private void CanWheelTiming()
    {
        timePassBewtweenWheels += Time.deltaTime;

        if (timePassBewtweenWheels >= 0.020f)
        {
            canScroll = true;
            timePassBewtweenWheels = 0;
        }
        else
            canScroll = false;
    }
}
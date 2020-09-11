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

    private int randMin = 10;
    private int randMax = 30; 

    //In minutes
    private int timeToShowNewEnemies = 1;

    private Vector2 mouseScroll;
    private bool canScroll = true;
    private float timePassBewtweenWheels;

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

        mouseScroll = new Vector2(0f, 0f);

        timeController = new TimeController();
    }

    private void Start()
    {
        EnemyGenerator.Instance.GenerateEnemies(25,3);
        writeBD = true;
        labelName.text = UserName;
        labelScore.text = "0000";

        StartCoroutine(ShowRound("Wave 0")); 

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

        
        if (Input.GetKeyDown(KeyCode.T))
        {
            timeController.seconds++;
        }
        
        

        if (!CanvasGamerOver.activeSelf)
        {
            ControlRandMinAndMax();

            GenerateEnemies();

            CallAnimationRoundTimer();

            //Player die
            if (playerStats.CurrentHealt <= 0 && writeBD)
            {
                IsNewScore = false;
                timeToShowNewEnemies = 0;

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

    private void GenerateEnemies() 
    {
            //Generate more enemies
        if (timeController.minutes >= timeToShowNewEnemies)
        {
            var minutes = timeController.minutes;
            
            var randomQuantity = UnityEngine.Random.Range(randMin * minutes, randMax * minutes);
            //generate new enemies and update timeToShowNewEnemies

            EnemyGenerator.Instance.GenerateEnemies(randomQuantity, 2 * minutes);

            timeToShowNewEnemies = timeController.minutes + 1;
        }

    }

    private void ControlRandMinAndMax() 
    {
        if (timeController.minutes > 4) randMax -= 1;
        else if (timeController.minutes > 9) randMax -= 1;
        else if (timeController.minutes > 14) randMax -= 1;
        else if (timeController.minutes > 19) randMax -= 1;
        else if (timeController.minutes > 24) randMax -= 1;
        else if (timeController.minutes > 29) randMax -= 1;
        else if (timeController.minutes > 34) randMax -= 1;

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
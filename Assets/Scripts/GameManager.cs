using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : PlayerConf
{
    public TMP_Text labelName;
    public TMP_Text labelScore;

    private int _currentScore;
    private int CurrentScore {
        get { 
            return _currentScore;  
        } 
        set {
            _currentScore = value;
            labelScore.text = Convert.ToString(_currentScore);
        }
    }

    private void Start()
    {
        labelName.text = UserName;
        labelScore.text = "0000";
    }


    public void UpScore(int upScor)
    {
        CurrentScore += upScor;
        Debug.Log("score: " + CurrentScore);
    }

}

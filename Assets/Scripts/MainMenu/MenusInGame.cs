using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusInGame : MonoBehaviour
{
    public TMP_Text finalScore;
    public TMP_Text finalTime;
    public TMP_Text textLabelScore;
    public TMP_Text textScore;
    public TMP_Text timeText; 

    private Color colorStandar = Color.white;
    private Color colorNewScore = Color.yellow;

    private void Start()
    {
        finalScore.text = GameManager.Instance.CurrentScore.ToString();
        finalTime.text = GameManager.Instance.GetCurrentTime();

        ChangeTextScore(GameManager.Instance.IsNewScore);
    }

    public void ReStartGame() => SceneManager.LoadScene(1);

    public void GoToMenu() {
        //Reset to control UI
        StaticListWeapons.ResetListWeapons();
        StaticExclusionArea.ResetListExclusionArea();

        SceneManager.LoadScene(0); 
    }

    private void ChangeTextScore(bool newScore) 
    {
        if (newScore)
        {
            textLabelScore.text = "New Score!  ";
            ChangeColor(colorNewScore);
        }
        else
        {
            textLabelScore.text = "Your Score: ";
            ChangeColor(colorStandar);
        }
    }

    private void ChangeColor(Color color) 
    {
        textLabelScore.color = color;
        textScore.color = color;
        timeText.color = color;
    }

}

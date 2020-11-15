/**
 * Department: Game Developer
 * File: MenusInGame.cs
 * Objective: Control of the menus in game.
 * Employee: Ramón Martínez Nieto
 */
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Control input of the menu in game, the menu in game represents a simple menú with two principal buttons, 
 * restart, menu. Control of the new scorte to.
 * 
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class MenusInGame : MonoBehaviour
{
    /**
     * TMP_Text with the final score
     */
    [Tooltip("Add Label (TMP_Text) with the final score")]
    public TMP_Text finalScore;

    /**
     * TMP_Text with the time (to get the final time)
     */
    [Tooltip("Add Label (TMP_Text) with the time")]
    public TMP_Text finalTime;

    /**
     * Label with the Score (in game) to get the final score and show in TMP_Text finalScore
     */
    [Tooltip("Add Label (TMP_Text) with the time")]
    public TMP_Text textLabelScore;

    /**
     * Label with the Score to ( in game )
     */
    [Tooltip("Add Label (TMP_Text) with the score")]
    public TMP_Text textScore;

    /**
     * 
     */
    [Tooltip("")]
    public TMP_Text timeText; 

    private Color colorStandar = Color.white;
    private Color colorNewScore = Color.yellow;

    private void Start()
    {
        finalScore.text = GameManager.Instance.CurrentScore.ToString();
        finalTime.text = GameManager.Instance.GetCurrentTime();

        ChangeTextScore(GameManager.Instance.IsNewScore);
    }

    /**
     * For the button "restart" game. Recharge scene 1 
     */
    public void ReStartGame() => SceneManager.LoadScene(2);

    /**
     * method to return to the menu.
     * 
     * This method clear the diferents statics arrays. 
     */
    public void GoToMenu() {
        //Reset to control UI
        StaticListWeapons.ResetListWeapons();
        StaticExclusionArea.ResetListExclusionArea();

        SceneManager.LoadScene(1); 
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
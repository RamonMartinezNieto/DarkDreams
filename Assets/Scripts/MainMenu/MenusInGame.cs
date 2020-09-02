using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusInGame : MonoBehaviour
{
    public TMP_Text finalScore;
    public TMP_Text finalTime; 

    private void Start()
    {
        finalScore.text = GameManager.Instance.CurrentScore.ToString();
        finalTime.text = GameManager.Instance.GetCurrentTime();
    }

    public void ReStartGame() => SceneManager.LoadScene(1); 

    public void GoToMenu() => SceneManager.LoadScene(0);



}

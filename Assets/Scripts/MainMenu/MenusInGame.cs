using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusInGame : MonoBehaviour
{
    public TMP_Text finalScore;

    private void Start()
    {
        finalScore.text = GameManager.Instance.CurrentScore.ToString();
    }

    public void ReStartGame() => SceneManager.LoadScene(1); 

    public void GoToMenu() => SceneManager.LoadScene(0);



}

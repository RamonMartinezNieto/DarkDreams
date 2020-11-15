/**
 * Department: Game Developer
 * File: intro.cs
 * Objective: Change scene when intrho finish
 * Employee: Ramón Martínez Nieto
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Class to change between intro and menu 
 */
public class intro : MonoBehaviour
{
    /**
     * Variable with the seconds to stay to play the video
     */
    [Tooltip("Video time in seconds (int)")]
    public float videoTime;

    void Start()
    {
        StartCoroutine(changeScene()); 
    }

    private IEnumerator changeScene() {
        yield return new WaitForSeconds(videoTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

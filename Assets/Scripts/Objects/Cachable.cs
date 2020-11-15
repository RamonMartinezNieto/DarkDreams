/**
 * Department: Game Developer
 * File: Cachable.cs
 * Objective: Abstract class to create differents objects to catch.
 * Employee: Ramón Martínez Nieto
 */
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/**
 * This abstract class is the base to create the different Cachable objetcs, there are control 
 * to set the position, to control the bar timmer, update the bar timmer, and the method to  detroy it. 
 * 
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public abstract class Cachable : MonoBehaviour
{
    private Image circleBar;
    private float timeToDispear;
    private float timePassed = 0f; 

    private void Start()
    {
        circleBar = GetComponentInChildren<Image>();
        circleBar.fillAmount = 1f;

        timeToDispear = Random.Range(2, 15);

        StartCoroutine(DispearCatchable());
    }

    private void Update() => circleBarTimer();
    
    private IEnumerator DispearCatchable() {
        yield return new WaitForSeconds(timeToDispear);

        Destroy(gameObject);
    }

    /**
     * This method call by DropObject to set the new position in the scene.
     */
    public void SetPosition(Vector3 cachablerPosition) =>  transform.position = cachablerPosition;
    
    //Method that reduce circle bar timer.
    private void circleBarTimer()
    {
        timePassed += Time.deltaTime;

        circleBar.fillAmount = (((timeToDispear - timePassed) * 100) / timeToDispear) / 100;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Cachable : MonoBehaviour
{

    private Image circleBar;

    private float timeToDispear;
    private float TotalTimeDuration; 

    private void Start()
    {
        circleBar = GetComponentInChildren<Image>();
        circleBar.fillAmount = 1f;

        timeToDispear = Random.Range(2, 15);
        TotalTimeDuration = Time.time + timeToDispear;

        StartCoroutine(DispearCatchable());
    }

    private void Update()
    {
        circleBarTimer();
    }

    private IEnumerator DispearCatchable() {
        yield return new WaitForSecondsRealtime(timeToDispear);
        Destroy(gameObject);
    }

    //This method call by DropObject
    public void SetPosition(Vector3 cachablerPosition) 
    {
        transform.position = cachablerPosition;
    }


    //Method that reduce circle bar timer.
    private void circleBarTimer()
    {
        float gameTime = Time.time; 

        float restTime = TotalTimeDuration - gameTime;

        circleBar.fillAmount = ((restTime * 100) / timeToDispear) / 100f;
    }
}

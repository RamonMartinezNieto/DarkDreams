using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    //This method call by DropObject
    public void SetPosition(Vector3 cachablerPosition) =>  transform.position = cachablerPosition;
    
    //Method that reduce circle bar timer.
    private void circleBarTimer()
    {
        timePassed += Time.deltaTime;

        circleBar.fillAmount = (((timeToDispear - timePassed) * 100) / timeToDispear) / 100;
    }
}
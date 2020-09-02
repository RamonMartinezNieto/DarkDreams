using UnityEngine;

public class TimeController
{
    
    private int seconds = 0;
    public int minutes { get; private set; } = 0;
    public float currentTimer { get; private set; } = 0;

    public void updateTime()
    {
        currentTimer += Time.deltaTime;

        if (currentTimer >= 1) { 
            seconds++;
            currentTimer = 0; 
        }
        if (seconds == 60) 
        {
            minutes++;
            seconds = 0;
        }
    }

    public string getFormatTimer()
    {
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void restartTimer()
    {
        seconds = 0;
        minutes = 0;
        currentTimer = 0;
    }
}
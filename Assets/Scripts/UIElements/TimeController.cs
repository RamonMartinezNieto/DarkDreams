using UnityEngine;

public class TimeController
{
    
    public int seconds { get; set; } = 0;
    public int minutes { get; private set; } = 0;

    public float currentTimer { get; private set; } = 0;


    public int secondsDown { get; private set; } = 59;
    public int minutesDown { get; private set; } = 0;

    public bool restartTimerColdDown { get; set;  } = true; 


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

    public void StartTimerDown() 
    {
        minutesDown = minutes-1;
        secondsDown = 59;
    }

    public string GetFormatTimerDown() 
    {
        if (restartTimerColdDown) {
            StartTimerDown();
            restartTimerColdDown = false; 
        }

        secondsDown = 60 - seconds;

        minutesDown = (minutes*2) - minutes; 

        return string.Format("{0:00}:{1:00}", minutesDown, secondsDown);
    }

    public bool GetFinishTime() 
    {
        if (secondsDown <= 0 && minutesDown <= 0 ) return true;
        else return false; 
    }




    public void restartTimer()
    {
        seconds = 0;
        minutes = 0;
        currentTimer = 0;
    }
}
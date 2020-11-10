using UnityEngine;
using UnityEngine.Events;

public class TimeController : MonoBehaviour
{
    /**
     * Delegate to create a new event when the minutes change  
     */
    public delegate void TimeMinutesChange();
    /**
     * Event to suscribe  
     */
    public static event TimeMinutesChange OnMinutesChanged;
    /**
     * Delegate to create a new event when the seconds changed
     */
    public delegate void TimeSecondsChange();
    /**
     * Event to suscribe 
     */
    public static event TimeMinutesChange OnSecondsChanged;

    private int _seconds;
    public int Seconds
    {
        get 
        {
            return _seconds; 
        }
        set 
        {
            this._seconds = value;
            //Launch event
            if (OnSecondsChanged != null)
                OnSecondsChanged();
        } 
    }

    private int _minutes; 
    public int Minutes {
        get 
        {
            return _minutes;
        } 
        private set 
        {
            _minutes = value;

            //Launch event
            if (OnMinutesChanged != null)
            {
                OnMinutesChanged();
            }
        } 
    }

    public float currentTimer { get; private set; } = 0;

    private void FixedUpdate()
    {
        updateTime();
    }

    public void updateTime()
    {
        currentTimer += Time.deltaTime;

        if (currentTimer >= 1) {
            Seconds++;
            currentTimer = 0; 
        }
        if (Seconds == 60) 
        {
            Minutes++;
            Seconds = 0;
        }
    }

    public string getFormatTimer()
    {
        return string.Format("{0:00}:{1:00}", Minutes, Seconds);
    }

    public void restartTimer()
    {
        Seconds = 0;
        Minutes = 0;
        currentTimer = 0;
    }

}
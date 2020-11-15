/**
 * Department: Game Developer
 * File: TimeController.cs
 * Objective: Create a custom timmer to launch events every second and every minute.
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;

/**
 * This class make a timmer and events to launch when the sconds or the minutes change it value. 
 * 
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
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
    /**
     * New second
     */
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
    /**
     * New minute
     */
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

    /**
     * Get current timer (public) the setter is private because it control with the specific setters and only this class
     * can change it. 
     */
    public float currentTimer { get; private set; } = 0;

    private void FixedUpdate()
    {
        updateTime();
    }

    
    private void updateTime()
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

    /**
     * Getter the complete time with the format MM:SS
     */
    public string getFormatTimer()
    {
        return string.Format("{0:00}:{1:00}", Minutes, Seconds);
    }

    /**
     * Reset the timer to create a new game.
     */
    public void restartTimer()
    {
        Seconds = 0;
        Minutes = 0;
        currentTimer = 0;
    }
}
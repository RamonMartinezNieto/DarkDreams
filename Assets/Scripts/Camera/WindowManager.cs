using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Create Event to check when screen change size
public class WindowManager : MonoBehaviour
{
    
    //Singleton
    public static WindowManager Instance = null;

    //Last saved size
    private Vector2 lastScreenSize;


    //  Delgate for the event
    public delegate void ScreenSizeChangeEventHandler(int Width, int Height);
    //  Event
    public event ScreenSizeChangeEventHandler ScreenSizeChangeEvent;           
    
    protected virtual void OnScreenSizeChange(int Width, int Height)
    {   //  Define Function trigger and protect the event for not null;
        if (ScreenSizeChangeEvent != null) ScreenSizeChangeEvent(Width, Height);
    }

    void Awake()
    {
        //Save size
        lastScreenSize = new Vector2(Screen.width, Screen.height);

        // Singleton
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    void Update()
    {
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        if (this.lastScreenSize != screenSize)
        {
            this.lastScreenSize = screenSize;
            //  Launch the event when the screen size change
            OnScreenSizeChange(Screen.width, Screen.height);
        }
    }
}
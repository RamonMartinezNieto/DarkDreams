using System;
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



    [Obsolete("Uses CanvasScaler component instead of this method")]
    private void SetSizeBars(int Width, int Height)
    {
        Vector3 newScale = new Vector3(1f, 1f, 0);
        float posY = -110f;

        if (Width < 480 || Height < 640)
        {
            newScale = new Vector3(.5f, .5f, 0);
            posY = -80f;
        }
        else if (Width < 600 || Height < 800)
        {
            newScale = new Vector3(.75f, .75f, 0);
            posY = -90f;
        }
        else
        {
            newScale = new Vector3(1f, 1f, 0);
        }

        //barsTransform.anchoredPosition = new Vector2(0f, posY);
        //barsTransform.localScale = newScale;
    }


    //Example To call this event: 
    //WindowManager.Instance.ScreenSizeChangeEvent += Instance_ScreenSizeChangeEvent;
    /*private void Instance_ScreenSizeChangeEvent(int Width, int Height)
    {
    }*/
}
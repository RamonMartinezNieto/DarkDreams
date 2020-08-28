using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerConf : MonoBehaviour
{
    //Settings
    private string _userName;
    private float _musicVolumen;
    private int _language;
    private string _crossHair;
    private bool _musicOn;
    private int _score; 

    public bool MusicOn
    {
        get
        {
            string music = PlayerPrefs.GetString("musicOn");
            if (music.Equals("on")) _musicOn = true;
            else if (music.Equals("off")) _musicOn = false;

            return _musicOn;
        }
        set
        {
            _musicOn = value;

            string musicOnString;
            if (value) musicOnString = "on";
            else musicOnString = "off";

            SoundManager.Instance.MuteAllSounds(!_musicOn);
            PlayerPrefs.SetString("musicOn", musicOnString);
        }
    }

    public float MusicVolumen
    {
        get
        {
            _musicVolumen = PlayerPrefs.GetFloat("musicVolumen");
            return _musicVolumen;
        }
        set
        {
            _musicVolumen = value;
            SoundManager.Instance.ChangeVolumen(_musicVolumen);
            PlayerPrefs.SetFloat("musicVolumen", value);
        }
    }

    public int LanguageInt
    {
        //0 - English, 1-Spanish, 2-Catalan
        get
        {
            _language = PlayerPrefs.GetInt("language");
            return _language;
        }
        set
        {
            PlayerPrefs.SetInt("language", value);
            _language = value;
        }
    }

    public string GetLanguageString()
    {
        string langauageString;

        switch (LanguageInt)
        {
            case 0:
                langauageString = "english";
                break;
            case 1:
                langauageString = "spanish";
                break;
            case 2:
                langauageString = "catalan";
                break;
            default:
                langauageString = "english";
                break;
        }
        return langauageString;
    }

    public string CrossHairString
    {
        get
        {
            _crossHair = PlayerPrefs.GetString("crossHair");
            return _crossHair;
        }
        set
        {
            _crossHair = value;
            PlayerPrefs.SetString("crossHair", value);
        }
    }


    public string UserName
    {
        get
        {
            _userName = PlayerPrefs.GetString("userName");
            return _userName;
        }
        set
        {
            _userName = value;
            PlayerPrefs.SetString("userName", value);
        }
    }


    public int Score
    {
        get
        {
            _score = PlayerPrefs.GetInt("score");
            return _score;
        }
        set
        {
            _score = value;
            PlayerPrefs.SetInt("score", value);
        }
    }

}

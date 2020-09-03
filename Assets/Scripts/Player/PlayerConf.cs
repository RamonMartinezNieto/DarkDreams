using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerConf : MonoBehaviour
{
    //Settings
    private string _userName;
    private float _musicVolumen;
    private bool _musicOn;
    private float _musicEffectVolumen;
    private bool _musicEffectOn;
    private int _language;
    private string _crossHair;
    private int _betterScore;
    private string _timeBetterScore;

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

            SoundManager.Instance.MuteMusic(!_musicOn);
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
            SoundManager.Instance.ChangeVolumenMusic(_musicVolumen);
            PlayerPrefs.SetFloat("musicVolumen", value);
        }
    }
    public bool MusicEffectOn
    {
        get
        {
            string musicEffect = PlayerPrefs.GetString("musicEffectOn");
            if (musicEffect.Equals("on")) _musicEffectOn = true;
            else if (musicEffect.Equals("off")) _musicEffectOn = false;

            return _musicEffectOn;
        }
        set
        {
            _musicEffectOn = value;

            string musicEffectOnString;
            if (value) musicEffectOnString = "on";
            else musicEffectOnString = "off";


            SoundManager.Instance.MuteEffectsSounds(!_musicEffectOn);

            PlayerPrefs.SetString("musicEffectOn", musicEffectOnString);
        }
    }

    public float MusicEffectVolumen
    {
        get
        {
            _musicEffectVolumen = PlayerPrefs.GetFloat("musicEffectVolumen");
            return _musicEffectVolumen;
        }
        set
        {
            _musicEffectVolumen = value;
            SoundManager.Instance.ChangeVolumenEffects(_musicEffectVolumen);
            PlayerPrefs.SetFloat("musicEffectVolumen", value);
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


    private int BetterScore
    {
        get
        {
            _betterScore = PlayerPrefs.GetInt("betterScore");
            return _betterScore;
        }
        set
        {
            _betterScore = value;
            PlayerPrefs.SetInt("betterScore", value);
        }
    }

    private string TimeBetterScore
    {
        get
        {
            _timeBetterScore = PlayerPrefs.GetString("timeBetterScore");
            return _timeBetterScore;
        }
        set
        {
            _timeBetterScore = value;
            PlayerPrefs.SetString("timeBetterScore", value);
        }
    }


    protected void SaveScoreAndTime(int currentScore, string currentTime) 
    {
        //Only save score with time when the score is better than the last score
        if (currentScore > BetterScore) 
        {
            BetterScore = currentScore;
            TimeBetterScore = currentTime;
        } 
    }

}

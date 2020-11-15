/**
 * Department: Game Developer
 * File: PlayerConf.cs
 * Objective: Control player's configuration using PlayerPrefs
 * Employee: Ramón Martínez Nieto
 */ 
using UnityEngine;

/**
 * Abstract class to set the player's configurations. 
 */
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

    /**
     * Set & Get the music. On/Off
     * Save in the PLayerPrefs "musicOn"
     * 
     * @return bool On/Off
     * @param bool On/Off
     */
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

    /**
     * Set & Get the music's volumen.
     * Save in the PLayerPrefs "musicVolumen"
     * 
     * @return bool float 0.0f - 1.0f
     * @param bool float 0.0f - 1.0f
     */
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
            SoundManager.Instance.ChangeVolumenMusic();
            PlayerPrefs.SetFloat("musicVolumen", value);
        }
    }

    /**
     * Set & Get the music effects. On/Off
     * Save in the PLayerPrefs "musicEffectOn"
     * 
     * @return bool On/Off
     * @param bool On/Off
     */
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

    /**
     * Set & Get the music effect's volumen.
     * Save in the PLayerPrefs "musicEffectVolumen"
     * 
     * @return bool float 0.0f - 1.0f
     * @param bool float 0.0f - 1.0f
     */
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
            SoundManager.Instance.ChangeVolumenEffects();
            PlayerPrefs.SetFloat("musicEffectVolumen", value);
        }
    }

    /**
     * Set & Get the Language selected.
     * Save in the PLayerPrefs "language"
     * 0 - English, 1-Spanish, 2-Catalan
     * 
     * @return int
     * @param int
     */
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

    /**
     * To get String language. 
     */
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

    /**
     * Set & Get the string with the name of the crosshair saved.
     * Save in the PLayerPrefs "crossHair"
     * 
     * @return String name's crosshair
     * @param String name's crosshair
     * 
     */
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

    /**
     * Set & get player's name. 
     * Save in the PLayerPrefs "userName"
     * 
     * @return String player's name
     * @param String player's name
     */
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

    /**
     * Set & Get the better score of the player.
     * Save in the PLayerPrefs "betterScore"
     * 
     * @return int Score
     * @param int Score
     * 
     */
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

    /**
     * Set & Get the better time of score of the player.
     * Save in the PLayerPrefs "timeBetterScore"
     * 
     * @return string Score
     * @param string Score
     * 
     */
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

    /**
     * Protected method to save the score and time at the same time. 
     * 
     * @param currentScore int
     * @param currentTime string
     */
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

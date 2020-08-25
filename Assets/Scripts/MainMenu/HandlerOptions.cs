using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HandlerOptions : MonoBehaviour
{

    public Toggle musicToggle;
    public Slider musicVolumenSlider;
    public TMP_Dropdown languageDropDown;

    public ToggleGroup crossHairToggleGroup;
    public Toggle crossWhite;
    public Toggle crossRed;
    public Toggle crossBlue;

    public TMP_InputField userNameInputField;

    //Settings
    private string _userName;
    private float _musicVolumen;
    private int _language;
    private string _crossHair;
    private bool _musicOn;


    private void Start()
    {
        //First time, to create PlayerPrefs
        if (PlayerPrefs.GetInt("FIRSTIMEOPENING", 1) == 1)
        {
            MusicOn = true;
            musicToggle.isOn = MusicOn;

            musicVolumenSlider.value = MusicVolumen = 1f;

            LanguageInt = languageDropDown.value = 2;

            CrossHairString = crossHairToggleGroup.ActiveToggles().FirstOrDefault().name;

            //TODO: Need to get UserName with a extra square box 
            userNameInputField.text = UserName; 

            PlayerPrefs.SetInt("FIRSTIMEOPENING", 0);
        }
        else
        {
            musicToggle.isOn = MusicOn;
            musicVolumenSlider.value = MusicVolumen;
            languageDropDown.value = LanguageInt;

            crossHairToggleGroup.allowSwitchOff = true;
            GetCurrentCrossHairToggle().isOn = true;
            
            userNameInputField.text = UserName;
        }
    }

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

    //Need to add this method in a toggle to change String in PlayerPrefs
    public void SetCrossHairActive() 
    {
        IEnumerable<Toggle> listTog = crossHairToggleGroup.ActiveToggles();

        foreach (Toggle t in listTog)  CrossHairString = t.name;
    }

    //Return current active CrossHair
    private Toggle GetCurrentCrossHairToggle()
    {
        switch (CrossHairString) 
        {
            case "blue":
                return crossBlue;
            case "red":
                return crossRed;
            case "white":
                return crossWhite;
            default:
                return crossWhite; 
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

    public void ChangeUserName() 
    {
        //Todo: need confirmation Box 
        UserName = userNameInputField.text; 
    }
}
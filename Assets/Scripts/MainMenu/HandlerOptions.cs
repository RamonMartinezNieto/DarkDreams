using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HandlerOptions : PlayerConf
{
    public static HandlerOptions Instance = null;

    public Toggle musicToggle;
    public Slider musicVolumenSlider;
    public TMP_Dropdown languageDropDown;

    public ToggleGroup crossHairToggleGroup;
    public Toggle crossWhite;
    public Toggle crossRed;
    public Toggle crossBlue;

    public TMP_InputField userNameInputField;

    public TMP_Text labelNameOptions;

    void Awake()
    {
        singletonInstance();
    }

    
    private void singletonInstance()
    {
        //Singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //Dont destroy on load to persevere this object in all scenes
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ChargePanel();
    }

    public void ChargePanel()
    {
        musicToggle.isOn = MusicOn;
        musicVolumenSlider.value = MusicVolumen;

        if (languageDropDown != null) languageDropDown.value = LanguageInt;

        crossHairToggleGroup.allowSwitchOff = true;
        GetCurrentCrossHairToggle().isOn = true;

        if(userNameInputField != null) userNameInputField.text = UserName;
    }


    //TODO: WTF? Need to add this method in a toggle to change String in PlayerPrefs
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

    public void ChangeUserName() 
    {
        //Todo: need confirmation Box 
        UserName = userNameInputField.text;
    }

}
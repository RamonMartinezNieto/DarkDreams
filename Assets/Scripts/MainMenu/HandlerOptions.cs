/**
 * Department: Game Developer
 * File: HandlerOptions.cs
 * Objective: Class to control the change of the different values (they are modified by the configuration menu and others)
 * Employee: Ramón Martínez Nieto
 */
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

/**
 * HandlerOptions has a single instance to use the different methods to change the settings.
 * Use it when you want change the settings, for example in a configuration menu, this handlerOptions
 * is addapted to the current configuration menu, if you want change more efficiently the settings use 
 * PlayerConf instead.
 * 
 * Inherit PlayerConf because use the methods of the playerConf
 * 
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 * @see PlayerConf 
 */
public class HandlerOptions : PlayerConf
{

    /**
     * Single Instance of the HandlerOptions using a Singleton pattern 
     */
    public static HandlerOptions Instance = null;

    /**
     * Toggle to activate o desactivate the music
     */
    [Tooltip("Add toggle to activate/desactivate the music")]
    public Toggle musicToggle;

    /**
     * Slider to change the volumen of the music
     */
    [Tooltip("Add Slider to change the volumen of the music")]
    public Slider musicVolumenSlider;

    /**
     * Toggle to activate o desactivate the effects (shoots and others)
     */
    [Tooltip("Add toggle to activate/desactivate effects")]
    public Toggle musicEffectToggle;

    /**
     * Slider to change the volumen of the effects
     */
    [Tooltip("Add Slider to change the volumen of the effects")]
    public Slider musicEffectSlider;

    /**
     * DropBox to choice language
     */
    [Tooltip("Add DropBox to choice language")]
    public TMP_Dropdown languageDropDown;

    /**
     * Group of toggles to choice one CrossHair
     */
    [Tooltip("Add Toggle group of CrossHairs")]
    public ToggleGroup crossHairToggleGroup;

    /**
     * Toggle with the white crosshair
     */
    [Tooltip("Add toggle of the white crosshair")]
    public Toggle crossWhite;

    /**
     * Toggle with the red crosshair
     */
    [Tooltip("Add toggle of the red crosshair")]
    public Toggle crossRed;

    /**
     * Toggle with the blue crosshair
     */
    [Tooltip("Add toggle of the blue crosshair")]
    public Toggle crossBlue;

    /**
     * InputFiled where the user enter the name (in configuration menu)
     */
    [Tooltip("Add input field on the user enter the name (configuration menu)")]
    public TMP_InputField userNameInputField;

    /**
     * Label to show to the user (in a box confirmation)
     */
    [Tooltip("Add Label to show to the user (in a box confirmation)")]
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

    /**
     * Method to charge the panel with the config that the user have. 
     * User allways have a basic configuration.
     */
    public void ChargePanel()
    {
        musicToggle.isOn = MusicOn;
        musicVolumenSlider.value = MusicVolumen;

        musicEffectToggle.isOn = MusicEffectOn ;
        musicEffectSlider.value = MusicEffectVolumen; 

        if (languageDropDown != null) languageDropDown.value = LanguageInt;

        crossHairToggleGroup.allowSwitchOff = true;
        GetCurrentCrossHairToggle().isOn = true;

        if(userNameInputField != null) userNameInputField.text = UserName;
    }

    /**
     * Establish the active crosshair
     */
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

    /**
     * Method to change the user name
     */
    public void ChangeUserName() 
    {
        //Todo: need confirmation Box 
        UserName = userNameInputField.text;
    }

}
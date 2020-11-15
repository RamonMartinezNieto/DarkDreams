/**
 * Department: Game Developer
 * File: UIHealthBar.cs
 * Objective: Specification of the UIBar to create a control of the UIHealthBar
 * Employee: Ramón Martínez Nieto
 */
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/**
 * 
 * This class is a especific class to control the HealthBar, override the methods that it need.
 * 
 * @see UIBar
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class UIHealthBar : UIBar
{
    /**
     * Slider of the Healthbar
     */
    [Tooltip("Add Slider of the Health Bar")]
    public Slider HealthBar;

    /**
     * Text in the up of the health bar
     */
    [Tooltip("Add text up to bar of the Health Bar")]
    public TMP_Text textHelath; 

    private void Start()
    {
        SetSize(HealthBar, playerStats.CurrentHealt / 100f);
        SetValueInt(HealthBar.value);
    }

    /**
     * Method to change the value of the bar 
     * 
     * @see UIBar#SetValueInt
     */
    public override void SetValueInt(float value) => textHelath.text = Convert.ToInt32((value * 100)).ToString();

    /**
     * Method to change the color of the bar. Deppends of the size.
     */
    public override void ChangeColor(float sizeNorm)
    {
        Color green = new Color(1f, 1f, 1f, 1f);
        Color darkGreen = new Color(.5f, .5f, .5f, 1f);
        Color red = new Color(1f, 0f, 0f, 1f);
        Color darkRed = new Color(.7f, 0f, 0f, 1f);

        if (sizeNorm >= 1.0f)
        {
            SetColor("HEndBar", green);
        }
        else if (sizeNorm > 0.3f && sizeNorm < 1f)
        {
            SetColor("HEndBar", darkGreen);
            SetColor("HFill", green);
            SetColor("HStartBar", green);
            SetColor("HBackground", darkGreen);
        }
        else if (sizeNorm <= 0.3f && sizeNorm > .0f)
        {
            SetColor("HEndBar", darkRed);
            SetColor("HStartBar", red);
            SetColor("HFill", red);
            SetColor("HBackground", darkRed);
        }
        else if (sizeNorm <= 0.0f)
        {
            SetColor("HEndBar", darkRed);
            SetColor("HStartBar", darkRed);
        }
    }
}

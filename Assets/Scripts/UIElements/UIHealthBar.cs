﻿
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : UIBar
{
    public Slider HealthBar;
    public TMP_Text textHelath; 

    private void Start()
    {
        SetSize(HealthBar, playerStats.CurrentHealt / 100f);
        SetValueInt(HealthBar.value);
    }

    public override void SetValueInt(float value) => textHelath.text = Convert.ToInt32((value * 100)).ToString();

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

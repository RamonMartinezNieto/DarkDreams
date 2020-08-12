using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class UIBar : MonoBehaviour
{

    public PlayerStats playerStats;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    public void SetSize(Slider slider, float sizeNorm)
    {
        slider.value = sizeNorm;
        ChangeColor(sizeNorm);
    }

    public float GetSize(Slider slider) { return slider.value; }

    public void SetColor(string bar, UnityEngine.Color color) => GameObject.Find(bar).GetComponent<Image>().color = color;

    public virtual void ChangeColor(float sizeNorm)
    {
        //Original colors to armor 
        Color blue = new Color(1f, 1f, 1f, 1f);
        Color darkBlue = new Color(0.5f, 0.5f, 0.5f, 1f);

        if (sizeNorm >= 1.0f)
        {
            SetColor("AEndBar", blue);
            SetColor("AStartBar", blue);
        }
        else if (sizeNorm <= 0.0f)
        {
            SetColor("AEndBar", darkBlue);
            SetColor("AStartBar", darkBlue);
        }
        else
        {
            SetColor("AEndBar", darkBlue);
            SetColor("AStartBar", blue);
        }
    }
}

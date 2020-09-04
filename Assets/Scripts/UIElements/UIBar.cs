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


    public void SetColor(string bar, UnityEngine.Color color) => GameObject.Find(bar).GetComponent<Image>().color = color;

    public abstract void ChangeColor(float sizeNorm);

    public abstract void SetValueInt(float value);

}


using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIArmorBar : UIBar
{
    public Slider ArmorBar;
    public TMP_Text textArmor;

    public static readonly float DAMAGE_REDUCE = 80;

    void Start()
    {       
        SetSize(ArmorBar, playerStats.CurrentArmor);
        SetValueInt(ArmorBar.value);
    }

    public override void SetValueInt(float value) => textArmor.text = Convert.ToInt32((value * 100)).ToString();

    public override void ChangeColor(float sizeNorm)
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

    //Armor bar get 80% of the enemy  damage
    public float GetDamageRestArmorEffect(int damage)
    {
        //return the damage absorving with armor
        float damageReturn = (float)damage;
        //Size of the current armorBar
        float currentArmor = playerStats.CurrentArmor * 100f;
        
        if (currentArmor > 0)
        {
            //Armor absorv DamageReduce% of the damage 
            damageReturn = damage - (damage * (DAMAGE_REDUCE / 100f));

            //Set currentArmor using PlayerStats method 
            playerStats.restArmor((damage - damageReturn) / 100F);

        }
        else if (currentArmor <= 0)
        {
            SetSize(ArmorBar, 0f);
            return damageReturn;
        }

        return damageReturn;
    }
}

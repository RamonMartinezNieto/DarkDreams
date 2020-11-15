/**
 * Department: Game Developer
 * File: UIArmorBar.cs
 * Objective: Specification of the UIBar to create a control of the UIArmorBar
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/**
 * This class is a especific class to control the ArmorBar, override the methods that it need.
 * 
 * @see UIBar
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class UIArmorBar : UIBar
{
    /**
     * Slider of the ArmorBar
     */
    [Tooltip("Add Slider of the Armor Bar")]
    public Slider ArmorBar;

    /**
     * Text in the up of the bar
     */
    [Tooltip("Add text up to bar of the Armor Bar")]
    public TMP_Text textArmor;

    /**
     * Variable that define the cuantity of the armor absorb 
     */
    public static readonly float DAMAGE_REDUCE = 80;

    void Start()
    {       
        SetSize(ArmorBar, playerStats.CurrentArmor);
        SetValueInt(ArmorBar.value);
    }

    /**
     * Method to change the value of the bar 
     * 
     * @see UIBar#SetValueInt
     */
    public override void SetValueInt(float value) => textArmor.text = Convert.ToInt32((value * 100)).ToString();

    /**
     * Method to change the color of the bar. Deppends of the size.
     */
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

    /**
      * Armor bar get 80% of the enemy  damage
      * 
      * @see DAMAGE_REDUCE
      */
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

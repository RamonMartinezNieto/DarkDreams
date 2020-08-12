
using UnityEngine;
using UnityEngine.UI;


public class UIArmorBar : UIBar
{
    public Slider ArmorBar;
    
    public static readonly float DAMAGE_REDUCE = 80;

    void Start()
    {       
        SetSize(ArmorBar, playerStats.CurrentArmor);
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

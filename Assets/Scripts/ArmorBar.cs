using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class is obsolete because is remplaced by UIArmorBar

[System.Obsolete("This class is obsolete, remplaced by UIArmorBar", true)]
public class ArmorBar : MonoBehaviour
{
    private Transform childArmorBar;
    private PlayerStats playerStats; 
    public static readonly float DAMAGE_REDUCE = 80;


    private void Start()
    {
        childArmorBar = transform.Find("Bar");
        playerStats = gameObject.GetComponentInParent<PlayerStats>();
        
        //Set initial size by PlayerStats Armor
        SetSize(playerStats.CurrentArmor);

    }

    public void SetSize(float sizeNorm)=> childArmorBar.localScale = new Vector3(sizeNorm, 1f);
    
    public Vector3 GetSize() { return childArmorBar.localScale; }
    
    public void SetColor(Color color) => childArmorBar.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    

    //Armor bar get 80% of the enemy  damage
    public float GetDamageRestArmorEffect(int damage)   //10
    {
        //return the damage absorving with armor
        float damageReturn = (float)damage;   //10f 

        //Size of the current armorBar
        float currentArmor = playerStats.CurrentArmor * 100f; //50
        
        if (currentArmor > 0)
        {
            //Armor absorv DamageReduce% of the damage 
            damageReturn = damage - (damage * (DAMAGE_REDUCE / 100f)); // 2
            
            //Set currentArmor using PlayerStats method 
            playerStats.restArmor((damage-damageReturn)/100F); 
            
        }
        else if (currentArmor <= 0)
        {
            SetSize(0f);
            return damageReturn;
        }

        return damageReturn;
    }

}

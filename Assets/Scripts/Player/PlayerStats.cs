using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ArmorBar armorBar;

    private int currentHealt = 100;
    public int CurrentHealt
    {
        get
        {
            return currentHealt;
        }
        private set
        {
            currentHealt = value; 

            if(currentHealt > 100){
                currentHealt = 100; 
            } else if(currentHealt <= 0){
                PlayerDie();
            }
        }
    }

    private float _currentArmor = .8f; 
    public float CurrentArmor
    {
        get
        {
            return _currentArmor;
        }
        private set
        {
            _currentArmor = value; 
            
            if(_currentArmor >= 1f){
                _currentArmor = 1f; 
            } else if(_currentArmor <= 0f){
                _currentArmor = 0f; 
            }

            //Update armor bar when currentArmor change (increase).
            armorBar.SetSize(_currentArmor);
        }
    }
   
    public void restHealth(int damage)
    {
        //Check if t he player have armor to absorve damage
        float restHealt = armorBar.GetDamageRestArmorEffect(damage); 
        CurrentHealt -= (int) restHealt; 

        float Health = CurrentHealt / 100f;
        healthBar.SetSize(Health);

        if (Health < .3f)
        {
            healthBar.SetColor(Color.red);
        }
    }

    public void sumHealth(int health)
    {
        CurrentHealt += health; 

        float Health = CurrentHealt / 100f;
        healthBar.SetSize(Health);

    }

    public void restArmor(float armorDecrease) => CurrentArmor -= armorDecrease; 
    
    public void sumArmor(float armorIncrease) => CurrentArmor += armorIncrease; 

    private void PlayerDie() =>   GameObject.FindGameObjectWithTag("Player").SetActive(false);
    
   
}

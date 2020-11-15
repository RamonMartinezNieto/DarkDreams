/**
 * Department: Game Developer
 * File: PlayerStats.cs
 * Objective: Control healt and armor of the player and update UI. 
 * Employee: Ramón Martínez Nieto
 */
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/**
 * Class to keep and change all stadistics of the player. 
 * Healt and armor. 
 * 
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class PlayerStats : MonoBehaviour
{
    /**
     * Healt Bar in the UI  
     */
    [Tooltip("Put UI's Healt Bar.")]
    [SerializeField] public UIHealthBar healthBar;

    /**
     * Armor Bar in the UI  
     */
    [Tooltip("Put UI's Armor Bar.")]
    [SerializeField] public UIArmorBar armorBar;

    /**
     * Slider healt Bar in the UI  
     */
    [Tooltip("Put UI's slider healt Bar.")]
    public Slider sliderHealtBar;
    
    /**
     * Slider armor Bar in the UI  
     */
    [Tooltip("Put UI's slider armor Bar.")]
    public Slider sliderArmorBar;

    /**
     * SpriteReneder of the player (using to change color when recive damage) 
     */
    [Tooltip("Put player's SpriteRenderer.")]
    public SpriteRenderer spriteRendererPlayer;

    private int currentHealt = 100;
    /**
     * Player's current healt.  
     * 
     * @param int Healt
     * @retunr int Current Healt
     */
    public int CurrentHealt
    {
        get
        {
            return currentHealt;
        }
        private set
        {
            currentHealt = value; 
            
            //TODO MODE GOOD: 
            //currentHealt = 100;

            if (currentHealt >= 100){
                currentHealt = 100; 
            } else if(currentHealt <= 0){
                PlayerDie();
            }
            
            healthBar.SetSize(sliderHealtBar, currentHealt/100f);
        }
    }

    private float _currentArmor = .5f;
    /**
     * Player's current armor.  
     * 
     * @param float armor
     * @retunr float Current armor
     */
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
            armorBar.SetSize(sliderArmorBar,_currentArmor);
        }
    }

    /**
     * Method to subtract life from the player.
     * 
     * @param int damage to subtract
     */
    public void restHealth(int damage)
    {
        //TODO: 
        //Check if t he player have armor to absorve damage
        float restHealt = armorBar.GetDamageRestArmorEffect(damage); 
        CurrentHealt -= (int) restHealt;

        if(CurrentHealt > 0)
            StartCoroutine(effectDamage());

        float Health = CurrentHealt / 100f;
    }

    /**
     * Method to increase healt to the player.
     * 
     * @param int healt to increase
     */
    public void sumHealth(int health)
    {
        CurrentHealt += health; 

        float Health = CurrentHealt / 100f;
    }

    private IEnumerator effectDamage() 
    {
        spriteRendererPlayer.color = new Color(1f,.5f,.5f);

        yield return new WaitForSeconds(0.1f);

        spriteRendererPlayer.color = Color.white;
    }

    /**
     * Method to subtract armor from the player.
     * 
     * @param float armor to subtract
     */
    public void restArmor(float armorDecrease) => CurrentArmor -= armorDecrease;

    /**
     * Method to increase armor to the player.
     * 
     * @param float armor to increase
     */
    public void sumArmor(float armorIncrease) => CurrentArmor += armorIncrease;

    /**
     * Method to kill the player 
     */
    private void PlayerDie() =>  gameObject.SetActive(false); 
    
}
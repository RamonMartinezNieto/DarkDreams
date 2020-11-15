/**
 * Department: Game Developer
 * File: CachableSield.cs
 * Objective: Specific class of Cachable to create a cachable shield.
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;

/**
 * This class provide a specific methods to control a cachable to increase armor.
 * 
 * @see Cachable
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class CachableShield : Cachable
{
    /**
     * Variable to change in Unity the value the amount of shield to increase.
     */
    [Tooltip("Quantity of shield that will increase this shield. .25f by default (check float number)")]
    public float quantityOfShieldIncrease = 0.25f;

    /**
     * Specific OnTriggerEnter2D to increase armor
     */
    void OnTriggerEnter2D(Collider2D other){
        
        if(other.gameObject.tag.Equals("Player")) {
            gameObject.SetActive(false); 
            other.GetComponentInParent<PlayerStats>().sumArmor(quantityOfShieldIncrease);
        }
    }
}

/**
 * Department: Game Developer
 * File: CachableHeart.cs
 * Objective: Specific class of Cachable to create a cachable heart.
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;

/**
 * This class provide a specific methods to control a cachable to increase health.
 * 
 * @see Cachable
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class CachableHeart : Cachable
{
    /**
     * Variable to change in Unity the value the amount of health to increase.
     */
    [Tooltip("Quantity of health that will increase this heart. 10 by default")]
    public int quantityOfHealth = 10;

    void OnTriggerEnter2D(Collider2D other){
        
        if(other.gameObject.tag.Equals("Player")) {
            gameObject.SetActive(false); 
            other.GetComponentInParent<PlayerStats>().sumHealth(quantityOfHealth);
        }
    }
}

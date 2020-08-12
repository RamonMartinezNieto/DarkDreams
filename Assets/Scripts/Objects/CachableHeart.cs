using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CachableHeart : Cachable
{
    [Tooltip("Quantity of health that will increase this heart. 10 by default")]
    public int quantityOfHealth = 10;

    void OnTriggerEnter2D(Collider2D other){
        
        if(other.gameObject.tag.Equals("Player")) {
            gameObject.SetActive(false); 
            other.GetComponentInParent<PlayerStats>().sumHealth(quantityOfHealth);
        }
    }
}

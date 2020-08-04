using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Tooltip("Quantity of shield that will increase this shield. .25f by default (check float number)")]
    public float quantityOfShieldIncrease = 0.25f;

    void OnTriggerEnter2D(Collider2D other){
        
        if(other.gameObject.tag.Equals("Player")) {
            gameObject.SetActive(false); 
            other.GetComponentInParent<PlayerStats>().sumArmor(quantityOfShieldIncrease);
        }
    }
}

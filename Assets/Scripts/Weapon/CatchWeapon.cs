using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchWeapon : MonoBehaviour
{
    public GameObject principalWeapon;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Player")){
            principalWeapon.SetActive(true);

            gameObject.SetActive(false);
        }
    }

}

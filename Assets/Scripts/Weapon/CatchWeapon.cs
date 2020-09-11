using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchWeapon : MonoBehaviour
{
    public GameObject weaponCatched;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Player")){

            Weapons w = weaponCatched.GetComponentInChildren<Weapons>();
            w.IsInPossesion = true;
            Weapons.TotalWeapons++;
            w.NumberThisWeapon = Weapons.TotalWeapons;
            
            if (StaticListWeapons.GetListAllWeapons().Count == 0)
            {
                weaponCatched.SetActive(true);
                w.IsActive = true;

            } else{
                 weaponCatched.SetActive(false);
                 w.IsActive = false;
            }

            StaticListWeapons.AddWeapon(w);
           
            ControlWeapons.Instance.AddNewWeaponCatched(weaponCatched);

            gameObject.SetActive(false);
        }
    }

}

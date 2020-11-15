/**
 * Department: Game Developer
 * File: CatchWeapon.cs
 * Objective: Have a control when the user catch a new weapon
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;

/**
 * 
 * This c lase is to add a weapon in the ground, this class activate the proces of the catching a weapon. 
 * Search the weapon in  the list, acitve it, and the rest of classes do their job to control the weapon.
 * 
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */ 
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

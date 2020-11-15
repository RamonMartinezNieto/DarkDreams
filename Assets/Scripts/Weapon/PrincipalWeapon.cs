/**
 * Department: Game Developer
 * File: PrincipalWeapon.cs
 * Objective: There is a specification of the Weapons to create a principal weapon.
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;

/**
 * 
 * Specification of the weapon to create a specific principal weapon. 
 * 
 * @see Weapon 
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class PrincipalWeapon : Weapons
{
    protected override float TimeDelayShot { get; set; } = .15f;

    void Start()
    {

        if (!IsMenuWeapon)
        {
            UpdateWeaponPosition(character.GetComponent<Transform>(), gameObject, character, transformWeapon);
            transformWeaponContainer.position = WeaponPosition;
        }

        UpdateWiewPivotWeapon(gameObject, character, transformWeaponContainer);

        canShoot = true;
    }

    void Update()
    {

        if (!IsMenuWeapon)
        {
            UpdateWeaponPosition(character.GetComponent<Transform>(), gameObject, character, transformWeapon);

            transformWeaponContainer.position = WeaponPosition;
        }

        UpdateWiewPivotWeapon(gameObject, character, transformWeaponContainer);

        //Check if CanShot is true
        CanShootTiming();

        if (!Pause.GameIsPaused)
        {
            //Hold on fire
            if (Input.GetMouseButton(0) && canShoot)
                Shoting(bulletType1);

            if (Input.GetButtonDown("Fire2") && (UIBullets.CurrentBullets > 0))
            {
                if (!IsMenuWeapon)
                    Shoting(bulletType2);
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincipalWeapon : Weapons
{

    private Transform weaponTransform;


    void Start()
    {
        weaponTransform = GetComponent<Transform>();


        updateWeaponPosition(character.GetComponent<Transform>(), gameObject, character, transformWeapon);
        updateWiewPivotWeapon(gameObject, character);

        weaponTransform.position = WeaponPosition;

        canShoot = true;

    }


    void Update()
    {
        updateWeaponPosition(character.GetComponent<Transform>(), gameObject, character, transformWeapon);
        updateWiewPivotWeapon(gameObject, character);

        weaponTransform.position = WeaponPosition;

        debugRayCast(weaponTransform);


        /* Continuos Button Pressed
                if (Input.GetMouseButton(0) && canShoot)
                {

                }
        */

        if (Input.GetButtonDown("Fire1"))
        {
            Shoting(bulletType1);
            
        }
        else if (Input.GetButtonDown("Fire2") && canShoot && (UIBullets.CurrentBullets > 0))
        {
            Shoting(bulletType2);
        }

    }


    public void SetActiveWeapon()
    {
        gameObject.SetActive(true);
    }

}

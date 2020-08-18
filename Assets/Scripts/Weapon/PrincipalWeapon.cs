using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincipalWeapon : Weapons
{

    private Transform weaponTransform;


    void Start()
    {
        weaponTransform = GetComponent<Transform>();
        
        UpdateWeaponPosition(character.GetComponent<Transform>(), gameObject, character, transformWeapon);
        UpdateWiewPivotWeapon(gameObject, character);

        weaponTransform.position = WeaponPosition;

        canShoot = true;
    }


    void Update()
    {
        UpdateWeaponPosition(character.GetComponent<Transform>(), gameObject, character, transformWeapon);
        UpdateWiewPivotWeapon(gameObject, character);

        weaponTransform.position = WeaponPosition;

        //TODO: Only to debug
        DebugRayCast(weaponTransform);

        
        //TODO: ¿Implemented this? Continuos Button Pressed
        /*
         * if (Input.GetMouseButton(0) && canShoot)
        {
            Shoting(bulletType1);
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

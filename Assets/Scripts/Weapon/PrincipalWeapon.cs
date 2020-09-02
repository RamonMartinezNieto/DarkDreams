using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincipalWeapon : Weapons
{
    public Transform transformWeaponContainer;

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

        //TODO: Only to debug
        //DebugRayCast(weaponTransform);


        //TODO: ¿Implemented this? Continuos Button Pressed
        /*
         * if (Input.GetMouseButton(0) && canShoot)
        {
            Shoting(bulletType1);
        }
        */


        if (!Pause.GameIsPaused)
        {
            if (Input.GetButtonDown("Fire1"))
                Shoting(bulletType1);
            else if (Input.GetButtonDown("Fire2") && canShoot && (UIBullets.CurrentBullets > 0))
            {
                if (!IsMenuWeapon)
                    Shoting(bulletType2);
            }
        }

    }

    public void SetActiveWeapon()
    {
        gameObject.SetActive(true);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincipalWeapon : Weapons
{
    public Transform transformWeaponContainer;

    private float timeDelayShot = 0.15f;
    private float timePassBewtweenShots;

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


        //Check if CanShot is true
        CanShootTiming();

        if (!Pause.GameIsPaused)
        {
            //Hold on fire
            if (Input.GetMouseButton(0) && canShoot)
                Shoting(bulletType1);
            
            if (Input.GetButtonDown("Fire1"))
                Shoting(bulletType1);

            else if (Input.GetButtonDown("Fire2") && (UIBullets.CurrentBullets > 0))
            {
                if (!IsMenuWeapon)
                    Shoting(bulletType2);
            }
        }

    }

    private void CanShootTiming() 
    {
        timePassBewtweenShots += Time.deltaTime;
        
        if (timePassBewtweenShots >= timeDelayShot)
        {
            canShoot = true;
            timePassBewtweenShots = 0;
        }
        else 
        {
            canShoot = false;
        }
    }

    public void SetActiveWeapon()
    {
        gameObject.SetActive(true);
    }

}

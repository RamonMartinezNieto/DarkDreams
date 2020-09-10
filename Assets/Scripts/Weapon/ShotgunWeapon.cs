using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : Weapons
{
    protected override float TimeDelayShot { get; set; } = .5f; 

    void Start()
    {

        UpdateWeaponPosition(character.GetComponent<Transform>(), gameObject, character, transformWeapon);
        transformWeaponContainer.position = WeaponPosition;
     
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

            /*
            if (Input.GetButtonDown("Fire1"))
                Shoting(bulletType1);
            */

            if (Input.GetButtonDown("Fire2") && (UIBulletsShotGun.CurrentBullets > 0))
            {
                if (!IsMenuWeapon)
                    NormalShoting(bulletType2);
            }
        }
    }

    public override void Shoting(GameObject bulletType)
    {
        //TODO: here is a position of the shoot, need move to ShotPrincipalWeapon, and correct the position
        Vector3 firePosition = transformWeapon.position;
        firePosition.z = -2f;

        for (int i = 0; i < 15; i++)
        {
            Instantiate(bulletType, firePosition, transformWeapon.rotation);
        }
    }

    public void NormalShoting(GameObject bulletType)
    {
        //TODO: here is a position of the shoot, need move to ShotPrincipalWeapon, and correct the position
        Vector3 firePosition = transformWeapon.position;
        firePosition.z = -2f;

        Instantiate(bulletType, firePosition, transformWeapon.rotation);
    }

}

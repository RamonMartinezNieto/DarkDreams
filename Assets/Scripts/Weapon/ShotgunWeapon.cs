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



}

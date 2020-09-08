using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincipalWeapon : Weapons
{
    protected override float TimeDelayShot { get; set; } = .2f;

    void Start()
    {
        Debug.Log(NumberThisWeapon);

        if (!IsMenuWeapon)
        {
            UpdateWeaponPosition(character.GetComponent<Transform>(), gameObject, character, transformWeapon);
            transformWeaponContainer.position = WeaponPosition;
        }

        UpdateWiewPivotWeapon(gameObject, character, transformWeaponContainer);

        canShoot = true;
    }


}

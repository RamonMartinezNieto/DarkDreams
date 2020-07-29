﻿using System.Collections;
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


        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            Shoting();
        } else if(Input.GetButtonDown("Fire2") && canShoot){
//TODO secondary Shoot
            Debug.Log("secondary shoot");
        }
    }




}

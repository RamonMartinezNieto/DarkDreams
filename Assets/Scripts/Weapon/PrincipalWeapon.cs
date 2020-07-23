using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincipalWeapon : Weapons
{

    public GameObject character;
    public Transform TransformWeaponContainer; 

    private Transform weaponTransform;

    void Start()
    {
        weaponTransform = GetComponent<Transform>();


        updateWeaponPosition(character.GetComponent<Transform>(), gameObject, character, TransformWeaponContainer);
        updateWiewPivotWeapon(gameObject, character);
        
        weaponTransform.position = WeaponPosition;

    }


    void Update()
    {
        updateWeaponPosition(character.GetComponent<Transform>(), gameObject, character, TransformWeaponContainer);
        updateWiewPivotWeapon(gameObject, character);

        weaponTransform.position = WeaponPosition;

        debugRayCast(weaponTransform); 
    }

}

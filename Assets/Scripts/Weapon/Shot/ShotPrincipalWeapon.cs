using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPrincipalWeapon : Shot
{
    //Need to set velocity, damage and destroy animation


    void Awake()
    {
        _shotVelocity = 2.5f; 
        _shotDamage = 10; 
    }

    void Start()
    {
        MovingShot();
    }



}

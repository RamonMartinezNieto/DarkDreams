using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPrincipalWeapon : Shot
{
    //Need to set velocity, damage and destroy animation

    [Tooltip("Weapon damage.")]
    public int damage = 10; 

    [Tooltip("Shot velocity.")]
    public float shotVelocity = 2.5f; 

    void Awake()
    {
        _shotVelocity = shotVelocity; 
        _shotDamage = damage; 
    }

    void Start()
    {
        MovingShot();
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPrincipalWeapon : Shot
{
    public bool IsMenuPrincipalShoot;

    //Need to set velocity, damage and destroy animation
    void Start()
    {
        MovingShot();

        if (!IsMenuPrincipalShoot)
        {
            SoundManager.Instance.PlayEffect("shootPrincipal");
        }
    }

}

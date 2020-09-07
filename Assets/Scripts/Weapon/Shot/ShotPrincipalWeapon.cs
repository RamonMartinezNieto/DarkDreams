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
            float variationPitch = Random.Range(.0f, .2f);
            float variationVol = Random.Range(.00f, .03f);

            SoundManager.Instance.PlayEffect("shootPrincipal",variationPitch, variationVol);
        }
    }

}

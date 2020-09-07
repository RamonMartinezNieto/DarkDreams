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
            //TODO: Suena raro si disparo rápido
            float variationPitch = Random.Range(-.1f, .2f);
            float variationVol = Random.Range(-.05f, .05f);

            SoundManager.Instance.PlayEffect("shootPrincipal",variationPitch, variationVol);
        }
    }

}

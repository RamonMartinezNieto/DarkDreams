using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPrincipalWeapon : Shot
{
    public bool IsMenuPrincipalShoot;

    void Start()
    {
        MovingShot();

        if (!IsMenuPrincipalShoot)
        {
            float variationPitch = Random.Range(-.1f, .2f);
            float variationVol = Random.Range(-.05f, .05f);

            SoundManager.Instance.PlayEffect("shootPrincipal",variationPitch, variationVol);
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShotGunShot : Shot
{

    void Start()
    {
        //float variationPitch = Random.Range(-.1f, .2f);
        float variationVol = Random.Range(.10f, .05f);

        SoundManager.Instance.PlayEffect("shootShotgun", 0f, variationVol);
        
        MovingShot();
    }

    new public void MovingShot()
    {
        timeDuration += Time.time;

        var direction2 = transform.right;

            direction2.x += UnityEngine.Random.Range(-.4f, +.4f);
            direction2.y += UnityEngine.Random.Range(-.6f, .6f);

        this.gameObject.GetComponent<Rigidbody2D>().velocity = direction2 * shotVelocity;
    }


}

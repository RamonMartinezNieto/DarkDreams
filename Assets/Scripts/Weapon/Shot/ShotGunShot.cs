using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunShot : Shot
{

    void Start()
    {
    

        //MovingShot();

        SoundManager.Instance.PlayEffect("shootPrincipal");
        
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

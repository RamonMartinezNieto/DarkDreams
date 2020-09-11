using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunShotSpecialSec : Shot
{
    void Start()
    {
        MovingShot();
    }

    new public void MovingShot()
    {
        timeDuration += Time.time;

        var direction2 = transform.right;

        direction2.x += Random.Range(-.9f, +.9f);
        direction2.y += Random.Range(-.9f, .9f);

        this.gameObject.GetComponent<Rigidbody2D>().velocity = direction2 * shotVelocity;
    }

}

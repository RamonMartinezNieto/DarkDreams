/**
 * Department: Game Developer
 * File: ShotGunShotSpecialSec.cs
 * Objective: Is a basic shot of the shotgun but chenge a little to modify the comportament an use it  in the secondary shot of the shotGun
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;

/**
 * 
 * Change a liitle the comportment of the principal shot of the shotgun to use it in the secondary shot of the shotgun
 * 
 * @see PlayerConf
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class ShotGunShotSpecialSec : Shot
{
    void Start()
    {
        MovingShot();
    }

    /**
     * Override MovingShot to stablish a diferent angels.
     */
    new public void MovingShot()
    {
        timeDuration += Time.time;

        var direction2 = transform.right;

        direction2.x += Random.Range(-.9f, +.9f);
        direction2.y += Random.Range(-.9f, .9f);

        this.gameObject.GetComponent<Rigidbody2D>().velocity = direction2 * shotVelocity;
    }

}

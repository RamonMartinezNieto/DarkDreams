/**
 * Department: Game Developer
 * File: ShotGunShot.cs
 * Objective: Have a specification of the secondary bullet of the shotgun
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;
using Random = UnityEngine.Random;

/**
 * 
 * This class is a specification of the class shot, to have a specific principal shot 
 * of the shot gun. The shot of the shotgun is in reality a multiple shoots with different 
 * angles. Extend methods with new public method.
 * 
 * @see Shot
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class ShotGunShot : Shot
{

    void Start()
    {
        //float variationPitch = Random.Range(-.1f, .2f);
        float variationVol = Random.Range(.10f, .05f);

        SoundManager.Instance.PlayEffect("shootShotgun", 0f, variationVol);
        
        MovingShot();
    }

    /**
     * Extensión of MovingShot to create a concrete angle to the differents shots, because 
     * when the principal shoot start, it create a multiple of this shoots. 
     * 
     * @see ShotgunWeapon#Shoting
     */
    new public void MovingShot()
    {
        timeDuration += Time.time;

        var direction2 = transform.right;

            direction2.x += UnityEngine.Random.Range(-.4f, +.4f);
            direction2.y += UnityEngine.Random.Range(-.6f, .6f);

        this.gameObject.GetComponent<Rigidbody2D>().velocity = direction2 * shotVelocity;
    }


}

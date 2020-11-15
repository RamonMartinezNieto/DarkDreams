/**
 * Department: Game Developer
 * File: CachableBulletShotgun.cs
 * Objective: Specific class of Cachable to create a cachable bullet of the shotgun.
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;

/**
 *  This class provide a specific methods to control a cachable to add a secondary bullet of the shotgun.
 * 
 * @see Cachable
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class CachableBulletShotgun : Cachable
{
    private UIBulletsShotGun secondaryShootsUI;

    void Awake() =>  secondaryShootsUI = GameObject.Find("SecondaryShootsShotGunUI").GetComponent<UIBulletsShotGun>();
    
    void OnTriggerEnter2D(Collider2D other) => cachtBullet(other);

    private void OnTriggerStay2D(Collider2D other) => cachtBullet(other);

    private void cachtBullet(Collider2D other) 
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (UIBulletsShotGun.CurrentBullets < 5)
            {
                gameObject.SetActive(false);
                secondaryShootsUI.enableBullet(UIBulletsShotGun.CurrentBullets);
            }
        }
    }
}

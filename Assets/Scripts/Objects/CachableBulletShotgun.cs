using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

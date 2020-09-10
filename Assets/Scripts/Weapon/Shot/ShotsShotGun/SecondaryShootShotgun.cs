using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryShootShotgun : Shot
{
   
    private SpriteRenderer bulletRender;

    private UIBulletsShotGun secondaryShootsUI;

    public ParticleSystem particleSystemSecondaryShoot;

    public GameObject prefabLittleBullets;

    private bool onlyOneTime = true;


    void Start()
    {
        bulletRender = transform.Find("Bullet").GetComponent<SpriteRenderer>();
        secondaryShootsUI = GameObject.Find("SecondaryShootsShotGunUI").GetComponent<UIBulletsShotGun>();

        secondaryShootsUI.disableBullet(UIBulletsShotGun.CurrentBullets - 1);

        SoundManager.Instance.PlaySecondaryEffect("shootSecondary");
        
        MovingShot();
    }

    void restBullet() {
        secondaryShootsUI.disableBullet(UIBulletsShotGun.CurrentBullets - 1); 
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (IsActive
            && !other.CompareTag("Player")
            && !other.CompareTag("GroundPlayerDetector")
            && !other.CompareTag("CrossHair")
            && !other.CompareTag("Cachable")
            && !other.name.Equals("Arrow")
            && !other.CompareTag("Shot")
            )
        {

            if (onlyOneTime)
            {
                
                SpecialShoting();
                onlyOneTime = false;

                secondaryShootsUI.

                StartCoroutine(DestroyShotAnimation());
            }
        }
    }

    private void SpecialShoting() 
    {
        Vector3 firePosition = transform.position;
        firePosition.z = -2f;

        //TODO: here is a position of the shoot, need move to ShotPrincipalWeapon, and correct the position
        for(int i = 0; i < 50; i ++)
            Instantiate(prefabLittleBullets, firePosition, transform.rotation);
    }

    public override IEnumerator DestroyShotAnimation()
    {
        //Extra explosion if no contanc with any object
        if (onlyOneTime)
        {
            SpecialShoting();
            onlyOneTime = false;
        }

        shootRigi.velocity = Vector2.zero;
        colliderShot.enabled = false;
        IsActive = false;

        StopLaunch();

        bulletRender.sprite = null;

        yield return new WaitForSeconds(0.85f);

        Destroy(gameObject);
        Destroy(shootContainer);
    }
    
    private void StopLaunch()
    {
        particleSystemSecondaryShoot.Stop();
        particleSystemSecondaryShoot.Clear();
    }
}
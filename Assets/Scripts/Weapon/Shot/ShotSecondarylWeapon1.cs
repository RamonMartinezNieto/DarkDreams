using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ShotSecondarylWeapon1 : Shot
{
    private ParticleSystem [] explosionParticles;
    private SpriteRenderer bulletRender; 

    private UIBullets secondaryShootsUI;

    public bool IsMenuSecondaryShoot;

    public ParticleSystem particleSystemSecondaryShoot; 

    //Need to set velocity, damage and destroy animation
    void Start()
    {

        bulletRender = transform.Find("Bullet").GetComponent<SpriteRenderer>();

        if (!IsMenuSecondaryShoot)
        {
            explosionParticles = transform.Find("Explosion").GetComponentsInChildren<ParticleSystem>();
            StopExplosion();

            secondaryShootsUI = GameObject.Find("SecondaryShootsUI").GetComponent<UIBullets>();
            restBullet();

            SoundManager.Instance.PlaySecondaryEffect("shootSecondary");
        }
        MovingShot();
    }

    void restBullet() => secondaryShootsUI.disableBullet(UIBullets.CurrentBullets - 1);

    public override IEnumerator DestroyShotAnimation()
    {
        shootRigi.velocity = Vector2.zero;

        StopLaunch();

        StartExplosion();
        bulletRender.sprite = null; 

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
        Destroy(shootContainer);
    }

    
    public void StartExplosion()
    {
        if (!IsMenuSecondaryShoot)
        {
            foreach (ParticleSystem p in explosionParticles)
                p.Play();
        }
    }

    private void StopExplosion() 
    {
        if (!IsMenuSecondaryShoot)
        {
            foreach (ParticleSystem ps in explosionParticles)
            ps.Stop();
        }
    }
    

    private void StopLaunch() 
    {
        particleSystemSecondaryShoot.Stop();
        particleSystemSecondaryShoot.Clear();
    }

}

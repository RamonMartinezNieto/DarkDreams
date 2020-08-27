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

    public bool IsMenuSecondatyShoot;


    public ParticleSystem prueba; 

    //Need to set velocity, damage and destroy animation
    void Start()
    {
        explosionParticles = transform.Find("Explosion").GetComponentsInChildren<ParticleSystem>();
        bulletRender = transform.Find("Bullet").GetComponent<SpriteRenderer>();
       
        StopExplosion();

        if (!IsMenuSecondatyShoot)
        {
            secondaryShootsUI = GameObject.Find("SecondaryShootsUI").GetComponent<UIBullets>();
            restBullet();
        }
        SoundManager.Instance.PlayEffect("shootSecondary");
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
        foreach (ParticleSystem p in explosionParticles)
            p.Play();
    }

    private void StopExplosion() 
    {
        foreach (ParticleSystem ps in explosionParticles)
            ps.Stop();
    }

    private void StopLaunch() 
    {
        Debug.Log("Stop launch");

        // particleSystemShot = gameObject.GetComponent<ParticleSystem>();
        prueba.Stop();
        prueba.Clear();
        
        
    }

}

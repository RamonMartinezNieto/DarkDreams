/**
 * Department: Game Developer
 * File: SecondaryShootShotgun.cs
 * Objective: Have a speciliciation of the shoot to create a secondary shoot of the shootgun.
 * Employee: Ramón Martínez Nieto
 */
using System.Collections;
using UnityEngine;
/**
 * This class is under Shot have a complete control of the shootgun's secondary shoot. 
 * IS VERY IMPORTANT, because the secondary shoot have a differents system particles, and 
 * very differnt comportment. 
 * 
 * @see Shot
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class SecondaryShootShotgun : Shot
{
   
    private SpriteRenderer bulletRender;

    private UIBulletsShotGun secondaryShootsUI;

    /**
     * Particle System of the secondary Shoot (this particle system follow the shoot)
     */
    [Tooltip("Add Particle System of the Secondary Shoot")]
    public ParticleSystem particleSystemSecondaryShoot;

    /**
     * Prefab of a pellet shoots
     */
    [Tooltip("Add prefab with the little bullets (pellet)")]
    public GameObject prefabLittleBullets;

    private bool onlyOneTime = true;

    void Start()
    {
        bulletRender = transform.Find("Bullet").GetComponent<SpriteRenderer>();
        secondaryShootsUI = GameObject.Find("SecondaryShootsShotGunUI").GetComponent<UIBulletsShotGun>();

        secondaryShootsUI.disableBullet(UIBulletsShotGun.CurrentBullets - 1);

        SoundManager.Instance.PlaySecondaryEffect("shootShotgunBigShot", 0.3f);
        
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


    /**
     * Method overrided to stop the particle system, show the partycle system of the explison, 
     * and destroy it. 
     * 
     * @return IEnumerator 
     */
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
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

    public float radiusExplosion = 1.50f;

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

            SoundManager.Instance.PlaySecondaryEffect("shootSecondary",0f);
        }
        MovingShot();
    }

    void restBullet() => secondaryShootsUI.disableBullet(UIBullets.CurrentBullets - 1);

    public override IEnumerator DestroyShotAnimation()
    {
        shootRigi.velocity = Vector2.zero;
        colliderShot.enabled = false;
        IsActive = false; 

        StopLaunch();

        StartExplosion();
        bulletRender.sprite = null;

        yield return new WaitForSeconds(0.85f);

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
            Vector2 posCenter = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.35f);
                Collider2D [] colls = Physics2D.OverlapCircleAll(
                    posCenter,
                    radiusExplosion, 
                    1  << LayerMask.NameToLayer("Default"),
                    minDepth:-1,
                    maxDepth:1 
                    );

                foreach(Collider2D col in colls) 
                {
                    if (col.CompareTag("Enemy"))
                    { 
                        Enemy enemy = col.GetComponent<Enemy>();
                        if (enemy != null)
                        {
                            enemy.TakeDamage(damage);
                        }
                    }
                }

                StartCoroutine(DestroyShotAnimation());
        }
    }

    private void OnDrawGizmos()
    {
        //TODO: TEST 
        Gizmos.color = Color.blue;
        Vector3 posCenter = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.35f,0);

        Gizmos.DrawSphere(posCenter, radiusExplosion);
    }
}

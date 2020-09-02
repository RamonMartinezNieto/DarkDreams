using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

abstract public class Shot : MonoBehaviour, IShooting
{

    public GameObject shootContainer;

    [Tooltip("Shot velocity.")]
    public float shotVelocity = 2.5f;

    [Tooltip("Weapon damage.")]
    public int damage = 5;

    [Tooltip("Time duration of the shoot.")]
    public float timeDuration = 2;

    private Animator animShot;
    protected Rigidbody2D shootRigi;
    protected ParticleSystem particleSystemShot;

    //Nedded to arrowShot
    protected Transform playerTransform;
    protected GameObject player;

    private void Awake()
    {
        shootRigi = GetComponent<Rigidbody2D>();
        animShot = GetComponent<Animator>();
        particleSystemShot = GetComponent<ParticleSystem>();

        //damage = GameObject.Find("SkeletonArcher").GetComponent<EnemySkeletonArcher>().Damage;
        player = GameObject.Find("Player");
        if (player == null) Destroy(this);
        else playerTransform = player.GetComponent<Rigidbody2D>().transform;
        // _shotDamage = damage;
        
        
    }

    private void Update()
    {
        if (Time.time >= timeDuration) StartCoroutine(DestroyShotAnimation());
    }

    public virtual void MovingShot()
    {
        timeDuration += Time.time;
        GetComponent<Rigidbody2D>().velocity = transform.right * shotVelocity;
    }

    //TODO: include animation
    public virtual IEnumerator DestroyShotAnimation()
    {
        
        shootRigi.velocity = Vector2.zero;
        animShot.SetBool("endShot", true);
        particleSystemShot.Stop();

        yield return new WaitForSeconds(GetAnimDuration(animShot, "ShotImpact"));

        Destroy(gameObject);
        Destroy(shootContainer);
    }

    protected float GetAnimDuration(Animator anim, string nameAnim) 
    {
        float animDur = .5f; 

        foreach (AnimationClip ac in anim.runtimeAnimatorController.animationClips) 
        {
            if (ac.name.Equals(nameAnim)) animDur = ac.length; 
        }

        return animDur;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Enemy") && other as CapsuleCollider2D)
             || (other.gameObject.CompareTag("GroundEnemyDetector")))
        {
            //TODO: Depende de como le de entra dos veces
            //When Shot hit the enemy
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy == null)
            {
                enemy = other.GetComponentInParent<Enemy>();
            }

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            StartCoroutine(DestroyShotAnimation());
        }
        else if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("GroundPlayerDetector"))
        {
            if (!other.isTrigger)
            {
                StartCoroutine(DestroyShotAnimation());
            }
        }
    }


    //Set arrow angle,  be carefoul, the angle that changes is the angle of the arrow container 
    public virtual void SetShotAngle(Vector3 objectiveTransform, float variationOfY = .0f)
    {
        objectiveTransform.y += variationOfY;

        Quaternion _loookRotation = Quaternion.LookRotation((objectiveTransform - transform.position).normalized);
        _loookRotation.x = 0.0f; _loookRotation.y = 0.0f;

        shootContainer.GetComponent<Transform>().rotation = _loookRotation;
    }
}
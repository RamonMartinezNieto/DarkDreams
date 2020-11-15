/**
 * Department: Game Developer
 * File: Shot.cs
 * Objective: Complete control of a shot
 * Employee: Ramón Martínez Nieto
 */
using System.Collections;
using UnityEngine;

/**
 * 
 * This class provide a complete control of a shoot, use this clas to extend and create a new one. 
 * There are methods virtual to override, check it. 
 * 
 * You can change all variables of the shoot in Unity, and change protected variables in the specification
 * 
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
abstract public class Shot : MonoBehaviour
{
    /**
     * Shoot container to rotate the shoot correctly 
     */
    public GameObject shootContainer;

    /**
     * Establish the shoot velcity 
     */
    [Tooltip("Shoot velocity.")]
    public float shotVelocity = 2.5f;

    /**
     * Establish a damage of the shoot  
     */
    [Tooltip("Shoot damage.")]
    public int damage = 5;

    /**
     * Time duration of the shoot  
     */
    [Tooltip("Time duration of the shoot.")]
    public float timeDuration = 2;

    protected Animator animShot;
    protected Rigidbody2D shootRigi;
    protected ParticleSystem particleSystemShot;

    //Nedded to arrowShot
    protected Transform playerTransform;
    protected GameObject player;

    protected CircleCollider2D colliderShot;
    protected bool IsActive = true;

    private void Awake()
    {
        shootRigi = GetComponent<Rigidbody2D>();
        animShot = GetComponent<Animator>();
        colliderShot = GetComponent<CircleCollider2D>();

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
    /**
     * Virtual IEnumeratior to destroy the animation of the shoot. 
     * 
     * @return IEnumerator
     */
    public virtual IEnumerator DestroyShotAnimation()
    {
        shootRigi.velocity = Vector2.zero;
        animShot.SetBool("endShot", true);
        particleSystemShot.Stop();

        colliderShot.enabled = false; 

        yield return new WaitForSeconds(GetAnimDuration(animShot, "ShotImpact"));

        Destroy(gameObject);
        Destroy(shootContainer);
    }

    /**
     *  Method to get the duration of the animation
     *  
     *  @return float Anim Duration
     */
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
        if (IsActive)
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
                    //Desactive this shot if hit a enemy
                    IsActive = false;
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
    }

    /**
     * Set arrow angle,  be carefoul, the angle that changes is the angle of the arrow container  
     */
    public virtual void SetShotAngle(Vector3 objectiveTransform, float variationOfY = .0f)
    {
        objectiveTransform.y += variationOfY;

        Quaternion _loookRotation = Quaternion.LookRotation((objectiveTransform - transform.position).normalized);
        _loookRotation.x = 0.0f; _loookRotation.y = 0.0f;

        shootContainer.GetComponent<Transform>().rotation = _loookRotation;
    }
}
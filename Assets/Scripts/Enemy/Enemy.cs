using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : MonoBehaviour
{
    private int _health;
    public int Health
    {
        get { return _health; }

        set { _health = value; }
    }

    private bool _playerDetection;
    public bool PlayerDetection { get; set; }

    [SerializeField] private HealthBar healthBar;

    private float Speed;

    private float VisionRangeRadius;

    public int Damage { get; set; }

    protected float DistanceToAttack;

    //private bool _inMovement = true;
    public bool InMovement { get; set; }

    public bool _attacking = false;
    public bool Attacking { get; set; }
    public Animator EnemyAnimator;

    public Rigidbody2D rbdEnemy;

    Vector2 inputVector;
    Vector2 randomFinalPosition;
    Vector2 currentPos;

    public Rigidbody2D characterRB;


    public void EnemyConstructor(int health, float speed, float visionRange, int Damage, float distanteToAttack)
    {
        Health = health;
        this.Speed = speed;
        this.Damage = Damage;
        this.DistanceToAttack = distanteToAttack;

        this.inputVector = RandomVector(-1.0f, 1.0f);
        this.currentPos = rbdEnemy.position;
        this.randomFinalPosition = RandomVector(-1.0f, 1.0f) + currentPos;
        this.VisionRangeRadius = visionRange;
        PlayerDetection = false;

        this.gameObject.GetComponent<CircleCollider2D>().radius = VisionRangeRadius;
        
        
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        float currentHealth = Health / 100f;
        healthBar.SetSize(currentHealth);

        if (currentHealth < .3f)
        {
            healthBar.SetColor(Color.red);
        }

        if (Health <= 0)
        {
            Die();
        }

    }

    public void Die()
    {
        //TODO reproduce animation, how? Y_Y
        //PlayAnimation("SkeletonDie");
        //Translate position of the enemy to simulate destroying this.
        rbdEnemy.transform.Translate(new Vector3(-250f, -250f, -150f));

    }

    //Movement in a place, when don't get vision with the player
    public void StaticMovement(bool attacking)
    {

        if (InMovement && !PlayerDetection)
        {
            currentPos = rbdEnemy.position;
            Vector2 movement = inputVector * Speed;
            Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;

            rbdEnemy.MovePosition(newPos);

            //Chekc if the enemy moves X and Y random vector
            if (currentPos.x.ToString("F1").Equals(randomFinalPosition.x.ToString("F1"))
            || currentPos.y.ToString("F1").Equals(randomFinalPosition.y.ToString("F1")))
            {
                InMovement = false;
            }

        }
        else
        {
            //New direction to move
            inputVector = RandomVector(-1.0f, 1.0f);
            currentPos = rbdEnemy.position;
            randomFinalPosition = RandomVector(-2.0f, 2.0f) + currentPos;

            InMovement = true;
        }

        //If the enemy is not attacking to the player
        if (!attacking)
        {
            PlayAnimation(gameObject.GetComponent<DirectionMovement>().CurrentDir.ToString());
        }
    }

    private void ChangeDirection()
    {
        inputVector = RandomVector(-1.0f, 1.0f);
        currentPos = rbdEnemy.position;

        var x = 0f;
        var y = 0f;

        if (randomFinalPosition.x < 0)
        {
            x = Mathf.Abs(randomFinalPosition.x);
        }
        else
        {
            x = randomFinalPosition.x * -1;
        }

        if (inputVector.y < 0)
        {
            y = Mathf.Abs(randomFinalPosition.y);
        }
        else
        {
            y = randomFinalPosition.y * -1;
        }

        randomFinalPosition = new Vector2(x, y) + currentPos;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("LimitsGround"))
        {
            ChangeDirection();
        }      
    }
        

    public void PlayAnimation(string playAnim)
    {
        EnemyAnimator.Play(playAnim);

    }

    private Vector2 RandomVector(float min, float max)
    {
        return new Vector2(Random.Range(min, max), Random.Range(min, max));
    }


    //Movement when get vision with the player
    public void MovementToPlayer(Vector2 playerPos)
    {
        currentPos = rbdEnemy.position;

        Vector2 direction = playerPos - currentPos;
        direction.Normalize();

        Vector2 newPos = currentPos + (direction * (Speed + .3f) * Time.fixedDeltaTime);

        rbdEnemy.MovePosition(newPos);

        
        if (!Attacking)
        {
            PlayAnimation(gameObject.GetComponent<DirectionMovement>().CurrentDir.ToString());
        }
    }

    //The animation have the method of the atack. Check the animation to know when the attack is launched.
    public void Attack(string animationAttack)
    {
        if (Attacking)
        {
            PlayAnimation("SkAttackSE");
        }
    }

    //Apply this method to animation attack, in the middle of the animation ocurrs this method.
    // To distance attacks is different, the atack is from shoot enemy attacks and not when the animation ocurrs. 
    public virtual void FineAttack()
    {
        //Rest live to the player, depends of the animation
        GameObject.Find("Player").GetComponent<PlayerStats>().restHealth(Damage);
    }


    public float RayToPlayerDistance(Rigidbody2D player)
    {
        float distance = 100;

        Vector3 currentPos = rbdEnemy.position;
        Vector3 playerPos = player.position;
        //Correction from pivot
        playerPos.y += 0.20f;

        RaycastHit2D ray = Physics2D.Raycast(currentPos, playerPos);

        if (ray.collider != null)
        {
            distance = Vector2.Distance(currentPos, playerPos);
        }

        //Remove DrawLine
        Debug.DrawLine(currentPos, playerPos, Color.blue);
        
        return distance;
    }



    void FixedUpdate()
    {
        if (!PlayerDetection && !Attacking)
        {
            //Todo, solo entra una puñetera vez
            StaticMovement(Attacking);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //When the player stay in the trigger Collider of the 
        if (other.gameObject.tag.Equals("Player"))
        {
            
            if (RayToPlayerDistance(other.GetComponent<Rigidbody2D>()) > DistanceToAttack - .03f)
            {
//TODO, I don't know if t his is the bes solution of my bug. Check it.
                Physics2D.IgnoreCollision(gameObject.GetComponent<CapsuleCollider2D>(), other);
                MovementToPlayer(other.GetComponent<Transform>().position);
            } 
            else if (RayToPlayerDistance(other.GetComponent<Rigidbody2D>()) < DistanceToAttack && !Attacking)
            {
                Attacking = true;
                Attack("SkAttackSE");
            }
            else
            {
                Attacking = false;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            PlayerDetection = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        //When the player exit of the trigger Collider of the 
        if (other.gameObject.tag.Equals("Player"))
        {
            InMovement = false;
            Attacking = false;
            PlayerDetection = false;
        }
    }
}



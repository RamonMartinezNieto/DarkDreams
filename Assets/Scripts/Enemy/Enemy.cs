using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

abstract public class Enemy : MonoBehaviour
{
    protected string[] ATTACKS = new string[4] { "SkAttackSE", "SkAttackSW", "SkAttackNW", "SkAttackNE" };

    //TODO: Health is very bad in bat, because it only have 50 of health, need to convert 100% an rest % not int
    public int Health { get; set; }
    private int initialHelath; 

    public bool PlayerDetection { get; set; }

    [SerializeField] private EnemyHealthBar healthBar;

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

    //public Rigidbody2D characterRB;

    public string directionToAttack { get; private set; }

    //Variables to controller when the enemy needs change their position.
    private float currentTime;
    private float stayTime;

    private Vector2 inputVector;
    private Vector2 randomFinalPosition;
    private Vector2 currentPos;
      

    public void EnemyConstructor(int health, float speed, float visionRange, int Damage, float distanteToAttack)
    {
        //TODO: test 
        currentTime = Time.time;
        stayTime = Random.Range(4f, 8f);  
        
        //Health indique only the bar 
        Health = 100;

        //initialHelath uses to calc decrease bar
        initialHelath = health; 

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
        //Rest % of the health
        Health -= 100 * damage / initialHelath;
        
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
        DropObject.InstantiateCachableObject(gameObject.transform.position);

        rbdEnemy.transform.Translate(new Vector3(-250f, -250f, -150f));
    }

    //Movement in a place, when don't get vision with the player
    public void StaticMovement(bool attacking)
    {
        if (InMovement && !PlayerDetection)
        {
            currentPos = rbdEnemy.position;
            Vector2 movement = inputVector * Speed;
            Vector2 newPos = currentPos + (movement * (Speed + .3F)) * Time.fixedDeltaTime;
            

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

    public Vector2 elVector;
    private void ChangeDirection()
    {
        //TODO: aquí hay un problema
        inputVector = RandomVector(-1.5f, 1.5f);
        elVector = inputVector; 

        currentPos = rbdEnemy.position;

        float x;
        float y;

        if (inputVector.x < 0)
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
        //Ignore collisions between enemies 
        if (other.gameObject.CompareTag("Enemy"))  { 
            Physics2D.IgnoreCollision(gameObject.GetComponent<CapsuleCollider2D>(), other.collider);
        }
        else 
        {
            ChangeDirection();
        }
    }

    public void PlayAnimation(string playAnim) =>  EnemyAnimator.Play(playAnim);

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
     public void Attack(string animationAttack = "SkAttackSE")
     {
        if (!Attacking)
        {
            PlayAnimation(animationAttack);
            
            Attacking = true;
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
        Vector3 playerPos = player.position;
        //Correction from pivot
        playerPos.y += 0.20f;

        if (RayToPlayer(player).collider != null)
        {
            distance = Vector2.Distance(currentPos, playerPos);
        }

        return distance;
    }

    private RaycastHit2D RayToPlayer(Rigidbody2D player) {

        Vector3 currentPos = rbdEnemy.position;
        Vector3 playerPos = player.position;
        //Correction from pivot
        playerPos.y += 0.20f;

        RaycastHit2D ray = Physics2D.Raycast(currentPos, playerPos);

        //TODO: Remove DrawLine
        Debug.DrawLine(currentPos, playerPos, Color.blue);

        return ray; 
    }

    protected void setDirectionToAttack(Vector3 algo) 
    {
        if (algo.x > .0f && algo.y > .0f)      { directionToAttack = ATTACKS[3]; }
        else if (algo.x > .0f && algo.y < .0f) { directionToAttack = ATTACKS[0]; }
        else if (algo.x < .0f && algo.y < .0f) { directionToAttack = ATTACKS[1]; }
        else if (algo.x < .0f && algo.y > .0f) { directionToAttack = ATTACKS[2]; }
    }
    

    void FixedUpdate()
    {
        //Thsi if is to force change the direction of the enemy
        if ((currentTime + stayTime ) < Time.time && !Attacking && !PlayerDetection) 
        {
            currentTime = Time.time;
            InMovement = false;
        } 
        if (!PlayerDetection && !Attacking)
        {
            // TODO: Solo entra una puñetera vez
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
//TODO:  I don't know if t his is the bes solution of my bug. Check it.
                Physics2D.IgnoreCollision(gameObject.GetComponent<CapsuleCollider2D>(), other);
                MovementToPlayer(other.GetComponent<Transform>().position);
                
                //Attacking = false; 
            } 
            else if (RayToPlayerDistance(other.GetComponent<Rigidbody2D>()) < DistanceToAttack && !Attacking)
            {
                //Attacking = true;// TODO: skeleton archer ? 

                //Change position of the attack, this change te direction look the enemy
                Ray ray = new Ray(transform.position, (other.transform.position - transform.position));
                
                setDirectionToAttack(ray.direction);

                Debug.DrawRay(transform.position, ray.direction, Color.yellow);
                Debug.DrawLine(transform.position, ray.direction, Color.green);

                Attack(directionToAttack);
            } else {
                Attacking = false; // TODO: skeleton archer ? 
            }
           
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            PlayerDetection = true;
        }

        if (other.gameObject.CompareTag("GroundPlayerDetector") ||
            other.gameObject.CompareTag("GroundEnemyDetector")) 
        {
            Collider2D[] colList = transform.GetComponentsInChildren<Collider2D>();
            foreach (Collider2D col in colList)
            {
                if (col.CompareTag("GroundEnemyDetector")) Physics2D.IgnoreCollision(col, other);
            }
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
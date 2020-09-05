using System;
using System.Collections;
using UnityEngine;

abstract public class Enemy : MonoBehaviour
{

    protected string[] ATTACKS = new string[4] { "SkAttackSE", "SkAttackSW", "SkAttackNW", "SkAttackNE" };

    public int Health { get; set; }

    private int initialHelath; 

    public bool PlayerDetection { get; set; }

    [SerializeField] private EnemyHealthBar healthBar;

    private float Speed;

    private float VisionRange;

    public int Damage { get; set; }

    protected float DistanceToAttack;

    public bool _attacking = false;
    
    public bool Attacking { get; set; }

    public Animator EnemyAnimator;

    public Rigidbody2D rbdEnemy;

    public string directionToAttack { get; private set; }

    //Variables to controller when the enemy needs change their position.
    private float currentTime;
    private float stayTime;

    private Vector2 inputVector;
    private Vector2 randomFinalPosition;
    private Vector2 currentPos;

    private GameObject player;
    private SpriteRenderer enemyRenderer; 

    private float extraVelocity = .08f;

    
    //TODO: review this, I don't know if this method is more efficien than translate position
    public void ActiveEnemey() 
    {
        gameObject.SetActive(true);
        enemyRenderer.color = Color.white;
    }

    public void EnemyConstructor(int health, float speed, float visionRange, int Damage, float distanteToAttack)
    {
        //Use these variables to calculate when the enemy needs change the position, 
        //This avoid  that enemy stay in the same place when the random vector is so close (because velocity affect at the enemy).
        currentTime = Time.time;
        stayTime = UnityEngine.Random.Range(4f, 8f);  
        
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
        this.VisionRange = visionRange;
        
        PlayerDetection = false;
        Attacking = false;

        player = GameObject.Find("Player");
        enemyRenderer = GetComponent<SpriteRenderer>();

        //TODO: Ignore physics between diferent ground detection
        if (player != null)
        {
            var groundPlayerDetect = GameObject.Find("ColliderDetect").GetComponent<CapsuleCollider2D>();
            var groundEnemyDetect = transform.Find("GroundDetect").GetComponent<CapsuleCollider2D>();
            Physics2D.IgnoreCollision(groundEnemyDetect, groundPlayerDetect);
        }

    }

    void FixedUpdate()
    {
        

        //Thsi if is to force change the direction of the enemy
        if ((currentTime + stayTime) < Time.time && !Attacking && !PlayerDetection)
        {
            currentTime = Time.time;

            ChangeDirection();

        }
        if (!PlayerDetection && !Attacking)
        {
            StaticMovement();
        }

        if (player != null)
        {
            PlayerDetectionMovement();
        }
    }

    //Decrease damage of the enemy. Convert health to 100% and decrease % damage.
    public void TakeDamage(int damage)
    {
        if(gameObject.activeSelf)
            StartCoroutine("effecEnemytDamage");

        //Rest % of the health
        Health -= 100 * damage / initialHelath;
        
        float currentHealth = Health / 100f;
        healthBar.SetSize(currentHealth);

        if (currentHealth < .3f) healthBar.SetColor(Color.red);
        
        if (Health <= 0)  Die();
    }

    private IEnumerator effecEnemytDamage()
    {
        enemyRenderer.color = new Color(1f, .25f, 0f);

        yield return new WaitForSeconds(0.1f);
              
        enemyRenderer.color = Color.white;
    }

    public void Die()
    {
        //TODO: reproduce animation, how? Y_Y
        //PlayAnimation("SkeletonDie");
        //Translate position of the enemy to simulate destroying this.
        DropObject.InstantiateCachableObject(gameObject.transform.position);

        transform.position = new Vector3(-252f, -247f, -150f);

        gameObject.SetActive(false); 

        EnemyRecovery er = FindObjectOfType<EnemyRecovery>();
        er.SaveEnemy(this);

        //TODO: Need calculate score
        GameManager.Instance.UpScore(100); 

        //TODO: leave this, I need use it to create enemies
        //er.RecoverSkeletonArcher(0f,0f);
    }


    //Movement in a place, when don't get vision with the player
    public void StaticMovement()
    {
        if (!PlayerDetection)
        {
            currentPos = rbdEnemy.position;
            Vector2 movement = inputVector * Speed;
            Vector2 newPos = currentPos + (movement * (Speed + extraVelocity)) * Time.fixedDeltaTime;

            rbdEnemy.MovePosition(newPos);

            if (!Attacking) PlayAnimation(GetComponent<DirectionMovement>().CurrentDir.ToString());
        }
    }

    private void ChangeDirection()
    {
        inputVector = RandomVector(-1.5f, 1.5f);

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

    public void PlayAnimation(string playAnim)
    {
        EnemyAnimator.Play(playAnim);
    }

    private Vector2 RandomVector(float min, float max)
    {
        return new Vector2(UnityEngine.Random.Range(min, max), UnityEngine.Random.Range(min, max));
    }

    //Movement when get vision with the player
    public void MovementToPlayer(Vector2 playerPos)
    {
        currentPos = rbdEnemy.position;

        Vector2 direction = playerPos - currentPos;
        direction.Normalize();

        Vector2 newPos = currentPos + (direction * (Speed + .8f) * Time.fixedDeltaTime);

        rbdEnemy.MovePosition(newPos);

        if (!Attacking)
        {
            PlayAnimation(gameObject.GetComponent<DirectionMovement>().CurrentDir.ToString());
        }
    }

    //The animation have the method of the atack. Check the animation to know when the attack is launched.
     public void Attack()
     {
        if (!Attacking)
        {
            PlayAnimation(directionToAttack);
            Attacking = true;
        }
    }

    //Apply this method to animation attack, in the middle of the animation ocurrs this method.
    // To distance attacks is different, the atack is from shoot enemy attacks and not when the animation ocurrs. 
    public virtual void FineAttack()
    {
        //Rest live to the player, depends of the animation
        player.GetComponent<PlayerStats>().restHealth(Damage);
    }

    // **************** IMPORTANT *********************//
    /* I used RayToPlayerDistance to calculate distance when player enter in a collider visión, 
     * This methos is INNEFICIENT. 
     * DON'T USE RAY TO CALCULATE DISTANCE, INSTEAD USE VECTOR2.DISTANCE
     * 
     * Memory consumption increase a lot whit this. 
     * 
     * Whit Rays when there are 60 enemies only have 15FPS, without Rays with 100 enemies 
     * have 60FPS.
     */
    /*
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

        //Debug.DrawLine(currentPos, playerPos, Color.blue);

        return ray; 
    }
    */
    // ****************************************//
    protected void SetDirectionToAttack(Vector3 algo) 
    {
        var dirInt = 0; 

        if (algo.x > .0f && algo.y > .0f)      { dirInt = 3; }
        else if (algo.x > .0f && algo.y < .0f) { dirInt = 0; }
        else if (algo.x < .0f && algo.y < .0f) { dirInt = 1; }
        else if (algo.x < .0f && algo.y > .0f) { dirInt = 2; }

        directionToAttack = ATTACKS[dirInt];
        EnemyAnimator.SetInteger("directionAttack", dirInt);
    }

    //Method to set direction attack from Animation
    public void SetDirectionAttack() 
    {
            Vector3 playerTransform = player.GetComponent<Transform>().position;
            Ray ray = new Ray(transform.position, playerTransform - transform.position);
            SetDirectionToAttack(ray.direction);
    }

 

    public void PlayerDetectionMovement() 
    {
        
        Vector3 pos = player.GetComponent<Transform>().position;
        double distance = System.Math.Round(Vector2.Distance(transform.position, pos),2);


        if (distance <= DistanceToAttack)   // X <= 2.5f
        {
            //Change position of the attack, this change te direction look the enemy
            Ray ray = new Ray(transform.position, (player.transform.position - transform.position));

            SetDirectionToAttack(ray.direction);

            //TODO: quit this
            //Debug.DrawRay(transform.position, ray.direction, Color.yellow);
            //Debug.DrawLine(transform.position, ray.direction, Color.green);

            Attack();
        }
        else if (distance < VisionRange && distance > DistanceToAttack)
        {
            MovementToPlayer(pos);

            Attacking = false;
            PlayerDetection = true;
        }
        else if (distance > VisionRange) 
        {
            PlayerDetection = false;
            Attacking = false;
        }
    }

    /*
    private void OnTriggerStay2D(Collider2D other)
    {
        //When the player stay in the trigger Collider of the 
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 pos = other.GetComponent<Transform>().position;
            float distance = Vector2.Distance(transform.position, pos);
            
            if (distance >= DistanceToAttack - .03f)
            {
                //I don't know if t his is the bes solution of my bug. Check it.
                Physics2D.IgnoreCollision(gameObject.GetComponent<CapsuleCollider2D>(), other);
                MovementToPlayer(pos);
                
                Attacking = false; 
            } 
            else if (distance < DistanceToAttack && !Attacking)
            {
                //Change position of the attack, this change te direction look the enemy
                Ray ray = new Ray(transform.position, (other.transform.position - transform.position));

                SetDirectionToAttack(ray.direction);

                //TODO: quit this
                //Debug.DrawRay(transform.position, ray.direction, Color.yellow);
                //Debug.DrawLine(transform.position, ray.direction, Color.green);
        
                Attack();
            }
        }
    }
    */
    /*
     * Important: colList used to checkc when the player enter in the circleCollider2D. 
     * Enemies only have a CircleCollider2D, is not the better solution, need to search
     * other solution.
     */
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Collider2D[] colList = GetComponentsInChildren<Collider2D>();
        

        if (other.gameObject.CompareTag("GroundPlayerDetector") ||
            other.gameObject.CompareTag("GroundEnemyDetector")) 
        {
            foreach (Collider2D col in colList)
            {
                if (col.CompareTag("GroundEnemyDetector")) Physics2D.IgnoreCollision(col, other);
            }
        }
    }


    //Function Relocate to move enemy to new point
    public virtual void Relocate(float x, float y) => transform.position = new Vector3(x, y, 0f);
    
    public void RestartHealth() 
    {
        //Restart Health and HealthBar
        Health = 100;
        float currentHealth = Health / 100f;
        healthBar.SetSize(currentHealth);
        healthBar.SetColor(new Color(0.07740552f, 0.6698113f, 0f));
    }
}
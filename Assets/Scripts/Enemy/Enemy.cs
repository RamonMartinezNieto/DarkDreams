/**
 * Department: Game Developer
 * File: Enemy.cs
 * Objective: Basic abstract class to create new enemies
 * Employee: Ramón Martínez Nieto
 */
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/**
 * Abstract class with all funcionality of the enemies.  Use the constructor to create 
 * new enemies. 
 * 
 * @author Ramón Martínez Nieto
 * 
 */
abstract public class Enemy : MonoBehaviour
{
    /**
     * Movements of the enmy, note: They have the same name of the animations
     */
    protected string[] ATTACKS = new string[4] { "SkAttackSE", "SkAttackSW", "SkAttackNW", "SkAttackNE" };

    /**
     * Health of the enmy. 
     * 
     * @return int current health
     * @param int health
     */
    public int Health { get; set; }
    private int initialHelath;

    /**
     * Variable to know if the enemy detect the player.
     * 
     * @return bool player detected
     */
    [HideInInspector]
    public bool PlayerDetection { get; set; }

    [SerializeField] 
    private EnemyHealthBar healthBar;

    /**
     * Speed of the enemy 
     */
    private float Speed;

    /**
     * Vision of the eny (radius)  
     */
    private float VisionRange;

    /**
     * To know or set the damage of the enemy. 
     * 
     * @param int damage
     * @return int currentDamage (this opens up the possibility of increased damage)
     */
    public int Damage { get; set; }

    /**
     * Attack distance, use it to know when the enemy is at the right distance to start the attack.
     */
    protected float DistanceToAttack;

    /**
     * To know if the enemy is attacking 
     */
    [HideInInspector]
    public bool _attacking = false;
    /**
     * To know if the enemy is attacking 
     */
    public bool Attacking { get; set; }

    /**
     * Animator of the enemy 
     */
    [Tooltip("Put the animator of the enemy")]
    public Animator EnemyAnimator;

    /**
     * RigiBody of the enemy. 
     */
    [Tooltip("Put the RigyBody2d of the enemy")]
    public Rigidbody2D rbdEnemy;

    /**
     * Direction To attack, to update the direction 
     */
    public string directionToAttack { get; private set; }

    /**
     * To know if the enemy is alive.
     */
    public bool IsAlive { get; private set; }

    private float currentTime;
    private float stayTime;

    //Variables to controll when the enemy needs change their position.
    private Vector2 inputVector;
    private Vector2 randomFinalPosition;
    private Vector2 currentPos;

    private GameObject player;

    private SpriteRenderer enemyRenderer; 

    private float extraVelocity = .08f;

    /**
     * Important! NavMeshAgent to include Unity3D IA, (using the asset to addapt to 2D) 
     */
    private NavMeshAgent agent;

    /**
     * Constructor of the all enemies. Use this class in the Awake method to create new enemies. 
     * 
     * @param int Health
     * @param float Speed
     * @param float Vision range (radius) 
     * @param int Damage of the enemy 
     * @param float Distance to attack (adjust deppends the animation) 
     */
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

        IsAlive = true; 
        PlayerDetection = false;
        Attacking = false;

        player = GameObject.Find("Player");
        enemyRenderer = GetComponent<SpriteRenderer>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        

        agent.speed = Speed + 1f;

        //TODO: Ignore physics between diferent ground detection
        //Because a incompresible motive, sometimes this dont run !!!!!!!! Y_Y
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
            StaticMovement();

        if (player != null)
            PlayerDetectionMovement();
    }

    /**
     * Decrease damage of the enemy. Convert health to 100% and decrease % damage, to addapt differents initial lives.
     * 
     * @param int damage quantity of decrease the healt.
     */ 
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

    /**
     * Method to "kill" the enemy, really it put setActive(false) and change variable IsAlive to false. 
     * Use the EnemyRecovery Class to findEnemy in the Enemies Alive and save in the list EnemyDie. 
     * 
     * Use GameManager to increase the score 
     * 
     * @see GameManager#UpScore
     * @see GameManager#Instance 
     * @see EnemyRecovery#SaveEnemyDie
     */
    public void Die()
    {
        if (IsAlive)
        {
            //gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

            //TODO: reproduce animation, how? Y_Y
            //PlayAnimation("SkeletonDie");
            //Translate position of the enemy to simulate destroying this.
            DropObject.InstantiateCachableObject(gameObject.transform.position);

            transform.position = new Vector3(-100f, -100f, 0f);

            gameObject.SetActive(false);

            EnemyRecovery er = FindObjectOfType<EnemyRecovery>();
            er.SaveEnemyDie(this);

            //TODO: Need calculate score
            GameManager.Instance.UpScore(100);

        }
        IsAlive = false; 
    }


    /**
     * Movement in a place, when don't get vision with the player
     * 
     *  AI with which the enemy moves around the map.
     * 
     */
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

    /**
     * Method to change direction. 
     */
    private void ChangeDirection()
    {
        inputVector = RandomVector(-1.5f, 1.5f);

        currentPos = rbdEnemy.position;

        float x;
        float y;
        
        if (inputVector.x < 0)
            x = Mathf.Abs(randomFinalPosition.x);
        else
            x = randomFinalPosition.x * -1;

        if (inputVector.y < 0)
            y = Mathf.Abs(randomFinalPosition.y);
        else
            y = randomFinalPosition.y * -1;

        randomFinalPosition = new Vector2(x, y) + currentPos;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Ignore collisions between enemies 
        if (other.gameObject.CompareTag("Enemy"))  
            Physics2D.IgnoreCollision(gameObject.GetComponent<CapsuleCollider2D>(), other.collider);
        else 
            ChangeDirection();
    }

    /**
     * Play a animation of the enemy. 
     * Motion animations, attack animations and whatewer you want.
     * 
     * @param string Name of the animation
     */
    public void PlayAnimation(string playAnim) => EnemyAnimator.Play(playAnim);
    
    private Vector2 RandomVector(float min, float max)
    {
        return new Vector2(UnityEngine.Random.Range(min, max), UnityEngine.Random.Range(min, max));
    }

    /**
     * Active agent to use IA (NavMeshAgent) to move the enemy across the map to the player. 
     * Use it when take vision.
     * 
     * @param Vector2 Player position
     */
    public void MovementToPlayer(Vector2 playerPos)
    {
        agent.enabled = true;
        agent.SetDestination(playerPos);

        if (!Attacking)
            PlayAnimation(gameObject.GetComponent<DirectionMovement>().CurrentDir.ToString());

        /* 
        //This is @deprecated, currently use the agent to add movement

        currentPos = rbdEnemy.position;

        Vector2 direction = playerPos - currentPos;
        direction.Normalize();

        Vector2 newPos = currentPos + (direction * (Speed + .8f) * Time.fixedDeltaTime);

        rbdEnemy.MovePosition(newPos);
        */
    }

    /**
     * The animation have the method of the atack. Check the animation to know when the attack is launched.
     * 
     * Is possible use it in other situations.
     */ 
     public void Attack()
     {
        if (!Attacking)
        {
            PlayAnimation(directionToAttack);
            Attacking = true;
        }
    }

    /**
     * Apply this method to animation attack, in the middle of the animation ocurrs this method.
     * To distance attacks is different, the atack is from shoot enemy attacks and not when the animation ocurrs. 
     *
     * Rest live to the player, depends of the animation
     */
    public virtual void FineAttack() => player.GetComponent<PlayerStats>().restHealth(Damage);

    /**
     * Set direction to attack, deppens of the array. Use it to play the correct animation.
     * 
     * @see Enemy#ATTACKS
     */
    protected void SetDirectionToAttack(Vector3 posTo) 
    {
        var dirInt = 0; 

        if (posTo.x > .0f && posTo.y > .0f)      { dirInt = 3; }
        else if (posTo.x > .0f && posTo.y < .0f) { dirInt = 0; }
        else if (posTo.x < .0f && posTo.y < .0f) { dirInt = 1; }
        else if (posTo.x < .0f && posTo.y > .0f) { dirInt = 2; }

        directionToAttack = ATTACKS[dirInt];
        EnemyAnimator.SetInteger("directionAttack", dirInt);
    }

    /**
     * Set direction to attack, deppens of the array. Use it to play the correct animation.
     * 
     * @see Enemy#SetDirectionToAttack(Vector3)
     * @deprecated use SetDirectionToAttack(Vector3)
     */
    public void SetDirectionAttack() 
    {
            Vector3 playerTransform = player.GetComponent<Transform>().position;
            Ray ray = new Ray(transform.position, playerTransform - transform.position);
            SetDirectionToAttack(ray.direction);
    }

    /**
     * Method to know if the player is in the vision range. 
     */
    public void PlayerDetectionMovement() 
    {
        Vector3 pos = player.GetComponent<Transform>().position;
        double distance = System.Math.Round(Vector2.Distance(transform.position, pos),2);

        if (distance < DistanceToAttack)   // X <= 2.5f
        {
            //Disable agent IA 
            agent.enabled = false;
            Vector3 direction = player.transform.position - transform.position;
            
            SetDirectionToAttack(direction);
            
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
            //Disable agent IA 
            agent.enabled = false;

            PlayerDetection = false;
            Attacking = false;
        }
    }

    /**
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

    /**
     * Function Relocate to move enemy to new point
     * Use it when a deactivated enemy is activated.
     * 
     * @see EnemyRecovery#RecoveryEnemy<T>
     */
    public virtual void Relocate(float x, float y) => transform.position = new Vector3(x, y, 0f);

    /**
     * Method for taking life from the enemy
     */
    public void RestartHealth() 
    {
        //Restart Health and HealthBar
        IsAlive = true;
        Health = 100;
        float currentHealth = Health / 100f;
        healthBar.SetSize(currentHealth);
        healthBar.SetColor(new Color(0.07740552f, 0.6698113f, 0f));
    }

    /**
     * Method to active enemy. 
     * 
     * @see EnemyRevory
     */
    public void ActiveEnemey()
    {
        gameObject.SetActive(true);
        enemyRenderer.color = Color.white;
    }
}
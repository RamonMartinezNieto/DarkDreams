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

    [SerializeField] private HealthBar healthBar; 

    private float Speed;

    private float VisionRange;

    private float Damage;

    private float DistanteToAttack;

    private bool InMovement = true;

    public Animator EnemyAnimator;

    public Rigidbody2D rbdEnemy;

    Vector2 inputVector;
    Vector2 randomFinalPosition;
    Vector2 currentPos;


    public void EnemyConstructor(int health, float speed, float visionRange, float Damage, float distanteToAttack)
    {
        Health = health;
        this.Speed = speed;
        this.Damage = Damage;
        this.DistanteToAttack = distanteToAttack;

        this.inputVector = RandomVector(-1.0f, 1.0f);
        this.currentPos = rbdEnemy.position;
        this.randomFinalPosition = RandomVector(-1.0f, 1.0f) + currentPos;

     
        //  Debug.Log(currentPos);
        //  Debug.Log(randomFinalPosition);

    }

    //Current vision of the enemy, when is in the range the enemy run to the player
    public void Vsion()
    {

    }

    public void TakeDamage(int damage)
    {
        Health -= damage; 

        float currentHealth = Health / 100f;
        healthBar.SetSize(currentHealth); 
        
        if(currentHealth < .3f){
            healthBar.SetColor(Color.red);
        }

        if (Health <= 0)
        {
            Die();
        }
        
    }

    public void Die()
    {
        Debug.Log("Die");
        //Translate position of the enemy to simulate destroying this.
        rbdEnemy.transform.Translate(new Vector3(-250f, -250f, -150f));
    }

    //Movement in a place, when don't get vision with the player
    public void StaticMovement()
    {
        if (InMovement)
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


        PlayAnimation(gameObject.GetComponent<DirectionMovement>().CurrentDir.ToString());
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
    public void MovementToPlayer()
    {

    }

    public void Attack()
    {

    }


}

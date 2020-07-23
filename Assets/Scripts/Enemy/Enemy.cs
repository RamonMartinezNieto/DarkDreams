using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : MonoBehaviour
{
    private int Health;

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

    public void EnemyConstructor(int healt, float speed, float visionRange, float Damage, float distanteToAttack)
    {
        this.Health = healt;
        this.Speed = speed;
        this.Damage = Damage;
        this.DistanteToAttack = distanteToAttack;

        this.inputVector =  RandomVector(-1.0f, 1.0f);
        this.currentPos = rbdEnemy.position; 
        this.randomFinalPosition = RandomVector(-1.0f,1.0f) + currentPos;
        
        Debug.Log(currentPos);
        Debug.Log(randomFinalPosition);

        }

    public void UpdateLive(int modifyHealth)
    {
        this.Health -= modifyHealth;
    }

    //Current vision of the enemy, when is in the range the enemy run to the player
    public void Vsion()
    {

    }


    
    //Movement in a place, when don't get vision with the player
    public void StaticMovement()
    {
        if(InMovement){
            
            currentPos = rbdEnemy.position; 
            Vector2 movement = inputVector * Speed; 
            Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
            
            rbdEnemy.MovePosition(newPos); 

            Debug.Log("Current X: " + currentPos.x.ToString("F1"));
            Debug.Log("FianlRandom Y: " +randomFinalPosition.x.ToString("F1")); 

            //Chekc if the enemy moves X and Y random vector
            if(currentPos.x.ToString("F1").Equals(randomFinalPosition.x.ToString("F1")) 
            || currentPos.y.ToString("F1").Equals(randomFinalPosition.y.ToString("F1")) ){
                InMovement = false; 
            }

        } else {
            //New direction to move
            inputVector =  RandomVector(-1.0f, 1.0f);
            currentPos = rbdEnemy.position; 
            randomFinalPosition = RandomVector(-2.0f,2.0f) + currentPos;

            InMovement = true; 
        }
    }

    //Movement when get vision with the player
    public void MovementToPlayer()
    {

    }

    public void Attack()
    {

    }

    private void PlayAnimation(string playAnim)
    {
        EnemyAnimator.Play(playAnim);
    }

    private Vector2 RandomVector(float min, float max)
    {
        return new Vector2(Random.Range(min, max), Random.Range(min, max));
    }

}

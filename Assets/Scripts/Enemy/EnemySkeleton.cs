using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Stats of the Skeleton: 
 * Health = 100;
 * Speed = 0.5f;
 * Vision = 2.0f;
 * Damage = 5; 
 * DistanceToAttack = .7f; 
 */
public class EnemySkeleton : Enemy
{

    void Awake()
    {
        //Health, speed, vision, damage, distanceToAttack
        EnemyConstructor(100, 0.5f, 3.0f, 5, .7f);

    }

    void FixedUpdate()
    {

        if (!PlayerDetection && !Attacking)
        {
            StaticMovement(Attacking);
        }


    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag.Equals("Player"))
        {
            PlayerDetection = true; 
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {

        //When the player stay in the trigger Collider of the 
        if (other.gameObject.tag.Equals("Player"))
        {

            if (RayToPlayer(other.GetComponent<Rigidbody2D>()) < DistanceToAttack)
            {
                Attacking = true;
                Attack();
            } else {
                Attacking = false; 
            }
            
            MovementToPlayer(other.GetComponent<Transform>().position);
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

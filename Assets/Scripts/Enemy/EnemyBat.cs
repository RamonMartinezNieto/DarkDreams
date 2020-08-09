using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy
{
    void Awake()
    {
        //Health, speed, vision, damage, distanceToAttack
        EnemyConstructor(50, 0.7f, 2.5f, 5, .4f);
    }

    //TODO: Currently bat no attack !!!! 


    private void OnTriggerStay2D(Collider2D other)
    {
        //When the player stay in the trigger Collider of the 
        if (other.gameObject.tag.Equals("Player"))
        {
            if (RayToPlayerDistance(other.GetComponent<Rigidbody2D>()) > DistanceToAttack - .03f)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<CapsuleCollider2D>(), other);
                MovementToPlayer(other.GetComponent<Transform>().position);
            }
            else if (RayToPlayerDistance(other.GetComponent<Rigidbody2D>()) < DistanceToAttack && !Attacking)
            {
                Attacking = true;
                Attack("BatAttackSE");
            }
            else
            {
                Attacking = false;
            }
        }
    }


}

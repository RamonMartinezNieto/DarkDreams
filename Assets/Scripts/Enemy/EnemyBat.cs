using UnityEngine;

public class EnemyBat : Enemy
{
    void Awake()
    {
        //Health, speed, vision, damage, distanceToAttack
        EnemyConstructor(35, 0.7f, 3.5f, 5, .4f);
        
        //Change ArrayAttacks
        ATTACKS[0] = "BatAttackE";
        ATTACKS[1] = "BatAttackW";
        ATTACKS[2] = "BatAttackW";
        ATTACKS[3] = "BatAttackE";
    }
    /*
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
                Ray ray = new Ray(transform.position, (other.GetComponent<Transform>().position - transform.position));
                SetDirectionToAttack(ray.direction);

                Attack();
            }
            else
            {
                Attacking = false;
            }
        }
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /* Stats of the Skeleton: 
     * Health = 100;
     * Speed = 0.5f;
     * Vision = 10.0f;
     * Damage = 5; 
     * DistanceToAttack = 2.0f; 
     */
public class EnemySkeleton : Enemy
{
    
    void Awake()
    {
        //Health, speed, vision, damage, distanceToAttack
        EnemyConstructor(100, 0.5f, 10.0f, 5, 2.0f);

    }

    void FixedUpdate()
    {
        StaticMovement(); 
    }
}

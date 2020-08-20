
/* Stats of the Skeleton: 
 * Health = 100;
 * Speed = 0.5f;
 * Vision = 2.0f;
 * Damage = 5; 
 * DistanceToAttack = .7f; 
 */
using System;
using UnityEngine;

public class EnemySkeleton : Enemy
{
    
    void Awake()
    {
        
        //Health, speed, vision, damage, distanceToAttack
        EnemyConstructor(90, 0.5f, 4f, 10, .7f);
    }

}

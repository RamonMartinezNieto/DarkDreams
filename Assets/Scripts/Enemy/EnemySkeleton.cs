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
        EnemyConstructor(90, 0.5f, 3.0f, 10, .7f);
    }
}

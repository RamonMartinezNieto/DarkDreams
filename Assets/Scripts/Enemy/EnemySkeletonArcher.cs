/**
 * Department: Game Developer
 * File: EnemySkeletonArcher.cs
 * Objective: Create a skeleton archer enemy.
 * Employee: Ramón Martínez Nieto
 */

using UnityEngine;

/**
 * Skeleton Archer Enemy, create a new enemy using the EnemyConstructor (Enemy class) in the awake method. 
 * 
 * @author Ramón Martínez Nieto
 * @see Enemy#EnemyConstructor
 * @version 1.0.0
 */
public class EnemySkeletonArcher : Enemy
{
    /**
     * Arrow that the enemy shot  
     */
    [Tooltip("Shot (arrow) that the enemy fire")]
    public GameObject arrowType; 
    
    void Awake()
    {
        //Health, speed, vision (rad), damage, distanceToAttack
        EnemyConstructor(65, 0.5f, 4.5f, 10, 2.5f);
    }


    /**
     * Overrride method FineAttack. 
     * Intantiate arrow, the arrow have the logich of the damage. 
     * 
     * @Override FineAttack
     * @See Enemy#FineAttack
     */
    public override void FineAttack() => Instantiate(arrowType, gameObject.transform.position, Quaternion.identity); 
    
}

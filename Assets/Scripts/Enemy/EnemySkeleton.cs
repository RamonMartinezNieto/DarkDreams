/**
 * Department: Game Developer
 * File: EnemySkeleton.cs
 * Objective: Create a skeleton enemy.
 * Employee: Ramón Martínez Nieto
 */


/**
 * 
 * Skeleton Enemy, create a new enemy using the EnemyConstructor (Enemy class) in the awake method. 
 * 
 * @author Ramón Martínez Nieto
 * @see Enemy#EnemyConstructor
 * @version 1.0.0
 */
public class EnemySkeleton : Enemy
{
    
    void Awake()
    {
        //Health, speed, vision, damage, distanceToAttack
        EnemyConstructor(90, 0.5f, 4f, 10, .7f);
    }

}

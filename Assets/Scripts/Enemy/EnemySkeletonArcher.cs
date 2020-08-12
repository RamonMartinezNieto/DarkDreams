using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonArcher : Enemy
{
    [Tooltip("Shot (arrow) that the enemy fire")]
    public GameObject arrowType; 
    
    void Awake()
    {
        //Health, speed, vision (rad), damage, distanceToAttack
        EnemyConstructor(65, 0.5f, 3.5f, 10, 2.5f);
    }


    //Overrride the original FineAttack.
    //Intantiate arrow, the arrow have the logich of the damage. 
    public override void FineAttack() => Instantiate(arrowType, gameObject.transform.position, Quaternion.identity); 
    
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public static EnemyGenerator Instance; 

    private EnemyRecovery er;
  

    private Vector2 doorOnePosition;
    private Vector2 doorTwoPosition;
    private Vector2 doorThreePosition;
    private Vector2 doorFourPosition;
    private Vector2 centerPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this) 
        {
            Destroy(this);
        }
    }


    void Start()
    {
        er = GetComponent<EnemyRecovery>();

        doorOnePosition = new Vector2(10.78f,-2.7f);
        doorTwoPosition = new Vector2(-7f, -2.2f);
        doorThreePosition = new Vector2(-7f, 5f);
        doorFourPosition = new Vector2(6.8f, 4.6f);
        centerPosition = new Vector2(0f, 0.7f);

        createEnemiesCenter<EnemySkeleton>(2);
        createEnemiesCenter<EnemySkeletonArcher>(2);
        createEnemiesCenter<EnemyBat>(2);
    }

    public void GenerateEnemies(int quantityTotal) 
    {

        int createEnemies2 = quantityTotal / 2;
        int r = Random.Range(4, createEnemies2);
        int r2 = Random.Range(r + 4, quantityTotal - 4);
        int r3 = quantityTotal - r2;

        createEnemieForEachDorr<EnemySkeleton>(r);
        createEnemieForEachDorr<EnemySkeletonArcher>(r2);
        createEnemieForEachDorr<EnemyBat>(r3);
    }

    private void createEnemiesCenter<T>(int quantity) where T : Enemy
    {
        for (int a = 0; a < quantity; a++)
            er.RecoveryEnemy<T>(centerPosition.x, centerPosition.y);
    }

    private void createEnemieForEachDorr<T>(int quantity) where T : Enemy
    {
        var total = 0; 
        var quant = quantity / 4;

        for (int a = 0; a < quant; a++)
        {
            er.RecoveryEnemy<T>(doorOnePosition.x, doorOnePosition.y);
            total++;
        }

        for (int b = 0; b < quant; b++) { 
            er.RecoveryEnemy<T>(doorTwoPosition.x, doorTwoPosition.y);
             total++;
        }
        for (int c = 0; c < quant; c++){
            er.RecoveryEnemy<T>(doorThreePosition.x, doorThreePosition.y);
           total++;
        }
        for (int z = 0; z < quant; z++){
            er.RecoveryEnemy<T>(doorFourPosition.x, doorFourPosition.y);
            total++;
        }

    }
}

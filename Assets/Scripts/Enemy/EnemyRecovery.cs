using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEditor;
using UnityEngine;

public class EnemyRecovery : MonoBehaviour
{

    private static List<Enemy> enemiesDied = new List<Enemy>();

    [SerializeField]
    private GameObject prefabSkeleton;

    [SerializeField]
    private GameObject EnemyBat;

    [SerializeField]
    private GameObject EnemySkeletonArcher;


    public void SaveEnemy(Enemy enemy)
    {
        enemiesDied.Add(enemy);
    }


    public void RecoverSkeleton(float x, float y) 
    {
        checkAndRelocateOrInstantiateEnemy<EnemySkeleton>("EnemySkeleton",x,y);
    }

    public void RecoverSkeletonArcher(float x, float y)
    {
        checkAndRelocateOrInstantiateEnemy<EnemySkeletonArcher>("EnemySkeletonArcher",x,y);
    }

    public void RecoverBat(float x, float y)
    {
        checkAndRelocateOrInstantiateEnemy<EnemyBat>("EnemyBat",x,y);
    }

    private void checkAndRelocateOrInstantiateEnemy<T>(string enemyType, float x, float y) 
    {
        int index = GetEnemy<T>();

        if (index == -1)
        {
            if (enemyType.Equals("EnemySkeleton"))
            {
                Instantiate(prefabSkeleton, new Vector3(x, y, 0f), Quaternion.identity);
            }
            else if (enemyType.Equals("EnemySkeletonArcher"))
            {
                Instantiate(EnemySkeletonArcher, new Vector3(x, y, 0f), Quaternion.identity);
            }
            else if (enemyType.Equals("EnemyBat"))
            {
                Instantiate(EnemyBat, new Vector3(x, y, 0f), Quaternion.identity);
            }
        }
        else {
            Enemy e = enemiesDied[index];
            e.Relocate(x,y);
            enemiesDied.RemoveAt(index);
        }
    }

    private int GetEnemy<T>() 
    {
        for(int i = 0; i < enemiesDied.Count; i++)
        {
            if (enemiesDied[i].GetType() == typeof(T))
            {
                return i; 
            }
        }
        return -1; 
    }
}

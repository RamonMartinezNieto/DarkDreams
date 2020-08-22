using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyRecovery : MonoBehaviour
{

    //TODO: I can make this class abstract to implement same clas with other type (Enemy / Cachable) and no repeat the same code?

    private static List<Enemy> enemiesDied = new List<Enemy>();

    public void SaveEnemy(Enemy enemy) => enemiesDied.Add(enemy);

    public void RecoveryEnemy<T>(float x, float y) where T : Enemy
    {
        int index = GetEnemy<T>();

        if (index == -1)
        {
            string pathPrefab = $"Prefabs/Enemys/{typeof(T)}";
            GameObject enemy = Instantiate(Resources.Load(pathPrefab, typeof(GameObject)), gameObject.transform) as GameObject;
            enemy.GetComponent<Enemy>().Relocate(x, y);
        }
        else {
            Enemy e = enemiesDied[index];
            e.Relocate(x,y);
            e.RestartHealth();
            e.ActiveEnemey();
            enemiesDied.RemoveAt(index);
        }
    }

    private int GetEnemy<T>() 
    {
        for(int i = 0; i < enemiesDied.Count; i++)
            if (enemiesDied[i].GetType() == typeof(T)) return i;
        
        return -1; 
    }
}
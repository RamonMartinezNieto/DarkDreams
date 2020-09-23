using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;
using UnityEngine;

public class EnemyRecovery : MonoBehaviour
{
    public static EnemyRecovery Instance; 

    //TODO: I can make this class abstract to implement same clas with other type (Enemy / Cachable) and no repeat the same code?
    private List<Enemy> enemiesDied = new List<Enemy>();

    private List<Enemy> enemiesAlive = new List<Enemy>();

    public int GetEnemiesAlive() { return enemiesAlive.Count; }

    /*
     //Only to test
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            Debug.LogWarning(enemiesAlive.Count);
        }
    }

    */

    private void Start()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);
    }

    public void SaveEnemyDie(Enemy enemy) {
        enemiesDied.Add(enemy);

        if (enemiesAlive.Contains(enemy)) 
        {
            enemiesAlive.Remove(enemy);
        }
    }

    public void ClearEnemyList() => enemiesDied.Clear();

    public void RecoveryEnemy<T>(float x, float y) where T : Enemy
    {
        int index = GetEnemy<T>();

        if (index == -1)
        {
            string pathPrefab = $"Prefabs/Enemys/{typeof(T)}";
            GameObject enemy = Instantiate(Resources.Load(pathPrefab, typeof(GameObject)), gameObject.transform) as GameObject;
            Enemy e = enemy.GetComponent<Enemy>();
            //save enemy when is alive
            enemiesAlive.Add(e);
            enemy.GetComponent<Enemy>().Relocate(x, y);
        }
        else {
            Enemy e = enemiesDied[index];
            e.Relocate(x,y);
            e.RestartHealth();
            e.ActiveEnemey();
            enemiesDied.RemoveAt(index);
            enemiesAlive.Add(e);
        }
    }

    private int GetEnemy<T>() 
    {
        for(int i = 0; i < enemiesDied.Count; i++)
            if (enemiesDied[i].GetType() == typeof(T)) return i;
        
        return -1; 
    }
}
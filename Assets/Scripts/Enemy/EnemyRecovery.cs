/**
 * Department: Game Developer
 * File: EnemyRecovery.cs
 * Objective: Pooling of the enemies. 
 * Employee: Ramón Martínez Nieto
 */
using System.Collections.Generic;
using UnityEngine;

/**
 * This class is a pooling of the enemies. 
 * Take the control of enemies die and alive.
 * Use this class to create new enemies, in case there is no dead in the list, instantiate 
 * other one. 
 * 
 * @author Ramón Martínez Nieto
 */
public class EnemyRecovery : MonoBehaviour
{
     /**
     * Singleton Instance 
     */
    public static EnemyRecovery Instance; 

    private List<Enemy> enemiesDied = new List<Enemy>();

    private List<Enemy> enemiesAlive = new List<Enemy>();

    /**
     *  Get the count how many enemies are alive in the scene. 
     *  
     *  @return int Count of the alive enemies 
     */
    public int GetEnemiesAlive() { return enemiesAlive.Count; }

    private void Start()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);
    }

    /**
     * Method to save a new enemy when it is eliminated of the scene. 
     * Remove of the enemies alive list and put it in the enmies died. 
     */
    public void SaveEnemyDie(Enemy enemy) {
        enemiesDied.Add(enemy);

        if (enemiesAlive.Contains(enemy)) 
        {
            enemiesAlive.Remove(enemy);
        }
    }

    /**
     * Method to clear enemies died list, util when the game finish to restart it.
     */
    public void ClearEnemyList() => enemiesDied.Clear();

    /**
     * Method to recovery a enemy when need a new one. First get the enemy of the enmiesDied list, 
     * in case theres in no one instantiate a new enemy. 
     * 
     * Very Impotant: The enemy prefab has to have the same name as the class.
     * 
     * This is the most beatifull method that I made.
     * 
     */
    public void RecoveryEnemy<T>(float x, float y) where T : Enemy
    {
        int index = GetEnemy<T>();

        if (index == -1)
        {
            //Look this is so nice, remember that the enemy prefab has to have the same name as the class.
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

    /**
     * Method to get a enemy of the list, only when there is one.  
     * 
     * @return int index of the enemy in the list
     */
    private int GetEnemy<T>() 
    {
        for(int i = 0; i < enemiesDied.Count; i++)
            if (enemiesDied[i].GetType() == typeof(T)) return i;
        
        return -1; 
    }
}
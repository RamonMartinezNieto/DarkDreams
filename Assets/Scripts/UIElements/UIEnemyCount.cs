/**
 * Department: Game Developer
 * File: UIEnemyCount.cs
 * Objective: Show to the player how much enemies are in the scene.
 * Employee: Ramón Martínez Nieto
 */

using TMPro;
using UnityEngine;

/**
 * 
 * This class show to the player how much enemies are in the scene using a label
 * 
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class UIEnemyCount : MonoBehaviour
{
    /**
     * Variable of the class EnemyRecovery with the Array with all enemies 
     */
    [Tooltip("Add GameObject with the EnemyRecovery class")]
    public EnemyRecovery er; 

    private TMP_Text labelEnemiesCount;

    private void Awake()
    {
        labelEnemiesCount = gameObject.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        labelEnemiesCount.text = er.GetEnemiesAlive().ToString();
    }
}

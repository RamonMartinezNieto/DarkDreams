using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMPro;
using UnityEngine;

public class UIEnemyCount : MonoBehaviour
{
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

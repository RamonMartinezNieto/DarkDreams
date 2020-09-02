using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPooling : MonoBehaviour
{
    public static ShootPooling Instance;

    private List<Shot> shotsToUse = new List<Shot>();

    private void Start()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);
    }

    public void SaveShoot(Shot shot) => shotsToUse.Add(shot);

    public void ClearEnemyList() => shotsToUse.Clear();

    public void RecoveryShot<T>(Vector3 firePosition, Quaternion q) where T : Shot
    {
        int index = GetShot<T>();

        if (index == -1)
        {
            string pathPrefab = $"Prefabs/Shots/Shoot";
            GameObject shot = Instantiate(Resources.Load(pathPrefab, typeof(GameObject)), firePosition, q) as GameObject;
            Shot s = shot.GetComponent<Shot>();

        }
        else
        {
            Shot e = shotsToUse[index];
            shotsToUse.RemoveAt(index);
        }
    }

    private int GetShot<T>()
    {
        for (int i = 0; i < shotsToUse.Count; i++)
            if (shotsToUse[i].GetType() == typeof(T)) return i;

        return -1;
    }
}

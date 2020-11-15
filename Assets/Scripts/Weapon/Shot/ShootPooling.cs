/**
 * Department: Game Developer
 * File: ShootPooling.cs
 * Objective: Have a pooling of all shoots
 * Employee: Ramón Martínez Nieto
 */
using System.Collections.Generic;
using UnityEngine;




/**
 * TODO: Need refactor and implement. 
 * 
 * This clas pretend have a pooling of all shoots like enemies pooling. 
 * THIS CLASS HAS NOT YET IMPLEMENTED 
 * 
 * 
 * @author Ramón Martínez Nieto
 * @version X.X.X
 */
public class ShootPooling : MonoBehaviour
{
    /**
     * Singleton Instance 
     */
    public static ShootPooling Instance;

    private List<Shot> shotsToUse = new List<Shot>();
    private void Start()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);
    }

    /**
     * Method to save a shoot (the shoot was destroyed)  
     */
    public void SaveShoot(Shot shot) => shotsToUse.Add(shot);

    /**
     * Method to clear the list 
     */
    public void ClearShootList() => shotsToUse.Clear();

    /**
     * Method to recovery shot or instantiate a new shot (depens the array) 
     */
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

    /**
     * Method to get shoot of the list 
     */
    private int GetShot<T>()
    {
        for (int i = 0; i < shotsToUse.Count; i++)
            if (shotsToUse[i].GetType() == typeof(T)) return i;

        return -1;
    }
}

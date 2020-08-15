using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObject : MonoBehaviour
{

    private static readonly GameObject[] cachableObjects = new GameObject[10];

    public GameObject bullet;
    public GameObject shield;
    public GameObject heart; 

    void Awake()
    {
        cachableObjects[0] = bullet;
        cachableObjects[1] = bullet;
        cachableObjects[2] = bullet;
        cachableObjects[3] = shield;
        cachableObjects[4] = shield;
        cachableObjects[5] = heart;
        cachableObjects[6] = heart;
        cachableObjects[7] = null;
        cachableObjects[8] = null;
        cachableObjects[9] = null;
    }

    public static void InstantiateCachableObject(Vector3 enemyPosition) {
        int element = Random.Range(0, 9);
        
        if (cachableObjects[element] != null)
        {
            GameObject cachable = Instantiate(cachableObjects[element]);
            cachable.GetComponent<Cachable>().SetPosition(enemyPosition); 
        } 
    }
}

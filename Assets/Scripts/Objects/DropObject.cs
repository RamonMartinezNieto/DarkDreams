/**
 * Department: Game Developer
 * File: DropObject.cs
 * Objective: This class pretends to give the functionality to be able to drop objects on the floor.
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;

/**
 * This class provide all logic to drop an random object when a enemy die. 
 * The drop depens of the array.
 * 
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class DropObject : MonoBehaviour
{

    private static readonly GameObject[] cachableObjects = new GameObject[10];

    /**
     * Add Prefab of the secondary bullet principal's weapon
     */
    [Tooltip("Add Prefab of the secondary principal's bullet")]
    public GameObject bullet;

    /**
     * GameObject with the shield cachable object
     */
    [Tooltip("Add Prefab of the principal bullet")]
    public GameObject shield;

    /**
     * GameObject with the heart cachable object
     */
    [Tooltip("Add Prefab of the principal bullet")]
    public GameObject heart;

    /**
     * Add Prefab of the secondary bullet shotgun's weapon
     */
    [Tooltip("Add Prefab of the secondary bullet shotgun's weapon")]
    public GameObject bulletSecondary; 

    static Transform parent; 

    void Awake()
    {
        //transform of the current GameObject
        parent = transform; 

        cachableObjects[0] = bullet;
        cachableObjects[1] = null;
        cachableObjects[2] = null;
        cachableObjects[3] = shield;
        cachableObjects[4] = null;
        cachableObjects[5] = heart;
        cachableObjects[6] = null;
        cachableObjects[7] = bulletSecondary;
        cachableObjects[8] = null;
        cachableObjects[9] = null;
    }

    /**
     * This method get a random Object of the Array and instantiate 
     */
    public static void InstantiateCachableObject(Vector3 enemyPosition) {
        int element = Random.Range(0, 9);
        
        if (cachableObjects[element] != null)
        {
            GameObject cachable = Instantiate(cachableObjects[element], parent);
            cachable.GetComponent<Cachable>().SetPosition(enemyPosition); 
        } 
    }
}

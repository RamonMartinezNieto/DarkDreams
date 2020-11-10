using UnityEngine;

public class DropObject : MonoBehaviour
{

    private static readonly GameObject[] cachableObjects = new GameObject[10];

    public GameObject bullet;
    public GameObject shield;
    public GameObject heart;
    public GameObject bulletSecondary; 

    static Transform parent; 

    void Awake()
    {
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

    public static void InstantiateCachableObject(Vector3 enemyPosition) {
        int element = Random.Range(0, 9);
        
        if (cachableObjects[element] != null)
        {
            GameObject cachable = Instantiate(cachableObjects[element], parent);
            cachable.GetComponent<Cachable>().SetPosition(enemyPosition); 
        } 
    }
}

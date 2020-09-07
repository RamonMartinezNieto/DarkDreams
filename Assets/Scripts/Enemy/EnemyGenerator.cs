
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyGenerator : MonoBehaviour
{
    public static EnemyGenerator Instance;
    
    public Tilemap tileMap;

    private EnemyRecovery er;

    private Vector2 doorOnePosition;
    private Vector2 doorTwoPosition;
    private Vector2 doorThreePosition;
    private Vector2 doorFourPosition;
    private Vector2 centerPosition;

    private float timeBetweenEnemies = .3f;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }


    void Start()
    {
        er = GetComponent<EnemyRecovery>();

        doorOnePosition = new Vector2(9.61f, -3.27f);
        doorTwoPosition = new Vector2(-7f, -2.2f);
        doorThreePosition = new Vector2(-7f, 5f);
        doorFourPosition = new Vector2(6.8f, 4.6f);
        centerPosition = new Vector2(0f, 0.7f);

        //6 enemies in the center
        createEnemiesCenter<EnemySkeleton>(2);
        createEnemiesCenter<EnemySkeletonArcher>(2);
        createEnemiesCenter<EnemyBat>(2);
        
    }

    //Note: forEachDoor is how many enemies respanw of every type forEachDoor = 3 = 12 enemies // 4 doors * 3 enemies.
    public void GenerateEnemies(int quantityTotal, int enemiesInDoors)
    {
        int randomEnemies = quantityTotal - (enemiesInDoors * 3);  //3 types of enemies
        int r = Random.Range(4, randomEnemies);
        int r2 = Random.Range(r + 4, randomEnemies - 4);
        int r3 = quantityTotal - r2;

        
        StartCoroutine(createEnemieForEachDorr<EnemySkeleton>(enemiesInDoors));
        StartCoroutine(createEnemieForEachDorr<EnemySkeletonArcher>(enemiesInDoors));
        StartCoroutine(createEnemieForEachDorr<EnemyBat>(enemiesInDoors));
        
        StartCoroutine(createEnemieRandomPlace<EnemySkeleton>(r));
        StartCoroutine(createEnemieRandomPlace<EnemySkeletonArcher>(r2));
        StartCoroutine(createEnemieRandomPlace<EnemyBat>(r3));
    }

    private void createEnemiesCenter<T>(int quantity) where T : Enemy
    {
        for (int a = 0; a < quantity; a++)
            er.RecoveryEnemy<T>(centerPosition.x, centerPosition.y);
    }

    private IEnumerator createEnemieForEachDorr<T>(int quantity) where T : Enemy
    {
        var quant = quantity / 4;

        for (int a = 0; a < quant; a++)
        {
            er.RecoveryEnemy<T>(doorOnePosition.x, doorOnePosition.y);
            yield return new WaitForSeconds(timeBetweenEnemies);
        }

        for (int b = 0; b < quant; b++)
        {
            er.RecoveryEnemy<T>(doorTwoPosition.x, doorTwoPosition.y);
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
        for (int c = 0; c < quant; c++)
        {
            er.RecoveryEnemy<T>(doorThreePosition.x, doorThreePosition.y);
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
        for (int z = 0; z < quant; z++)
        {
            er.RecoveryEnemy<T>(doorFourPosition.x, doorFourPosition.y);
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }

    
    private IEnumerator createEnemieRandomPlace<T>(int quantity) where T : Enemy
    {
         for (int h = 0; h < quantity; h++)
         {
            Vector3 a = createRandomCoordenates();

            while (ExclusionArea.CheckCoordinates(a.x, a.y))
            {
                a = createRandomCoordenates();
            }
            
            er.RecoveryEnemy<T>(a.x, a.y);

            yield return new WaitForSeconds(timeBetweenEnemies);
         }
        yield return new WaitForSeconds(timeBetweenEnemies);
    }

    private Vector3 createRandomCoordenates()
    {
        var coordenates = tileMap.GetCellCenterWorld(new Vector3Int(
                      Random.Range(tileMap.cellBounds.xMin + 1, tileMap.cellBounds.xMax - 1),
                      Random.Range(tileMap.cellBounds.yMin + 1, tileMap.cellBounds.yMax - 1),
                          0));

        return coordenates; 
    }
}
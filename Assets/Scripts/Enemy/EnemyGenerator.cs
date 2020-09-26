/**
 * Department: Game Developer
 * File: EnemyGenerator.cs
 * Objective: Generate enemies in the map.
 * Employee: Ramón Martínez Nieto
 */
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This class provide all necesary to generate enemies in arround the map and on the doors
 * 
 * @author Ramón Martínez Nieto
 */
public class EnemyGenerator : MonoBehaviour
{
    /**
     * Singleton Instance 
     */
    public static EnemyGenerator Instance;

    /**
     * Tile map of the ground. 
     */
    [Tooltip("Add Tilemap of the ground.")]
    public Tilemap tileMap;

    private EnemyRecovery er;

    //Variables of the door's vectors.
    private Vector2 doorOnePosition;
    private Vector2 doorTwoPosition;
    private Vector2 doorThreePosition;
    private Vector2 doorFourPosition;

    private Vector2 centerPosition;

    //Time between respanws
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

        //6 enemies in the center in the first call
        createEnemiesCenter<EnemySkeleton>(2);
        createEnemiesCenter<EnemySkeletonArcher>(2);
        createEnemiesCenter<EnemyBat>(2);
    }

    
    /**
     * Class to generate enemies. 
     * Note: forEachDoor is how many enemies respanw of every type forEachDoor = 3 = 12 enemies // 4 doors * 3 enemies.
     * 
     * @param quantityTotal int Total to generate enemis.
     * @param enemiesInDorrs How many enemies appears in the doors
     */
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

    /**
     * To create new enemies in the center of the map
     */ 
    private void createEnemiesCenter<T>(int quantity) where T : Enemy
    {
        for (int a = 0; a < quantity; a++)
            er.RecoveryEnemy<T>(centerPosition.x, centerPosition.y);
    }

    /**
     * To create enemies in the doors 
     */
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

    /**
     * To create enemies in random position 
     */
    private IEnumerator createEnemieRandomPlace<T>(int quantity) where T : Enemy
    {
         for (int h = 0; h < quantity; h++)
         {
            Vector3 a = createRandomCoordenates();

            //Check if the new coordinates are in the Sprite, in this case get new coordinates
            while (StaticExclusionArea.CheckCoordinatesSprite(a.x, a.y))
            {
                a = createRandomCoordenates();
            }
            
            //Use RecoveryEnemy to get new enemy (a die enemy or new instance) 
            er.RecoveryEnemy<T>(a.x, a.y);

            yield return new WaitForSeconds(timeBetweenEnemies);
         }
        yield return new WaitForSeconds(timeBetweenEnemies);
    }

    /**
     * Method to get new coordinates  
     */
    private Vector3 createRandomCoordenates()
    {
        var coordenates = tileMap.GetCellCenterWorld(new Vector3Int(
                      Random.Range(tileMap.cellBounds.xMin + 1, tileMap.cellBounds.xMax - 1),
                      Random.Range(tileMap.cellBounds.yMin + 1, tileMap.cellBounds.yMax - 1),
                          0));

        return coordenates; 
    }
}
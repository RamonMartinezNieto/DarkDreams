/**
 * Department: Game Developer
 * File: Zone.cs
 * Objective: Create an abstract element to create diferents zones in the map
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;


/**
 * This class is an abstract class to create diferents zones in the map. Use this if 
 * you want create a new zone to respawn enemies in determinate point. 
 * 
 * Currently I do not use this method, I use an automatic respawn in all map. 
 * This method is intended for a history mode.
 * 
 *  @author Ramón Martínez Nieto
 *  @deprecated
 */
public abstract class Zone : MonoBehaviour
{
    private EnemyRecovery er;

    /**
     * Quantity of enemies to repoblate. 
     * 
     * @param int 
     */
    protected int createEnemies { get; set;  } = 45;

    /**
     * First coordinate of the squeare zone. 
     * 
     * @param float X coordinate
     */
    protected float xRange1 { get; set; }
    /**
     * Second coordinate of the squeare zone. 
     * 
     * @param float X coordinate
     */
    protected float xRange2 { get; set; }
    /**
     * First coordinate of the squeare zone. 
     * 
     * @param float Y coordinate
     */
    protected float yRange1 { get; set; }
    /**
     * Second coordinate of the squeare zone. 
     * 
     * @param float Y coordinate
     */
    protected float yRange2 { get; set; }


    void Start()
    {
        er = FindObjectOfType<EnemyRecovery>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < createEnemies; i++)
            {
                float x = Random.Range(xRange1, xRange2);
                float y = Random.Range(yRange1, yRange2);

                int createEnemies2 = createEnemies / 2;
                int r = Random.Range(10, createEnemies2);
                int r2 = Random.Range(r + 10, createEnemies);

                if (i < r)
                {
                    er.RecoveryEnemy<EnemySkeleton>(x, y);
                }
                else if (i >= r2)
                {
                    er.RecoveryEnemy<EnemySkeletonArcher>(x, y);
                }
                else
                {
                    er.RecoveryEnemy<EnemyBat>(x, y);
                }
            }
            gameObject.SetActive(false);
        }
    }

    /**
     * Method to make the fog disappear slowly. 
     */
    public void dispearfog(ParticleSystem fog)
    {
        AnimationCurve curve = new AnimationCurve(new Keyframe(1f, 1f), new Keyframe(0.8f, 0.8f), new Keyframe(0.2f, 0.2f));
        var szOlt = fog.sizeOverLifetime;
        szOlt.size = new ParticleSystem.MinMaxCurve(2f, curve);

        var main = fog.main;
        main.loop = false;
    }
}

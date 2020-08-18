using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Zone : MonoBehaviour
{
    private EnemyRecovery er;

    protected int createEnemies { get; set;  } = 45;

    protected float xRange1 { get; set; }
    protected float xRange2 { get; set; }
    protected float yRange1 { get; set; }
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
                    er.RecoverSkeletonArcher(x, y);
                }
                else if (i >= r2)
                {
                    er.RecoverSkeleton(x, y);
                }
                else
                {
                    er.RecoverBat(x, y);
                }
            }
            gameObject.SetActive(false);
        }
    }

    public void dispearfog(ParticleSystem fog)
    {
        AnimationCurve curve = new AnimationCurve(new Keyframe(1f, 1f), new Keyframe(0.8f, 0.8f), new Keyframe(0.2f, 0.2f));
        var szOlt = fog.sizeOverLifetime;
        szOlt.size = new ParticleSystem.MinMaxCurve(2f, curve);

        var main = fog.main;
        main.loop = false;
    }
}

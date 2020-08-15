using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ZoneOne : MonoBehaviour
{
    private EnemyRecovery er;

    public ParticleSystem fogZoneOne;

    void Start()
    {
        er = FindObjectOfType<EnemyRecovery>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < 15; i++)
            {
                float x = Random.Range(-0.2f, 5.3f);
                float y = Random.Range(-1.28f, -5.5f);

                if (i < 5)
                {
                    er.RecoverSkeletonArcher(x, y);
                }
                else if (i >= 5 && i < 10)
                {
                    er.RecoverSkeleton(x, y);
                }
                else
                {
                    er.RecoverBat(x, y);
                }
            }

            dispearfog();

            gameObject.SetActive(false);
        }
    }

    public void dispearfog()
    {
        AnimationCurve curve = new AnimationCurve(new Keyframe(1f, 1f), new Keyframe(0.8f, 0.8f), new Keyframe(0.2f, 0.2f));
        var szOlt = fogZoneOne.sizeOverLifetime;
        szOlt.size = new ParticleSystem.MinMaxCurve(2f, curve);

        var main = fogZoneOne.main;
        main.loop = false;
    }
}

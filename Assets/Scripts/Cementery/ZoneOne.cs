using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ZoneOne : Zone
{
    public ParticleSystem fogZoneOne;

    private void Awake()
    {
        createEnemies = 60;

        //Set rectangle position to drop enemies
        xRange1 = -.2f;
        xRange2 = 5.3f;
        yRange1 = -1.28f;
        yRange2 = -5.5f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dispearfog(fogZoneOne);
    }
}

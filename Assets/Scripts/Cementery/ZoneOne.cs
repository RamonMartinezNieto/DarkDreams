/**
 * Department: Game Developer
 * File: ZoneOne.cs
 * Objective: Implement first zone in the map to repopulate enemies.
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;

/**
 * First zone on the map, put coordinates in Awake method. 
 * 
 *  @author Ramón Martínez Nieto
 *  @deprecated
 *  @version 0.0.1
 */
public class ZoneOne : Zone
{
    /**
     * Specific fog, needed it to dispear slowly. Check that there is visible in the scene.
     */
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

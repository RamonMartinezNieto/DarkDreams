/**
 * Department: Game Developer
 * File: ZonTwo.cs
 * Objective:  Implement second zone in the map to repopulate enemies.
 * Employee: Ramón Martínez Nieto
 */

/**
 * Scond zone on the map, put coordinates in Awake method. 
 * 
 *  @author Ramón Martínez Nieto
 *  @deprecated
 */
public class ZoneTwo : Zone
{
    private void Awake()
    {
        createEnemies = 10;


        xRange1 = -6.58f ;
        xRange2 = -10.37f ;

        yRange1 = -0.37f;
        yRange2 = 3.23f;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

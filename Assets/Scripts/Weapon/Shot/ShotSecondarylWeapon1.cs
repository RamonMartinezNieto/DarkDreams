﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ShotSecondarylWeapon1 : Shot
{
    
    private UIBullets secondaryShootsUI; 

    //Need to set velocity, damage and destroy animation
    void Start()
    {
        secondaryShootsUI = GameObject.Find("SecondaryShootsUI").GetComponent<UIBullets>(); 

        restBullet();
        MovingShot();
    }

    void restBullet() 
    {
        secondaryShootsUI.disableBullet(UIBullets.CurrentBullets - 1);
    }




}
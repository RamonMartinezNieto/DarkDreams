/**
 * Department: Game Developer
 * File: ShotPrincipalWeapon.cs
 * Objective: Create a specific bullet. Principal bullet of the principal weapon
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;

/**
 * 
 * Specific shoot. Principal shoot of the principal weapon.
 * Use method Start to inicialice this shoot, movement and other things. 
 * 
 * 
 * @see Shot
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class ShotPrincipalWeapon : Shot
{
    /**
     * Variable to mark in Unity if this shoot is in the menu. 
     * The comportment is different in the menu.
     */
    public bool IsMenuPrincipalShoot;

    void Start()
    {
        MovingShot();

        if (!IsMenuPrincipalShoot)
        {
            float variationPitch = Random.Range(-.1f, .2f);
            float variationVol = Random.Range(-.05f, .05f);

            SoundManager.Instance.PlayEffect("shootPrincipal",variationPitch, variationVol);
        }
    }

}

/**
 * Department: Game Developer
 * File: WeaponListController.cs
 * Objective: Have a object to create a wepons in the UI
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/**
 * 
 * This class only represent a class to create a weapon to show in the UI game. 
 * Use this class when you need show a weapon, the object is this :D. 
 * 
 * @see ControlWeapons
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class WeaponListController : MonoBehaviour
{
    /**
     * Texture of the Weapon
     */
    [Tooltip("Add RawImage of the weapon texture")]
    public RawImage weaponTexture;

    /**
     * Number of the weapon (the number is using to change the weapons)
     */
    [Tooltip("Put the number of the weapon")]
    public int number;

    /**
     * TMP_Text on the number of weapons is visible
     */
    [Tooltip("Add TMP_Text of the weapon")]
    public TMP_Text numberWeaponText;   
}

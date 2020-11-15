/**
 * Department: Game Developer
 * File: UIBullet.cs
 * Objective: Control secondary bullets of the principal weapon.
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;
using UnityEngine.UI;

/**
 * 
 * This class is a especific class to control UIBullets in the scene, to know how much bullets have the player.
 * This class control how much bullets have the player and control if they can use the bullets (they can if 
 * they have bullets obviously
 * 
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class UIBullets : MonoBehaviour
{
    //TODO change set to private
    /**
     * Static CurrentBullets, all weapons start with 5 bullets, an this is the top
     */
    public static int CurrentBullets { get; private set; } = 5;

    /**
     * Sprite to show a texture when the bullet is used
     */
    [Tooltip("Add the sprite when no have bullet (B/W)")]
    public Sprite disableBulletTexture;
    
    /**
     * Texture when the bullet is avaible  
     */
    [Tooltip("Add Sprite of the bullet active (with colors)")]
    public Sprite enableBulletTexture;
    
    /**
     * RawImage with the canvasBullets (background)  
     */
    [Tooltip("Add RawImage with the canvas bullets")]
    public RawImage[] canvasBullets;

    private void Start()
    {
        CurrentBullets = 5; 
    }

    /**
     * Method to disable a bullet when it is used. 
     */
    public void disableBullet(int index) 
    {
        canvasBullets[index].texture = disableBulletTexture.texture;
        CurrentBullets--; 
    }

    /**
     * Method to active a bullet when catch one. 
     */
    public void enableBullet(int index) 
    {
        canvasBullets[index].texture = enableBulletTexture.texture;
        CurrentBullets++; 
    }   
}
/**
 * Department: Game Developer
 * File: ExclusionArea.cs
 * Objective: Object with add a new exlusión area to respawn enemies.
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;


/**
 * This class is whole a componente to add a new exlusión area when the enemies 
 * can't respawn.
 * 
 * Deppen of the SpriteRenderer. 
 */
public class ExclusionArea : MonoBehaviour
{
    /**
     * SpriteRenderer that represent the exclusion area.
     */
    private SpriteRenderer spriteRenderExclusionArea;

    /**
     * @see  StaticExclusionArea#AddNewSprite
     */
    private void Start()
    {
        spriteRenderExclusionArea = gameObject.GetComponent<SpriteRenderer>();

        StaticExclusionArea.AddNewSprite(spriteRenderExclusionArea);
    }
}


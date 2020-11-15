/**
 * Department: Game Developer
 * File: StaticExclusionArea.cs
 * Objective: Recopilatory of all areas to create correctly the exclusion area.
 * Employee: Ramón Martínez Nieto
 */
using System.Collections.Generic;
using UnityEngine;

/**
 * In this clas there is the list of all exclusión areas in the scene (adding with the script ExclusionArea). 
 * Theres is a method to check if a coordinate is in the exclusion.
 * 
 * @see ExclusionArea
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class StaticExclusionArea : MonoBehaviour
{
    public static StaticExclusionArea Instance; 

    private readonly static List<SpriteRenderer> listSpriteRenderers = new List<SpriteRenderer>();

    //Singlenton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
     //   DontDestroyOnLoad(Instance);
    }

    /**
     * Method to add new SpriteRender in the list where all SpriteRenders are
     * 
     * @see ExclusionArea
     */
    public static void AddNewSprite(SpriteRenderer sr)
    {
        //lock the List to increase security
        lock (listSpriteRenderers)
        {
            listSpriteRenderers.Add(sr);
        }
    }

    /**
     * Method to check if a cordinate is inside the SpriteRenderer 
     */
    public static bool CheckCoordinatesSprite(float x, float y)
    {
        foreach (SpriteRenderer sr in listSpriteRenderers) 
        {
            if (sr.bounds.Contains(new Vector3(x, y, 1f)))
                return true;
        }
        return false; 
    }

    /**
     * Method to reset list, use it when restart the scene
     */
    public static void ResetListExclusionArea() => listSpriteRenderers.Clear();
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static void AddNewSprite(SpriteRenderer sr)
    {
        lock (listSpriteRenderers)
        {
            listSpriteRenderers.Add(sr);
        }
    }

    public static bool CheckCoordinatesSprite(float x, float y)
    {
        foreach (SpriteRenderer sr in listSpriteRenderers) 
        {
            if (sr.bounds.Contains(new Vector3(x, y, 1f)))
                return true;
        }
        return false; 
    }

    public static void ResetListExclusionArea() => listSpriteRenderers.Clear();
}
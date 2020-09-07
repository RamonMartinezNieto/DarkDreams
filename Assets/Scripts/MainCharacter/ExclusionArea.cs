using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclusionArea : MonoBehaviour
{
    public static SpriteRenderer spriteRenderExclusionArea;

    private void Start()
    {
        spriteRenderExclusionArea = GetComponent<SpriteRenderer>();
    }

    public static bool checkCorrdinates(float x, float y) 
    {
        if (spriteRenderExclusionArea.bounds.Contains(new Vector3(x, y, 1f)))
            return true;
        else
            return false;
    }

}


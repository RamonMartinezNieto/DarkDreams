using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclusionArea : MonoBehaviour
{
    private SpriteRenderer spriteRenderExclusionArea;

    private void Start()
    {
        spriteRenderExclusionArea = gameObject.GetComponent<SpriteRenderer>();

        StaticExclusionArea.AddNewSprite(spriteRenderExclusionArea);
    }

    public bool CheckCoordinates(float x, float y) 
    {
        if (spriteRenderExclusionArea.bounds.Contains(new Vector3(x, y, 1f)))
            return true;
        else
            return false;
    }
}


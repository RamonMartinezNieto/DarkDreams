using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOrderInLayer : MonoBehaviour
{

    // Order in layer ALL objects in scene, that's so nice because the character is behind or front of the objects. 
    // But... I think is slower than other options

    SpriteRenderer [] rendererFernce;

    void Awake()
    {
        rendererFernce = FindObjectsOfType<SpriteRenderer>(); 
    }

    private void FixedUpdate()
    {
        foreach (SpriteRenderer renderer in rendererFernce)
        {
            renderer.sortingOrder = (int)(renderer.transform.position.y * -100);
        }
    }

}

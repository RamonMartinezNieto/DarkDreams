using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{


    public Transform crosshairTransform;


    void Update()
    {
        //detect mouse position
        crossHairPosition(crosshairTransform);

    }


    private void crossHairPosition(Transform corsshariTransform)
    {
        corsshariTransform.position = getMousePosition();
    }


    public static Vector3 getMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
    }



}


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


    //Return number of the direction from object position
    // 0: S // 1 : SE // 2:E // 3:NE // 4:N // 5: NW // 6:W // 7:SW 
    public float CrossHairAngle(Vector3 objectPosition){
        
        Vector3 crossHairDir = CrossHairDirection();

        Vector2 normDir = objectPosition - crossHairDir;
        float step = 360f / 8;
        float halfstep = step / 2;
        
        float angle = Vector2.SignedAngle(Vector2.up, normDir);

        angle += halfstep;
        
        if (angle < 0){
            angle += 360;
        } 

        float stepCount = angle / step;

                
        return Mathf.FloorToInt(stepCount);
    }


    //Check direction of the crosshair
    private Vector3 CrossHairDirection(){
        Vector3 crossHairDir = new Vector3(0.0f,0.0f,0.0f);

        if(crosshairTransform.position.x < 0){
            crossHairDir.x = crosshairTransform.position.x - 0.1f; 
        } else {
            crossHairDir.x = crosshairTransform.position.x + 0.1f; 
        }


        if(crosshairTransform.position.y < 0 ){
            crossHairDir.y = crosshairTransform.position.y - 0.1f; 
        } else {
            crossHairDir.y = crosshairTransform.position.y + 0.1f; 
        }

        return crossHairDir;
    }

}


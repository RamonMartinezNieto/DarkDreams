/**
 * Department: Game Developer
 * File: MiniMapCamera.cs
 * Objective: Control mini map camera
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;

/**
 * Class to add to mini camera to control it . 
 */
public class MinimapCamera : MonoBehaviour
{

    /**
     * Transform of the player (target of the mini camera)  
     */
    [Tooltip("Put transform player")]
    public Transform player;
    
    private void LateUpdate()
    {
        Vector3 newPos = player.position;
        newPos.z = -10; 

        transform.position = newPos;
    }
}

/**
 * Department: Game Developer
 * File: MainCamera.cs
 * Objective: Control the camera's movement.
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;

/**
 * Class to control the basic movement of the camera
 * 
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class MainCamera : MonoBehaviour
{
    /**
     * GameObject that the camera follow
     */
    [Tooltip("GameObject that the camera follow")]
    public GameObject FollowGameObject;

    private Vector3 tranformGameObject;

    void Awake()
    {
        tranformGameObject = FollowGameObject.GetComponent<Transform>().position;
    }

    void LateUpdate()
    {
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        tranformGameObject = FollowGameObject.GetComponent<Transform>().position;
        tranformGameObject.z = -10f; 
        
        gameObject.transform.position = tranformGameObject;
    }
}
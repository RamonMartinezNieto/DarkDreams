using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject FollowGameObject;

    private Vector3 tranformGameObject;

    void Awake()
    {
        tranformGameObject = FollowGameObject.GetComponent<Transform>().position;
    }

    void Update()
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

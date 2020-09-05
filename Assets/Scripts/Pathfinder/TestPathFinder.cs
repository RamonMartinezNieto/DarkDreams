using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestPathFinder : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;
    private bool stop;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            agent.speed += 0.1f;
        }
        if ((transform.position - target.transform.position).magnitude < 1 && !stop)
        {
            stop = true;
            //target.GetComponent<Navigate>().enabled = false;

        }
        Debug.Log("moviendose");
        agent.SetDestination(target.transform.position);
    }
}

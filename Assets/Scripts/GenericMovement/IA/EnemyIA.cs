using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour
{
    public GameObject target;

    private NavMeshAgent agent;
    private bool stop;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        float a = Vector3.Distance(transform.position, target.GetComponent<Transform>().position);

        if (a <= 2f)
        {
            agent.enabled = true;
            agent.SetDestination(target.transform.position);
        }
        else if(a< 0.2f || a > 2f)
        {
            agent.enabled = false;
            Debug.Log("que siga con su vida");
        }
    }
}

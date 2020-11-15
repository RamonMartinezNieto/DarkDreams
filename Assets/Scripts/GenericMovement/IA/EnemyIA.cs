/**
 * Department: Game Developer
 * File: EnemyIA.cs
 * Objective: Adding AI to enemies.
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;
using UnityEngine.AI;

/**
 * Class to adding AI to the enemies.
 * 
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class EnemyIA : MonoBehaviour
{
    /**
     * Target of the enemy, normaly the player.
     */
    [Tooltip("Player or other GameObject to be targeted by the enemy")]
    public GameObject target;

    private NavMeshAgent agent;

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

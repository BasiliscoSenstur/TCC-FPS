using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;

    Vector3 targetPosition;
    public NavMeshAgent agent;

    public float chaseDistance, stopChaseDistance;
    bool isChasing;

    void Start()
    {
        
    }

    void Update()
    {
        targetPosition = PlayerController.instance.transform.position;
        targetPosition.y = transform.position.y;

        if (!isChasing)
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= chaseDistance)
            {
                isChasing = true;
            }
        }
        else
        {
            agent.destination = targetPosition;

            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > stopChaseDistance)
            {
                isChasing = false;
            }
        }
    }
}

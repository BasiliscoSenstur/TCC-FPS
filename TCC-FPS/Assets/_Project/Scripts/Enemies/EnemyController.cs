using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;

    Vector3 targetPosition, startPosition;
    public NavMeshAgent agent;

    public float chaseDistance, stopChaseDistance;
    bool isChasing;

    public float chaseCounter;
    void Start()
    {
        startPosition = transform.position;
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

            if (chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;
            }
            if (chaseCounter <= 0)
            {
                chaseCounter = 0;
                agent.destination = startPosition;
            }
        }
        else
        {
            agent.destination = targetPosition;

            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > stopChaseDistance)
            {
                isChasing = false;
                chaseCounter = 5f;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody body;

    public float chaseDistance, stopChaseDistance;
    bool isChasing;

    void Start()
    {
        
    }

    void Update()
    {
        if (!isChasing)
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= chaseDistance)
            {
                isChasing = true;
            }
        }
        else
        {
            transform.LookAt(PlayerController.instance.transform.position);

            body.velocity = transform.forward * moveSpeed;

            if(Vector3.Distance(transform.position, PlayerController.instance.transform.position)> stopChaseDistance)
            {
                isChasing = false;
            }
        }
    }
}

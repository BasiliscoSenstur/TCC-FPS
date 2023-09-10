using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool isChasing;
    [HideInInspector] public Vector3 startPosition, targetPosition;

    public float chaseDistance, stopChasingDistance;
    public float stopChasingCounter;

    public float aimTime, aimCounter;

    [Header("State Machine")]
    public Transform firePoint;
    public GameObject projectile;

    [Header("State Machine")]
    public string STATE;
    EnemyAbstract currentState;
    public EnemyIdle idle = new EnemyIdle();
    public EnemyChasing chasing = new EnemyChasing();
    public EnemyShooting shooting = new EnemyShooting();

    void Start()
    {
        startPosition = transform.position;

        currentState = idle;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.LogicsUpdate(this);
        STATE = currentState.ToString();
    }

    public void SwitchState(EnemyAbstract newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public void Shot(int amount)
    {
        if (Mathf.Abs(shooting.angle) < 30f)
        {
            for(int i = 0; i < amount; i++)
            {
                Instantiate(projectile, firePoint.position, firePoint.rotation);
            }
        }
    }
}

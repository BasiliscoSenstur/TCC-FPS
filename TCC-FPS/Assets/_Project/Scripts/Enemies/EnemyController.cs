using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("State Machine")]
    public string STATE;
    public EnemyAbstract currentState;
    public EnemyIdle enemyIdle = new EnemyIdle();
    public EnemyChasing enemyChasing = new EnemyChasing();

    [Header("Movement")]
    public NavMeshAgent agent;
    public Vector3 targetPosition, startPosition;

    [Header("Patrol")]
    public float chaseCounter;
    public float chaseDistance, stopChaseDistance;
    public bool isChasing;

    [Header("Shot")]
    public Transform firePoint;
    public GameObject bullet;
    public float timeBetweenShots;

    void Start()
    {
        startPosition = transform.position;
        currentState = enemyIdle;
        currentState.EnterState(this);
    }

    void Update()
    {
        targetPosition = PlayerController.instance.transform.position;
        targetPosition.y = transform.position.y;

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
        StartCoroutine(ShotCo(amount));
    }

    IEnumerator ShotCo(int amount)
    {
        for (int i = 0; i < amount; i++) 
        {
            Debug.Log("Shot:" + i);
            Projectile();
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void Projectile()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        //timeBetweenShots = 2f;
        Debug.Log("Enemy Shot");
    }
}

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
    public EnemyShooting enemyShooting = new EnemyShooting();

    [Header("Movement")]
    public NavMeshAgent agent;
    public Vector3 targetPosition, startPosition;

    [Header("Animation")]
    public Animator anim;
    public string currentAnimation;

    [Header("Patrol")]
    public float chaseCounter;
    public float chaseDistance, stopChaseDistance;
    public bool isChasing;

    [Header("Shot")]
    public int fireRate;
    public GameObject bullet;
    public Transform firePoint;
    public float timeBetweenShots;

    [Header("DEBUG")]
    public string chasingCounter;

    void Start()
    {
        startPosition = transform.position;
        currentState = enemyIdle;
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

    public void ChangeAnimation(string newAnimation)
    {
        if(currentAnimation == newAnimation)
        {
            return;
        }
        else
        {
            anim.Play(newAnimation);
            currentAnimation = newAnimation;
        }
    }

    public void Shot()
    {
        agent.destination = transform.position;
        ChangeAnimation("Enemy_Shoot");
        StartCoroutine(ShotCo());
    }
    IEnumerator ShotCo() 
    {
        Projectile();
        yield return new WaitForSeconds(10f);
        Debug.Log("Shot");
    }

    public void Projectile()
    {
        firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 0.5f, 0f));

        Vector3 targetDir = PlayerController.instance.transform.position - transform.position;
        float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

        if (Mathf.Abs(angle) < 30f)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            timeBetweenShots = 2f;
            Debug.Log("Enemy Shot");
        }
        else
        {
            timeBetweenShots = 2f;
        }
    }
}

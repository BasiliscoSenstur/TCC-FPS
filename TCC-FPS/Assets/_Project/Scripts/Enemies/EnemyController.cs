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
        firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 0.5f, 0f));

        Vector3 targetDir = PlayerController.instance.transform.position - transform.position;
        float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

        if (Mathf.Abs(angle) < 30f)
        {
            agent.destination = transform.position;
            ChangeAnimation("Enemy_Shot");

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

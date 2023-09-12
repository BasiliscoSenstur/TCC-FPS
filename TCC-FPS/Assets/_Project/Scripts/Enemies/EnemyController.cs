using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    public NavMeshAgent agent;
    public bool isChasing;
    [HideInInspector] public Vector3 startPosition, targetPosition;
    public float chaseDistance, stopChasingDistance;
    public float stopChasingCounter;
    public float aimTime, aimCounter;

    [Header("Animation")]
    public Animator anim;
    public string currentAnimation;

    [Header("Shot")]
    public Transform firePoint;
    public GameObject projectile;
    public int numberOfShots;
    public float fireRate;

    [Header("State Machine")]
    public string STATE;
    EnemyAbstract currentState;
    public EnemyIdle idle = new EnemyIdle();
    public EnemyChasing chasing = new EnemyChasing();
    public EnemyShooting shooting = new EnemyShooting();

    [Header("DEBUG")]
    public string SHOOTED;

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
        SHOOTED = shooting.counter.ToString();
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
        anim.gameObject.transform.localPosition = new Vector3(0f, -1f, 0f);
        anim.Play(newAnimation);
        currentAnimation = newAnimation;
    }

    public void Shot(int amount)
    {
        StartCoroutine(ShotCo(amount));
    }


    IEnumerator ShotCo(int amount)
    {
        if (Mathf.Abs(shooting.angle) < 30f)
        {
            for(int i = 0; i < amount; i++)
            {
                Instantiate(projectile, firePoint.position, firePoint.rotation);

                yield return new WaitForSeconds(fireRate);
            }
            Debug.Log("Shooted");
        }
    }
}

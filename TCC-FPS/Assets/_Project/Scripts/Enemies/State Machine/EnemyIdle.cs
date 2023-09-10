using System.Runtime.Serialization;
using UnityEngine;

public class EnemyIdle : EnemyAbstract
{
    public override void EnterState(EnemyController enemy)
    {
        enemy.ChangeAnimation("Enemy_Idle");
    }
    public override void LogicsUpdate(EnemyController enemy)
    {
        //Start Agro
        if (Vector3.Distance(enemy.transform.position, PlayerController.instance.transform.position) <= enemy.chaseDistance)
        {
            enemy.isChasing = true;
            enemy.SwitchState(enemy.chasing);
        }

        //Time to return to Start position
        if (enemy.stopChasingCounter > 0)
        {
            enemy.stopChasingCounter -= Time.deltaTime;
        }
        else
        {
            enemy.agent.destination = enemy.startPosition;
        }

        if (Vector3.Distance(enemy.transform.position, enemy.targetPosition) <= 0.2f || Vector3.Distance(enemy.transform.position, enemy.startPosition) <= 0.2f)
        {
            enemy.ChangeAnimation("Enemy_Idle");
        }
        else
        {
            enemy.ChangeAnimation("Enemy_Run");
        }
    }
    public override void ExitState(EnemyController enemy)
    {

    }
}

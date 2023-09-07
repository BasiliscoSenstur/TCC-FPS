using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyIdle : EnemyAbstract
{
    public override void EnterState(EnemyController enemy)
    {
        enemy.ChangeAnimation("Enemy_Idle");
    }
    public override void LogicsUpdate(EnemyController enemy)
    {
        if (Vector3.Distance(enemy.transform.position, PlayerController.instance.transform.position) <= enemy.chaseDistance)
        {
            enemy.SwitchState(enemy.enemyChasing);
        }

        if (enemy.chaseCounter > 0)
        {
            enemy.chaseCounter -= Time.deltaTime;
        }
        if (enemy.chaseCounter <= 0)
        {
            enemy.chaseCounter = 0;
            enemy.agent.destination = enemy.startPosition;
        }

        if(enemy.transform.position == enemy.startPosition) 
        {
            enemy.ChangeAnimation("Enemy_Idle");
        }
    }
    public override void ExitState(EnemyController enemy)
    {

    }
}

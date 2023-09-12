using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class EnemyChasing : EnemyAbstract
{
    public override void EnterState(EnemyController enemy)
    {
        enemy.aimCounter = enemy.aimTime;
        enemy.ChangeAnimation("Enemy_Run");
    }
    public override void LogicsUpdate(EnemyController enemy)
    {
        enemy.targetPosition = PlayerController.instance.transform.position;
        enemy.targetPosition.y = enemy.transform.position.y;

        if (Vector3.Distance(enemy.transform.position, PlayerController.instance.transform.position) >= 5)
        {
            enemy.agent.destination = enemy.targetPosition;
        }

        if (enemy.aimCounter > 0)
        {
            enemy.aimCounter -= Time.deltaTime;
        }
        else
        {
            enemy.aimCounter = 0;
            enemy.SwitchState(enemy.shooting);
        }

        //Lost target
        if (Vector3.Distance(enemy.transform.position, PlayerController.instance.transform.position) >= enemy.stopChasingDistance)
        {
            enemy.isChasing = false;
            enemy.stopChasingCounter = 5f;
            enemy.SwitchState(enemy.idle);
        }
    }
    public override void ExitState(EnemyController enemy)
    {

    }
}

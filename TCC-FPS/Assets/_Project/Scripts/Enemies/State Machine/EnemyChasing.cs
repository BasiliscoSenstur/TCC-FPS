using UnityEngine;

public class EnemyChasing : EnemyAbstract
{
    public override void EnterState(EnemyController enemy)
    {
        enemy.aimCounter = enemy.aimTime;
    }
    public override void LogicsUpdate(EnemyController enemy)
    {
        enemy.targetPosition = PlayerController.instance.transform.position;
        enemy.targetPosition.y = enemy.transform.position.y;

        enemy.agent.destination = enemy.targetPosition;

        if (enemy.aimCounter > 0)
        {
            enemy.SwitchState(enemy.shooting);
        }

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

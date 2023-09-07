using UnityEngine;

public class EnemyChasing : EnemyAbstract
{
    public override void EnterState(EnemyController enemy)
    {

    }
    public override void LogicsUpdate(EnemyController enemy)
    {
        enemy.agent.destination = enemy.targetPosition;

        if (enemy.timeBetweenShots > 0)
        {
            enemy.timeBetweenShots -= Time.deltaTime;
        }
        if (enemy.timeBetweenShots <= 0)
        {
            enemy.Shot(3);
            enemy.agent.destination = enemy.transform.position;
        }

        if (Vector3.Distance(enemy.transform.position, PlayerController.instance.transform.position) > enemy.stopChaseDistance)
        {
            enemy.chaseCounter = 5f;
            enemy.SwitchState(enemy.enemyIdle);
        }
    }
    public override void ExitState(EnemyController enemy)
    {

    }
}

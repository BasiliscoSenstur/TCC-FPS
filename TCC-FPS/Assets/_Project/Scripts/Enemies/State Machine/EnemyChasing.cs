using UnityEngine;

public class EnemyChasing : EnemyAbstract
{
    public float counter;
    public override void EnterState(EnemyController enemy)
    {
        enemy.ChangeAnimation("Enemy_Run");
        counter = enemy.timeBetweenShots;
    }
    public override void LogicsUpdate(EnemyController enemy)
    {
        enemy.targetPosition = PlayerController.instance.transform.position;
        enemy.targetPosition.y = enemy.transform.position.y;

        enemy.agent.destination = enemy.targetPosition;

        if (enemy.timeBetweenShots > 0)
        {
            enemy.timeBetweenShots -= Time.deltaTime;
        }
        if(enemy.timeBetweenShots <= 0)
        {
            enemy.SwitchState(enemy.enemyShooting);
        }


        if (Vector3.Distance(enemy.transform.position, PlayerController.instance.transform.position) > enemy.stopChaseDistance)
        {
            enemy.isChasing = false;
            enemy.chaseCounter = 5f;
            enemy.SwitchState(enemy.enemyIdle);
        }
    }
    public override void ExitState(EnemyController enemy)
    {

    }
}

using UnityEngine;

public class EnemyIdle : EnemyAbstract
{
    public override void EnterState(EnemyController enemy)
    {

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
    }
    public override void ExitState(EnemyController enemy)
    {

    }
}

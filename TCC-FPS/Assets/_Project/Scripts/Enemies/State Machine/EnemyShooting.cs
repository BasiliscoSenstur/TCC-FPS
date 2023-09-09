using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooting : EnemyAbstract
{
    float wait;
    public override void EnterState(EnemyController enemy)
    {
        enemy.ChangeAnimation("Enemy_Shoot");
        wait = 1;
    }
    public override void LogicsUpdate(EnemyController enemy)
    {
        for (int i = 0; i < enemy.fireRate; i++)
        {
            enemy.Shot();
        }

        if (wait > 0)
        {
            wait -= Time.deltaTime;
        }
        if (wait <= 0)
        {
            wait = 0;
            enemy.SwitchState(enemy.enemyChasing);
        }

        //if (enemy.enemyChasing.counter > 0)
        //{
        //    enemy.enemyChasing.counter -= Time.deltaTime;
        //}
        //if (enemy.enemyChasing.counter <= 0)
        //{
        //    if (enemy.isChasing)
        //    {
        //        enemy.SwitchState(enemy.enemyChasing);
        //    }
        //    else
        //    {
        //        enemy.SwitchState(enemy.enemyIdle);
        //    }
        //}

        enemy.chasingCounter = wait.ToString();
    }
    public override void ExitState(EnemyController enemy)
    {

    }
}

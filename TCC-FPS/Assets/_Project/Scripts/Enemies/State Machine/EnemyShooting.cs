using UnityEngine;

public class EnemyShooting : EnemyAbstract
{
    public float angle;
    public float counter;
    public override void EnterState(EnemyController enemy)
    {
        counter = enemy.fireRate * enemy.numberOfShots;

        enemy.agent.destination = enemy.transform.position;

        enemy.ChangeAnimation("Enemy_Shoot");

        enemy.Shot(enemy.numberOfShots);
    }
    public override void LogicsUpdate(EnemyController enemy)
    {
        enemy.aimCounter -= Time.deltaTime;

        enemy.transform.LookAt(PlayerController.instance.transform.position);
        enemy.firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 0.5f, 0f));

        Vector3 targetDir = PlayerController.instance.transform.position - enemy.transform.position;
        angle = Vector3.SignedAngle(targetDir, enemy.transform.forward, Vector3.up);

        if (counter > 0)
        {
            counter -= Time.deltaTime;
        }
        else
        {
            enemy.SwitchState(enemy.chasing);
        }
    }
    public override void ExitState(EnemyController enemy)
    {

    }
}

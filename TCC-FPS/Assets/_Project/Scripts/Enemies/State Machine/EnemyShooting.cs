using UnityEngine;

public class EnemyShooting : EnemyAbstract
{
    public float angle;
    public override void EnterState(EnemyController enemy)
    {

    }
    public override void LogicsUpdate(EnemyController enemy)
    {
        enemy.aimCounter -= Time.deltaTime;

        enemy.firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 0.5f, 0f));

        Vector3 targetDir = PlayerController.instance.transform.position - enemy.transform.position;
        angle = Vector3.SignedAngle(targetDir, enemy.transform.forward, Vector3.up);

        if (enemy.aimCounter <= 0)
        {
            Debug.Log("Shot");
            enemy.Shot(3);

            if (enemy.isChasing)
            {
                enemy.SwitchState(enemy.chasing);
            }
            else
            {
                enemy.SwitchState(enemy.idle);
            }
        }
    }
    public override void ExitState(EnemyController enemy)
    {

    }
}

using UnityEngine;

public class Run : Abstract
{
    float originalSpeed;
    public override void EnterState(PlayerController player)
    {
        player.ChangeAnimation("Player_Run");
        originalSpeed = player.speed;
        player.speed = 14;
    }
    public override void LogicsUpdateState(PlayerController player)
    {
        //Running
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            player.SwitchState(player.walk);
        }
        if (player.moveInput.x == 0 || player.moveInput.z == 0)
        {
            player.SwitchState(player.walk);
        }

        //Shot
        if (Input.GetMouseButtonDown(0))
        {
            player.Shot();
        }

        player.Movement();
    }
    public override void ExitState(PlayerController player)
    {
        player.speed = originalSpeed;
    }
}

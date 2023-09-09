using UnityEngine;

public class Idle : Abstract
{
    public override void EnterState(PlayerController player)
    {
        player.ChangeAnimation("Player_Idle");
    }
    public override void LogicsUpdateState(PlayerController player)
    {
        if (player.velocity.x != 0 || player.velocity.z != 0)
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

    }
}

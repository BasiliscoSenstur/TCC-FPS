using UnityEngine;

public class Jump : Abstract
{
    public override void EnterState(PlayerController player)
    {

    }
    public override void LogicsUpdateState(PlayerController player)
    {
        if (player.controller.isGrounded)
        {
            player.SwitchState(player.idle);
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

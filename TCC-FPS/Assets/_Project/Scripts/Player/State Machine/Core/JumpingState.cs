using UnityEngine;

public class JumpingState : Abstract
{
    public bool run;
    public override void EnterState(PlayerController player)
    {

    }
    public override void LogicsUpdateState(PlayerController player)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
        }
        else
        {
            run = false;
        }

        if (player.canJump)
        {
            if (!run)
            {
                player.SwitchState(player.idle);
            }
            else
            {
                player.SwitchState(player.running);
            }
        }

        player.Movement();
    }
    public override void ExitState(PlayerController player)
    {

    }
}

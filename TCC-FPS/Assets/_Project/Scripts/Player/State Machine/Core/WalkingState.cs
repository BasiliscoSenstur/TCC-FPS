using UnityEngine;

public class WalkingState : Abstract
{
    public override void EnterState(PlayerController player)
    {

    }
    public override void LogicsUpdateState(PlayerController player)
    {
        //Idle
        if(player.moveInput.x == 0)
        {
            if(player.moveInput.z == 0)
            {
                player.SwitchState(player.idle);
            }
        }

        //Running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            player.SwitchState(player.running);
        }

        //Jumping
        if (Input.GetButtonDown("Jump") && player.canJump)
        {
            player.Jump();
        }

        player.Movement();
    }
    public override void ExitState(PlayerController player)
    {

    }
}

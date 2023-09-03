using UnityEngine;

public class IdleState : Abstract
{
    public override void EnterState(PlayerController player)
    {

    }
    public override void LogicsUpdateState(PlayerController player)
    {
        //Walking
        if(player.moveInput.x != 0)
        {
            player.SwitchState(player.walking);
        }
        if(player.moveInput.z != 0)
        {
            player.SwitchState(player.walking);
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

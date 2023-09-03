using UnityEngine;

public class IdleState : Abstract
{
    public override void EnterState(PlayerController player)
    {
        player.ChangeAnimation("Player_Idle");
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

        //Shot
        if(Input.GetButtonDown("Fire1"))
        {
            player.Shot();
        }

        player.Movement();
    }
    public override void ExitState(PlayerController player)
    {

    }
}

using UnityEngine;

public class RunningState : Abstract
{
    float original;
    public override void EnterState(PlayerController player)
    {
        original = player.moveSpeed;
        player.moveSpeed = 12;
    }
    public override void LogicsUpdateState(PlayerController player)
    {
        //Stop Running
        if(Input.GetKeyUp(KeyCode.LeftShift))
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
        player.moveSpeed = original;
    }
}

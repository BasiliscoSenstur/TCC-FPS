using UnityEngine;

public class RunningState : Abstract
{
    float original;
    public override void EnterState(PlayerController player)
    {
        player.ChangeAnimation("Player_Run");
        original = player.moveSpeed;
        player.moveSpeed = 12;
    }
    public override void LogicsUpdateState(PlayerController player)
    {
        //Stop Running
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            player.SwitchState(player.walking);
        }
        if (player.moveInput.x == 0 || player.moveInput.z == 0)
        {
            player.SwitchState(player.walking);
        }

        //Jumping
        if (Input.GetButtonDown("Jump") && player.canJump)
        {
            player.Jump();
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
        player.moveSpeed = original;
    }
}

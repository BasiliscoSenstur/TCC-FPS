using UnityEngine;

public class JumpingState : Abstract
{
    public bool run;
    public override void EnterState(PlayerController player)
    {
        player.ChangeAnimation("Player_Idle");
    }
    public override void LogicsUpdateState(PlayerController player)
    {
        //Verifica se player quer chegar no chão correndo
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

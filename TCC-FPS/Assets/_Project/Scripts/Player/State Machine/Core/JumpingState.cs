using UnityEngine;

public class JumpingState : Abstract
{
    public override void EnterState(PlayerController player)
    {
        //Debug.Log("!");
    }
    public override void LogicsUpdateState(PlayerController player)
    {
        //if (player.controller.isGrounded)
        //{
        //    player.SwitchState(player.idleState);
        //}

        //player.Rotation();
        //player.Movement();
    }
    public override void PhysicsUpdateState(PlayerController player)
    {

    }
    public override void ExitState(PlayerController player)
    {

    }
}

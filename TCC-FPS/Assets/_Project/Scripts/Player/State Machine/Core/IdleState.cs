using UnityEngine;

public class IdleState : Abstract
{
    public override void EnterState(PlayerController player)
    {

    }
    public override void LogicsUpdateState(PlayerController player)
    {  
        if(player.moveInput.x !=0 && player.moveInput.z !=0)
        {
            player.SwitchState(player.runningState);
        }

        player.Rotation();
        player.Movement();
    }
    public override void PhysicsUpdateState(PlayerController player)
    {

    }
    public override void ExitState(PlayerController player)
    {

    }
}

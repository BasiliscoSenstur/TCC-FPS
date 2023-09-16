using UnityEngine;

public class Walk : Abstract
{
    public override void EnterState(PlayerController player)
    {
        player.ChangeAnimation("Player_Walk");
    }
    public override void LogicsUpdateState(PlayerController player)
    {
        //Run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            player.SwitchState(player.run);
        }

        //Shot
        if (player.activeGun.canAutoFire)
        {
            if (Input.GetMouseButton(0))
            {
                if (player.activeGun.fireCounter <= 0)
                {
                    player.Shot();
                    player.activeGun.fireCounter = player.activeGun.fireRate;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                player.Shot();
            }
        }

        player.Movement();
    }
    public override void ExitState(PlayerController player)
    {

    }
}

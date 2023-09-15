using UnityEngine;

public class Walk : Abstract
{
    public override void EnterState(PlayerController player)
    {
        player.ChangeAnimation("Player_Walk");
    }
    public override void LogicsUpdateState(PlayerController player)
    {
        //if (player.velocity.x == 0 && player.velocity.z == 0)
        //{
        //    player.SwitchState(player.idle);
        //}

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

        ////Reload
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    if (player.activeGun.currentAmmo < player.activeGun.maxAmmo)
        //    {
        //        player.ReloadGun();
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}

        player.Movement();
    }
    public override void ExitState(PlayerController player)
    {

    }
}

using UnityEngine;

public class Idle : Abstract
{
    public override void EnterState(PlayerController player)
    {
        player.ChangeAnimation("Player_Idle");
    }
    public override void LogicsUpdateState(PlayerController player)
    {
        if (player.moveInput.x != 0 || player.moveInput.z != 0)
        {
            player.SwitchState(player.walk);
        }

        //player.SwitchState(player.walk);

        //Shot
        //if (Input.GetMouseButtonDown(0))
        //{
        //    player.Shot();
        //}

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

        //Reload
         if(Input.GetKeyDown(KeyCode.R)) 
        {
            if (player.activeGun.currentAmmo < player.activeGun.maxAmmo)
            {
                player.activeGun.reloadCounter = player.activeGun.reloadTime;

                if (player.activeGun.reloadCounter <= 0)
                {
                    player.ReloadGun();
                }
            }
        }
    }
    public override void ExitState(PlayerController player)
    {

    }
}

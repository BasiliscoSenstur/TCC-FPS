using UnityEngine;

public class Run : Abstract
{
    float originalSpeed;
    public override void EnterState(PlayerController player)
    {
        player.ChangeAnimation("Player_Run");
        originalSpeed = player.speed;
        player.speed = 14;
    }
    public override void LogicsUpdateState(PlayerController player)
    {
        //Running
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            player.SwitchState(player.walk);
        }
        if (player.moveInput.x == 0 || player.moveInput.z == 0)
        {
            player.SwitchState(player.walk);
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

        //Reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (player.activeGun.currentAmmo < player.activeGun.maxAmmo)
            {
                player.ReloadGun();
            }
            else
            {
                return;
            }
        }

        player.Movement();
    }
    public override void ExitState(PlayerController player)
    {
        player.speed = originalSpeed;
    }
}

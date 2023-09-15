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

        //Shot
        if (player.activeGun.reloadCounter <= 0)
        {
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
        }

        //Jump
        if (player.controller.isGrounded)
        {
            //player.ySpeed = -0.2f;
            if (Input.GetButtonDown("Jump"))
            {
                player.ySpeed = player.jumpForce;
                Debug.Log("Teste");
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
    }
    public override void ExitState(PlayerController player)
    {

    }
}

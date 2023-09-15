using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public bool health;
    public bool bullet;

    public void OnTriggerEnter(Collider other)
    {
        if (health)
        {
            if (PlayerHealthController.instance.currenthealth < PlayerHealthController.instance.maxHealth)
            {
                PlayerHealthController.instance.HealPlayer(12);
                Destroy(gameObject);
            }
        }
        if (bullet)
        {
            if (PlayerController.instance.activeGun.currentAmmo < PlayerController.instance.activeGun.maxAmmo)
            {
                PlayerController.instance.activeGun.PickAmmo(PlayerController.instance.activeGun.pickUpAmount);
                Destroy(gameObject);
            }
        }
    }
}

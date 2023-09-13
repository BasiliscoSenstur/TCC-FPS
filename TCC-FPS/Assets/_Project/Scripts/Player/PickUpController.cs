using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public bool health, bullet;

    public void OnTriggerEnter(Collider other)
    {
        if (health)
        {
            PlayerHealthController.instance.HealPlayer(12);
            Destroy(gameObject);
        }
        if (bullet)
        {
            //Bullets
        }
    }
}

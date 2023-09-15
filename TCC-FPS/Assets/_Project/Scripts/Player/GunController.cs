using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Ammo")]
    public int maxAmmo;
    public int startAmmo, currentAmmo, pickUpAmount;

    [Header("Fire")]
    public GameObject bullet;
    public bool canAutoFire;
    [HideInInspector] public float fireCounter;
    public float fireRate;
    [HideInInspector] public float reloadCounter;
    //public float reloadTime;

    void Awake()
    {
        currentAmmo = startAmmo;
    }

    void Start()
    {

    }

    void Update()
    {
        if (fireCounter > 0) 
        {
            fireCounter -= Time.deltaTime;
        }

        if (reloadCounter > 0)
        {
            reloadCounter -= Time.deltaTime;
        }

        //if(currentAmmo == 0)
        //{
        //    PlayerController.instance.ReloadGun();
        //}
    }

    public void PickAmmo(int amount)
    {
        if (currentAmmo < maxAmmo)
        {
            currentAmmo += amount;
        }
        if(currentAmmo >= maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
        UIController.instance.UpdateAmmoDisplay();
    }
}

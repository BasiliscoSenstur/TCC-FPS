using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Ammo")]
    public int maxAmmo;
    public int startAmmo, pickUpAmount;
    [HideInInspector] public int currentAmmo;

    [Header("Fire")]
    public bool canAutoFire;
    public GameObject bullet;
    public int bulletDamage;
    public Transform firePoint;
    public float fireRate;
    [HideInInspector] public float fireCounter;
    [HideInInspector] public float reloadCounter;
    public float aimSpeed;
    public int aimFov;

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

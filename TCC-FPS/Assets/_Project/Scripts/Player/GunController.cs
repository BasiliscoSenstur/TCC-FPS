using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bullet;
    public bool canAutoFire;
    public int maxAmmo;
    public int currentAmmo;

    [HideInInspector] public float fireCounter;
    public float fireRate;

    [HideInInspector] public float reloadCounter;
    public float reloadTime;


    void Start()
    {
        currentAmmo = maxAmmo;
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
}

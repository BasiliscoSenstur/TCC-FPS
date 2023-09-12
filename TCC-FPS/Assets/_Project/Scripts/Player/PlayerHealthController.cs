using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    private void Awake()
    {
        instance = this;
    }
    //-----------------------------------//

    public int currenthealth, maxHealth;
    void Start()
    {
        currenthealth = maxHealth;
        UIController.instance.UpdateHealthDisplay();
    }

    void Update()
    {
        
    }

    public void DemagePlayer(int amount)
    {
        //amount /= 2;
        currenthealth -= amount;
        if (currenthealth <= 0)
        {
            gameObject.SetActive(false);
            GameManager.instance.PlayerDied();
        }
        UIController.instance.UpdateHealthDisplay();
    }
}

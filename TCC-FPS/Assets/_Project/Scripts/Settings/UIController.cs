using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    private void Awake()
    {
        instance = this;
    }
    //-----------------------------------//

    public Slider healthBar;
    public TMP_Text healthText;
    public TMP_Text ammoText;


    public CanvasGroup fadeScreen;
    public bool fadeIn;
    void Start()
    {
        FadeScreen();
        UpdateAmmoDisplay();
    }

    void Update()
    {
        if (fadeIn)
        {
            fadeScreen.alpha = Mathf.MoveTowards(fadeScreen.alpha, 1f, 0.2f);
        }
        else
        {
            fadeScreen.alpha = Mathf.MoveTowards(fadeScreen.alpha, 0f, 0.05f);
        }
    }

    public void UpdateHealthDisplay()
    {
        healthBar.value = PlayerHealthController.instance.currenthealth;
        //healthText.text = "HEALTH: " + PlayerHealthController.instance.currenthealth + "/" + PlayerHealthController.instance.maxHealth;
    }

    public void FadeScreen()
    {
        if (fadeIn)
        {
            fadeIn = false;
        }
        else
        {
            fadeIn = true;
        }
    }

    public void UpdateAmmoDisplay()
    {
        ammoText.text = "Ammo:" + PlayerController.instance.activeGun.currentAmmo.ToString();
    }
}

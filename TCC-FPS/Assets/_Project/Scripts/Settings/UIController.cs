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

    public CanvasGroup fadeScreen;
    public bool fadeIn;
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void UpdateHealthDisplay()
    {
        healthBar.value = PlayerHealthController.instance.currenthealth;
        //healthText.text = "HEALTH: " + PlayerHealthController.instance.currenthealth + "/" + PlayerHealthController.instance.maxHealth;
        Debug.Log("Teste");
    }

    public void FadeScreen()
    {

    }
}

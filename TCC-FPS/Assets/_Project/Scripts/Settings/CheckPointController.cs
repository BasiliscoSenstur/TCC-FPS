using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointController : MonoBehaviour
{
    public string cpName;
    void Start()
    {
        if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_Cp"))
        {
            if (PlayerPrefs.GetString(SceneManager.GetActiveScene().name + "_Cp") == cpName)
            {
                PlayerController.instance.transform.position = transform.position;
            }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)) 
        {
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_Cp", "");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_Cp", cpName);
        }
    }
}

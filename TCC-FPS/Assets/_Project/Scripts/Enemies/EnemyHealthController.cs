using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int health;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DamageEnemy(int amoumt)
    {
        health -= amoumt;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

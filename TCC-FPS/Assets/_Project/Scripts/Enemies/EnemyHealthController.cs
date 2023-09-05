using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int enemyHealth = 5;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void DamageEnemy(int amout)
    {
        enemyHealth -= amout;
        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}

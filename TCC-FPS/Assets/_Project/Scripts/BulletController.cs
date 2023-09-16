using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;
    Rigidbody rb;
    public GameObject impactEffect;
    public bool playerBullet, enemyBullet;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.velocity = transform.forward * bulletSpeed;
        Destroy(gameObject, 10f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && playerBullet)
        {
            if (transform.position.y > 1.3)
            {
                other.gameObject.GetComponent<EnemyHealthController>().DemageEnemy(PlayerController.instance.activeGun.bulletDamage * 27);
                Debug.Log("Headshot");
            }
            else
            {
                other.gameObject.GetComponent<EnemyHealthController>().DemageEnemy(PlayerController.instance.activeGun.bulletDamage);
            }
        }

        if (other.CompareTag("Player") && enemyBullet)
        {
            PlayerHealthController.instance.DemagePlayer(1);
            Debug.Log("Hit Player at " + transform.position);
        }

        Destroy(gameObject);
        Instantiate(impactEffect, transform.position + (transform.forward * -bulletSpeed * Time.deltaTime), transform.rotation);
    }
}

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
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(1);
            //Destroy(other.gameObject);
        }

        if (other.CompareTag("Player") && enemyBullet)
        {
            //Player Health
            Debug.Log("Hit Player at " + transform.position);
        }

        Destroy(gameObject);
        Instantiate(impactEffect, transform.position + (transform.forward * -bulletSpeed * Time.deltaTime), transform.rotation);
    }
}

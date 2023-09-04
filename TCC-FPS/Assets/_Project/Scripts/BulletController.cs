using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;
    Rigidbody rb;
    public GameObject impactEffect;

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
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
        Instantiate(impactEffect, transform.position + (transform.forward * -bulletSpeed * Time.deltaTime), transform.rotation);
    }
}

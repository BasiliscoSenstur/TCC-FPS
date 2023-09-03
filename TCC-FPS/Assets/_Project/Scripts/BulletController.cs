using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;
    Rigidbody rb;
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
        Destroy(gameObject);
    }
}

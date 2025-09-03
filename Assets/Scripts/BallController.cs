using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float hitForce = 10f;
    public float downwardForce = -3f; // fuerza vertical para que la pelota caiga
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.useGravity = true; // activamos gravedad para caída natural
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 direction = (transform.position - collision.transform.position).normalized;

            // Agregamos componente vertical descendente
            Vector3 force = new Vector3(direction.x, downwardForce, direction.z) * hitForce;

            rb.velocity = Vector3.zero;
            rb.AddForce(force, ForceMode.Impulse);
        }
    }
}




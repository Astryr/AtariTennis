using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServeManager : MonoBehaviour
{
    public Rigidbody ballRb;
    public Transform servePositionPlayer1;
    public Transform servePositionPlayer2;
    public bool isPlayerOneServing = true;

    public Slider powerBar;
    public float maxPower = 15f;
    public float powerSpeed = 30f;
    private float currentPower = 0f;
    private bool charging = false;

    void Start()
    {
        PositionBallForServe();
        powerBar.gameObject.SetActive(true);
    }

    void Update()
    {
        if (charging)
        {
            currentPower += powerSpeed * Time.deltaTime;
            if (currentPower > maxPower)
                currentPower = 0f;

            powerBar.value = currentPower / maxPower;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            charging = true;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            charging = false;
            ServeBall();
        }
    }

    void PositionBallForServe()
    {
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;
        ballRb.useGravity = false;

        if (isPlayerOneServing)
            ballRb.transform.position = servePositionPlayer1.position;
        else
            ballRb.transform.position = servePositionPlayer2.position;
    }

    void ServeBall()
    {
        ballRb.useGravity = true;

        Vector3 direction = isPlayerOneServing ? Vector3.forward : Vector3.back;
        Vector3 force = new Vector3(0f, -2f, direction.z) * currentPower;

        ballRb.AddForce(force, ForceMode.Impulse);
        currentPower = 0f;
        powerBar.value = 0f;
        powerBar.gameObject.SetActive(false);
        this.enabled = false; // desactiva el script hasta el próximo saque
    }
}

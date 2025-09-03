using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool isPlayerOne = true;

    // Límites en X (ancho de la cancha)
    private Vector2 movementLimitsX = new Vector2(-5f, 5f);

    // Límites en Z (profundidad de la cancha)
    private Vector2 playerOneLimitsZ = new Vector2(-10.5f, 0f); // mitad inferior
    private Vector2 playerTwoLimitsZ = new Vector2(0f, 10.5f);  // mitad superior

    void Update()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (isPlayerOne)
        {
            // Movimiento con WASD
            moveX = Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0;
            moveZ = Input.GetKey(KeyCode.S) ? -1 : Input.GetKey(KeyCode.W) ? 1 : 0;
        }
        else
        {
            // Movimiento con flechas
            moveX = Input.GetKey(KeyCode.LeftArrow) ? -1 : Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
            moveZ = Input.GetKey(KeyCode.DownArrow) ? -1 : Input.GetKey(KeyCode.UpArrow) ? 1 : 0;
        }

        Vector3 move = new Vector3(moveX, 0f, moveZ).normalized * moveSpeed * Time.deltaTime;
        transform.position += move;

        // Aplicar límites según el jugador
        Vector2 currentLimitsZ = isPlayerOne ? playerOneLimitsZ : playerTwoLimitsZ;

        float clampedX = Mathf.Clamp(transform.position.x, movementLimitsX.x, movementLimitsX.y);
        float clampedZ = Mathf.Clamp(transform.position.z, currentLimitsZ.x, currentLimitsZ.y);

        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }
}

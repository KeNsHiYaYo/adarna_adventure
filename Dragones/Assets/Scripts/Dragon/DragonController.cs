using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 70f;
    public float ascendSpeed = 5f;

    void Update()
    {
        // Movimiento hacia adelante
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        // Rotaci�n horizontal
        float horizontal = Input.GetAxis("Horizontal");
        transform.Rotate(0f, horizontal * rotationSpeed * Time.deltaTime, 0f);

        // Subir / bajar
        float vertical = Input.GetAxis("Vertical");
        transform.position += transform.up * vertical * ascendSpeed * Time.deltaTime;

        // Inclinaci�n visual (opcional)
        // Puedes inclinar el drag�n al girar si quieres m�s realismo
    }
}


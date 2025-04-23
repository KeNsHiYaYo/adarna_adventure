using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    public float moveSpeed = 10f; //Velocidad Normal
    public float boostSpeed = 20f; //Velocidad rapida
    private float currentSpeed; //Variable donde guardara la velocidad


    void Update()
    {
        if (Input.GetMouseButton(0)) // Clic izquierdo
        {
            currentSpeed = boostSpeed;
            transform.position += transform.forward * currentSpeed * Time.deltaTime;
        }
        else
        {
            currentSpeed = moveSpeed;
            transform.position += transform.forward * currentSpeed * Time.deltaTime;
        }
    }
}


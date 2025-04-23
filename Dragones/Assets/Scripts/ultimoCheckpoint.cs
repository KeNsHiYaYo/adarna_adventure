using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ultimoCheckpoint : MonoBehaviour
{
    public Vector3 lastCheckpoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Muerte"))
        {
            muere();
        }
    }

    public void muere()
    {
        transform.position = lastCheckpoint;
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ultimoCheckpoint>().lastCheckpoint = GetComponent<Transform>().position;
            Destroy(gameObject);
        }
    }
}

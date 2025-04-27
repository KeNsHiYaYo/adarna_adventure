using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RingTrigger : MonoBehaviour
{
    public RingTrigger nextRing; // El siguiente anillo en la secuencia
    public GameObject ringVisual; // El modelo visual del anillo (puede ocultarse al pasar)

    public bool isActive = false;

    void Start()
    {
        if (ringVisual == null)
            ringVisual = this.gameObject;

        // Solo se activa el primer anillo manualmente desde el editor
        if (!isActive)
            ringVisual.SetActive(false);
    }

    public void ActivateRing()
    {
        isActive = true;
        ringVisual.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive && other.CompareTag("Dragon")) // o "Dragon" según tu configuración
        {
            Debug.Log("Pasaste por un anillo");

            // Desactivar este anillo
            ringVisual.SetActive(false);
            isActive = false;

            // Activar el siguiente
            if (nextRing != null)
            {
                nextRing.ActivateRing();
            }
            else
            {
                Debug.Log("¡Todos los anillos completados!");
                LoadNextScene();
                // Aquí puedes lanzar evento de victoria, animación, etc.
            }
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene("final");
    }
}

using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Vector2 sensibility = new Vector2(2f, 2f);
    public Transform cameraTransform; // arrastrar Main Camera aquí en el Inspector

    private float pitch = 0f; // Rotación vertical acumulada

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = GameObject.Find("Main Camera").transform;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        // Rotar horizontalmente el dragón
        transform.Rotate(Vector3.up * hor * sensibility.x);

        // Rotar verticalmente el dragón (pitch)
        pitch -= ver * sensibility.y;
        pitch = Mathf.Clamp(pitch, -60f, 60f); // Limita cuánto puede mirar arriba/abajo

        transform.localRotation = Quaternion.Euler(pitch, transform.localEulerAngles.y, 0f);
    }
}

using UnityEngine;

public class FPSCameraDragon : MonoBehaviour
{
    public Vector2 sensibility = new Vector2(2f, 2f);
    public Transform cameraTransform; // arrastrar Main Camera aquí en el Inspector

    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;

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

        // Rotar horizontalmente el objeto Player (eje Y)
        //transform.Rotate(Vector3.up * hor * sensibility.x);
        horizontalRotation += hor * sensibility.x;
        horizontalRotation = Mathf.Clamp(horizontalRotation, -80f, 80f);

        // Rotar verticalmente la cámara (eje X)
        verticalRotation -= ver * sensibility.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f);

        cameraTransform.localEulerAngles = new Vector3(verticalRotation, horizontalRotation, 0f);
    }
}

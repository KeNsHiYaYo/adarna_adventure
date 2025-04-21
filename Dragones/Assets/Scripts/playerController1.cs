using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController1 : MonoBehaviour
{
    //Variables para el movimiento del player
    private new Rigidbody rigidbody; 
    public float movementSpeed;
    public float normalSpeed;
    public float slowSpeed = 2f;

    //Variables para el balanceo en el Parapeto
    public float balance = 0f;
    public float balanceThreshold = 30f;
    public float balanceSpeed = 20f;
    public float balanceDrift = 5f;
    public Transform playerBody;
    private bool isOnBeam = false; // Activar/desactivar la mecanica

    // Variables para el viento
    public float windMinForce = 5f;
    public float windMaxForce = 15f;
    public float gustDuration = 2f;
    public float timeBetweenGusts = 3f;

    private float windTimer = 0f;
    private float gustTimer = 0f;
    private float currentWindForce = 0f;
    private float currentWindDirection = 0f;
    private bool windIsBlowing = false;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        movementSpeed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        Vector3 velocity = Vector3.zero;
        if (hor !=0 || ver != 0)
        {
            Vector3 direction = (transform.forward * ver + transform.right * hor).normalized;
            velocity = direction * movementSpeed;
        }
        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;

        if (isOnBeam)
        {
            // Inestabilidad natural
            balance += Random.Range(-balanceDrift, balanceDrift) * Time.deltaTime;

            // Ráfagas de viento con efecto visual y físico
            if (!windIsBlowing)
            {
                windTimer += Time.deltaTime;
                if (windTimer >= timeBetweenGusts)
                {
                    windIsBlowing = true;
                    gustTimer = 0f;

                    // Dirección aleatoria: -1 (izquierda) o 1 (derecha)
                    currentWindDirection = Random.Range(0, 2) == 0 ? -1f : 1f;

                    // Fuerza aleatoria
                    currentWindForce = Random.Range(windMinForce, windMaxForce);
                }
            }
            else
            {
                gustTimer += Time.deltaTime;

                //Efecto físico: empuja al jugador lateralmente
                Vector3 windPush = transform.right * currentWindDirection * currentWindForce * 0.5f;
                rigidbody.AddForce(windPush, ForceMode.Force);

                if (gustTimer >= gustDuration)
                {
                    windIsBlowing = false;
                    windTimer = 0f;
                }
            }


            // Corrección con input
            float horizontalInput = Input.GetAxis("Horizontal");
            balance -= horizontalInput * balanceSpeed * Time.deltaTime;

            // Rotación visual (opcional)
            playerBody.localRotation = Quaternion.Euler(0f, 0f, balance);

            // Si se cae...
            if (Mathf.Abs(balance) > balanceThreshold)
            {
                Debug.Log("¡Perdiste el equilibrio!");
                // Aquí podrías reiniciar la escena, reproducir animación, etc.
            }
        }
        else
        {
            // Resetear balance al volver al suelo
            balance = Mathf.Lerp(balance, 0f, Time.deltaTime * 5f);
            playerBody.localRotation = Quaternion.Euler(0f, 0f, balance);
        }

    }

     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BalanceBeam"))
        {
            isOnBeam = true;
            movementSpeed = slowSpeed;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BalanceBeam"))
        {
            isOnBeam = false;
            movementSpeed = normalSpeed;
        }
    }
}

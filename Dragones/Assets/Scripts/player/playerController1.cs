using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController1 : MonoBehaviour
{
    private new Rigidbody rigidbody;
    public float movementSpeed;
    public float normalSpeed;
    public float slowSpeed = 2f;

    public Animator animator;

    public float balance = 0f;
    public float balanceThreshold = 30f;
    public float balanceSpeed = 20f;
    public float balanceDrift = 5f;
    public Transform playerBody;
    private bool isOnBeam = false;

    public float windMinForce = 5f;
    public float windMaxForce = 15f;
    public float gustDuration = 2f;
    public float timeBetweenGusts = 3f;

    private float windTimer = 0f;
    private float gustTimer = 0f;
    private float currentWindForce = 0f;
    private float currentWindDirection = 0f;
    private bool windIsBlowing = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        movementSpeed = normalSpeed;
    }

    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        bool isMoving = hor != 0 || ver != 0;

        if (isMoving)
        {
            Vector3 direction = (transform.forward * ver + transform.right * hor).normalized;
            Vector3 moveVelocity = direction * movementSpeed;
            moveVelocity.y = rigidbody.velocity.y;
            rigidbody.velocity = moveVelocity;
        }
        else
        {
            Vector3 stopVelocity = new Vector3(0, rigidbody.velocity.y, 0);
            rigidbody.velocity = stopVelocity;
        }

        if (animator != null)
        {
            animator.SetBool("isMoving", isMoving);
            animator.SetBool("isBalancing", isOnBeam);
        }

        if (isOnBeam)
        {
            balance += Random.Range(-balanceDrift, balanceDrift) * Time.deltaTime;

            if (!windIsBlowing)
            {
                windTimer += Time.deltaTime;
                if (windTimer >= timeBetweenGusts)
                {
                    windIsBlowing = true;
                    gustTimer = 0f;
                    currentWindDirection = Random.Range(0, 2) == 0 ? -1f : 1f;
                    currentWindForce = Random.Range(windMinForce, windMaxForce);
                }
            }
            else
            {
                gustTimer += Time.deltaTime;
                Vector3 windPush = transform.right * currentWindDirection * currentWindForce * 0.5f;
                rigidbody.AddForce(windPush, ForceMode.Force);

                if (gustTimer >= gustDuration)
                {
                    windIsBlowing = false;
                    windTimer = 0f;
                }
            }

            float horizontalInput = Input.GetAxis("Horizontal");
            balance -= horizontalInput * balanceSpeed * Time.deltaTime;
            playerBody.localRotation = Quaternion.Euler(0f, 0f, balance);

            if (Mathf.Abs(balance) > balanceThreshold)
            {
                Debug.Log("¡Perdiste el equilibrio!");
                // Podrías reiniciar la escena aquí
            }
        }
        else
        {
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


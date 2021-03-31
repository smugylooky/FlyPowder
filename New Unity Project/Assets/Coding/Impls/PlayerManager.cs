using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    public float velocidad;
    public float velocidadMaxima;
    public float alturaSalto;
    private float velocidadActual = 0.0f;
    bool jumping = false;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerControls.isMovingRight())
        {
            velocidadActual = velocidad;
        }
        else
        {
            if (PlayerControls.isMovingLeft())
            {
                velocidadActual = -velocidad;
            }
            else
            {
                velocidadActual = 0;
            }
        }
        if (PlayerControls.isJumping())
        {
            jumping = true;
        }
    }

    private void FixedUpdate()
    {
        Debug.Log(velocidadActual * Time.fixedDeltaTime * 50);
        if (Mathf.Abs(velocidadActual * Time.fixedDeltaTime * 50) > velocidadMaxima)
        {
            velocidadActual = velocidadActual / Mathf.Abs(velocidadActual) * velocidadMaxima;
        }

        playerRigidBody.AddForce(Vector2.right * velocidadActual * Time.fixedDeltaTime * 50);

        if (jumping)
        {
            playerRigidBody.AddForce(Vector2.up * alturaSalto * Time.fixedDeltaTime * 50, ForceMode2D.Impulse);
            jumping = false;
        }

    }
}

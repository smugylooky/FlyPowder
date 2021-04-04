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
    bool onair = false;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        velocidadActual = 0;
        if (!onair)
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
            }
            if (PlayerControls.isJumping() && !onair)
            {
                jumping = true;
                onair = true;
            }
        }
    }

    private void FixedUpdate()
    {
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "terreno")
        {
            onair = false;
        }
    }
}

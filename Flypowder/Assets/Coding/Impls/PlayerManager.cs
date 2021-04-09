using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private float lastRBSpeed;
    public float velocidad;
    public float velocidadMaxima;
    public float alturaSalto;
    private float velocidadActual;
    bool jumping = false;
    bool onair = false;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        velocidadActual = 0.0f;
        lastRBSpeed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        velocidadActual = lastRBSpeed;
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

        lastRBSpeed = playerRigidBody.velocity.x;

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "terreno")
        {
            onair = false;
        }
        if (collision.gameObject.tag == "plataforma")
        {
            foreach (ContactPoint2D hitPos in collision.contacts)
            {
                if (hitPos.normal.y > 0 && onair)
                {
                    onair = false;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "terreno" || collision.gameObject.tag == "plataforma") && !onair)
        {
            onair = true;
        }
    }

}

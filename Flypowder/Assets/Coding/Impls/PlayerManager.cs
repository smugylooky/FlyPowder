using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    private float lastRBSpeed;

    public float friccionDefault;
    public float deslizOnCrouch;
    public float velocidad;
    public float velocidadMaximaG;
    public float velocidadMaximaA;
    public float alturaSalto;

    public float saltoAlturaY;
    public float saltoLargoY;

    private float velocidadActual;
    bool jumping;
    bool onair;
    bool crouching;
    private SFXManager sfxManager;
    // Start is called before the first frame update
    void Start()
    {
        jumping = false;
        onair = false;
        crouching = false;

        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        sfxManager = FindObjectOfType<SFXManager>();

        velocidadActual = 0.0f;
        lastRBSpeed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(onair);
        velocidadActual = lastRBSpeed;
        if (!onair)
        {

            if ((PlayerControls.isMovingLeft() || PlayerControls.isMovingRight()) && !crouching)
            {
                UpdateSpeedFromInputs();
            }
            else 
            {
                playerAnimator.SetBool("Is Running", false);
            }


            if (PlayerControls.isJumping())
            {
                playerAnimator.SetTrigger("Jumping");
                playerAnimator.SetBool("On Air", true);
                jumping = true;
                onair = true;
            }

            if (PlayerControls.isCrouching())
            {
                playerAnimator.SetTrigger("Crouching");
                playerAnimator.SetBool("Is Crouching", true);
                crouching = true;
            }
            else
            {
                playerAnimator.SetBool("Is Crouching", false);
                crouching = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (jumping)
        {
            sfxManager.PlayJump();
            float specialJumpY = 1f;
            onair = true;

            if (crouching)
            {
                if (Mathf.Abs(playerRigidBody.velocity.x) > 3) //ES UN SALTO LARGO
                {
                    specialJumpY = saltoLargoY;
                    playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x > 0 ? velocidadMaximaA : -velocidadMaximaA, playerRigidBody.velocity.y);
                }
                else //SALTO ALTURA
                {
                    specialJumpY = saltoAlturaY;
                }
            }

            playerRigidBody.AddForce(Vector2.up * alturaSalto * Time.fixedDeltaTime * 50 * specialJumpY, ForceMode2D.Impulse);
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x * 0.65f, playerRigidBody.velocity.y);
            jumping = false;
        }

        CheckSpeed();

        playerRigidBody.AddForce(Vector2.right * velocidadActual * Time.fixedDeltaTime * 50);

        lastRBSpeed = playerRigidBody.velocity.x;

    }

    private void CheckSpeed()
    {
        if (!onair && Mathf.Abs(playerRigidBody.velocity.x) > velocidadMaximaG)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x > 0 ? velocidadMaximaG : -velocidadMaximaG, playerRigidBody.velocity.y);
        }

        if (onair && Mathf.Abs(playerRigidBody.velocity.x) > velocidadMaximaA)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x > 0 ? velocidadMaximaA : -velocidadMaximaA, playerRigidBody.velocity.y);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "terreno")
        {
            onair = false;
            playerAnimator.SetBool("On Air",false);
        }
        if (collision.gameObject.tag == "plataforma")
        {
            foreach (ContactPoint2D hitPos in collision.contacts)
            {
                if (hitPos.normal.y > 0 && onair)
                {
                    onair = false;
                    playerAnimator.SetBool("On Air", false);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stage hazard")
        {
                  
            this.transform.position = new Vector2(-65.51f, 6.87f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "terreno" || collision.gameObject.tag == "plataforma") && !onair)
        {
            onair = true;
            crouching = false;
        }
    }

    public void ShallBeFlippedOnShot(Vector2 normalizedCoords)
    {
        if (-normalizedCoords.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void UpdateSpeedFromInputs()
    {
        if (PlayerControls.isMovingRight())
        {
            if (playerRigidBody.velocity.x > 3f)
            {
                playerAnimator.SetTrigger("Running");
                playerAnimator.SetBool("Is Running", true);
                GetComponent<SpriteRenderer>().flipX = true;
            }
            velocidadActual = velocidad;
        }
        else
        {
            if (PlayerControls.isMovingLeft())
            {
                if (playerRigidBody.velocity.x < 3f)
                {
                    playerAnimator.SetTrigger("Running");
                    playerAnimator.SetBool("Is Running", true);
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                velocidadActual = -velocidad;
            }
        }
    }

}

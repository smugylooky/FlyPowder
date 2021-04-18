using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    private float lastRBSpeed;
    public float velocidad;
    public float velocidadMaximaG;
    public float velocidadMaximaA;
    public float alturaSalto;
    private float velocidadActual;
    bool jumping = false;
    bool onair = false;
    private SFXManager sfxManager;
    private float defaultFriction;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        sfxManager = FindObjectOfType<SFXManager>();
        velocidadActual = 0.0f;
        lastRBSpeed = 0.0f;
        defaultFriction = playerRigidBody.sharedMaterial.friction;
    }

    // Update is called once per frame
    void Update()
    {
        velocidadActual = lastRBSpeed;
        if (!onair)
        {
            if (velocidadActual.Equals(0)) 
            {
                playerAnimator.SetBool("Is Running", false);
            } 
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


            if (PlayerControls.isJumping() && !onair)
            {
                playerAnimator.SetTrigger("Jumping");
                playerAnimator.SetBool("On Air", true);
                jumping = true;
                onair = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!onair && Mathf.Abs(playerRigidBody.velocity.x) > velocidadMaximaG)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x > 0 ? velocidadMaximaG : -velocidadMaximaG, playerRigidBody.velocity.y);
        }

        if (onair && Mathf.Abs(playerRigidBody.velocity.x) > velocidadMaximaA)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x > 0 ? velocidadMaximaA : -velocidadMaximaA, playerRigidBody.velocity.y);
        }

        playerRigidBody.AddForce(Vector2.right * velocidadActual * Time.fixedDeltaTime * 50);

        if (jumping)
        {
            sfxManager.PlayJump();
            playerRigidBody.AddForce(Vector2.up * alturaSalto * Time.fixedDeltaTime * 50, ForceMode2D.Impulse);
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x * 0.75f, playerRigidBody.velocity.y);
            jumping = false;
        }

   

        lastRBSpeed = playerRigidBody.velocity.x;

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

}

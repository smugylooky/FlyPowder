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
    public float airPenaltyTimer;
    public float velocidad;
    public float velocidadMaximaG;
    public float velocidadMaximaA;
    public float alturaSalto;

    public float saltoAlturaY;
    public float saltoLargoY;
    
    [SerializeField]
    private FlyPowderSceneManager sceneManager;

    private float velocidadActual;
    private bool jumping;
    private bool onair;
    private bool crouching;
    private bool timeOutAir;
    private bool timingJumpPenalty;
    private SFXManager sfxManager;
    // Start is called before the first frame update
    void Start()
    {
        jumping = false;
        onair = false;
        crouching = false;
        timeOutAir = false;
        timingJumpPenalty = false;

        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        sfxManager = SFXManager.Instance;

        velocidadActual = 0.0f;
        lastRBSpeed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        velocidadActual = lastRBSpeed;

        if (PlayerControls.isJumping() && !timeOutAir)
        {
            jumping = true;
            onair = true;
            timeOutAir = true;
            timingJumpPenalty = false;
        }

        if (!onair)
        {
            StopAllCoroutines();
            timeOutAir = false;
            timingJumpPenalty = false;

            if ((PlayerControls.isMovingLeft() || PlayerControls.isMovingRight()) && !crouching)
            {
                UpdateSpeedFromInputs();
            }
            else 
            {
                playerAnimator.SetBool("Is Running", false);
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
            playerAnimator.SetTrigger("Jumping");
            playerAnimator.SetBool("On Air", true);
            sfxManager.PlayJump();

            float specialJumpY = 1f;

            //onair = true;

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

            StopAllCoroutines();
            jumping = false;
            Debug.Log(timeOutAir);
        }

        if (onair && !timingJumpPenalty && !timeOutAir)
        {
            timingJumpPenalty = true;
            StartCoroutine(JumpPenaltyApplication());
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "terreno")
        {
            StopAllCoroutines();
            onair = false;
            timeOutAir = false;
            timingJumpPenalty = false;
            playerAnimator.SetBool("On Air", false);
        }
        if (collision.gameObject.tag == "plataforma")
        {
            foreach (ContactPoint2D hitPos in collision.contacts)
            {
                if (hitPos.normal.y > 0 && onair)
                {
                    StopAllCoroutines();
                    onair = false;
                    timeOutAir = false;
                    timingJumpPenalty = false;
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
        if (collision.gameObject.tag == "NextLevel") 
        {
            sfxManager.playSonidoCambioMapa();
            if (Application.loadedLevel == 3)
            { sceneManager.LoadSecondLevel(); }
            else if (Application.loadedLevel == 4) { sceneManager.LoadThirdLevel(); }
        }
        if (collision.gameObject.tag == "terreno" || collision.gameObject.tag == "plataforma")
        {
            StopAllCoroutines();
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

    private IEnumerator JumpPenaltyApplication()
    {
        Debug.Log("Empezando corutina");
        yield return new WaitForSeconds(airPenaltyTimer/10);
        timeOutAir = true;
        timingJumpPenalty = false;
    }

}

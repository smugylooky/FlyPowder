using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponBase armaEquipada;

    public GameObject bala;

    private int municionActual;
    private Rigidbody2D playerRigidBody;
    private Vector2 coordsRaton;
    private Vector2 playercoords;
    private Vector2 normalizedCoords;
    private bool disparando;
    private bool hasArmaEquipada;
    private bool recargando;
    private bool onRecargaCooldown;
    private bool onShotCooldown;
    //bool que compruebe que se ha disparado fuera del intervalo entre disparos

    void Start()
    {
        disparando = false;
        recargando = false;
        onShotCooldown = false;
        onRecargaCooldown = false;
        //arma = PlayerInventory.getEquippedWeapon();
        if (armaEquipada != null)
        {
            hasArmaEquipada = true;
            municionActual = armaEquipada.balasCargadorMax;
        }
        else
        {
            hasArmaEquipada = false;
        }

        playerRigidBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!hasArmaEquipada) { hasArmaEquipada = armaEquipada != null; }
        if (!recargando && hasArmaEquipada)
        {
            if (PlayerControls.isShooting(armaEquipada.tipoDisparo) && municionActual > 0)
            {
                playercoords = transform.position;
                coordsRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                normalizedCoords = (playercoords - coordsRaton).normalized;
                disparando = true;
            }

            if (PlayerControls.isReloading() && municionActual < armaEquipada.balasCargadorMax)
            {
                recargando = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (disparando)
        {
            if (!onShotCooldown)
            {
                BulletSetup();
                playerRigidBody.AddForce(normalizedCoords * armaEquipada.retroceso * Time.fixedDeltaTime * 50, ForceMode2D.Impulse);
                municionActual--;
                disparando = false;
                onShotCooldown = true;
                StartCoroutine(EsperarEntreDisparos());
            }
        }

        if (recargando && !onRecargaCooldown) 
        {
            onRecargaCooldown = true;
            StartCoroutine(Recargar());
        }
    }

    private void BulletSetup()
    {
        GameObject bullet = Instantiate(bala, new Vector2(transform.position.x, transform.position.y), new Quaternion());
        //bullet.transform.rotation = Quaternion.LookRotation(-normalizedCoords);
        Physics2D.IgnoreCollision(bullet.GetComponent<BoxCollider2D>(), this.GetComponent<BoxCollider2D>(), true);
        //bullet.GetComponent<Rigidbody2D>().velocity = (-normalizedCoords * 2);
        //bullet.SetActive(false); bullet.SetActive(true);
        //Debug.Log(normalizedCoords + "////" + -normalizedCoords);
    }

    private IEnumerator Recargar()
    {
        yield return new WaitForSeconds(armaEquipada.tiempoRecarga);
        municionActual = armaEquipada.balasCargadorMax;
        recargando = false;
        onRecargaCooldown = false;
    }

    private IEnumerator EsperarEntreDisparos()
    {
        yield return new WaitForSeconds(armaEquipada.tiempoEntreDisparos);
        onShotCooldown = false;
    }

    private void setArmaEquipada(WeaponBase armaEquipada)
    {
        this.armaEquipada = armaEquipada;
    }
    private void DebugDatos()
    {
        //Debug.Log("RATON: " + coordsRaton);
        //Debug.Log("JUGADOR: " + playercoords);
        //Debug.Log("RESTA TOTAL NORMALIZED: " + normalizedCoords);
    }
}

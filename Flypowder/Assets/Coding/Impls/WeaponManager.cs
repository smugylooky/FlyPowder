using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponBase armaEquipada;
    public GameObject bala;
    private SFXManager sfx;
    private static int municionActual;
    private Rigidbody2D playerRigidBody;
    private Vector2 coordsRaton;
    private Vector2 playercoords;
    private Vector2 normalizedCoords;
    private bool disparando;
    private bool hasArmaEquipada;
    private bool recargando;
    private static bool onRecargaCooldown;
    private bool onShotCooldown;
    //bool que compruebe que se ha disparado fuera del intervalo entre disparos

    void Start()
    {
        disparando = false;
        recargando = false;
        onShotCooldown = false;
        onRecargaCooldown = false;
        sfx = GameObject.Find("SFXManager").GetComponent<SFXManager>();
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
        playercoords = transform.position;
        coordsRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        normalizedCoords = (playercoords - coordsRaton);
        normalizedCoords = normalizedCoords.normalized;

        if (!recargando && hasArmaEquipada)
        {
            if (PlayerControls.isShooting(armaEquipada.tipoDisparo) && municionActual > 0)
            {
                disparando = true;
            }

            if ((PlayerControls.isReloading() && municionActual < armaEquipada.balasCargadorMax) || municionActual == 0)
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
                if (-normalizedCoords.x > 0)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }

                BulletSetup();
                sfx.playShootingDefault();
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
            sfx.playReloadingingDefault();
            StartCoroutine(Recargar());
        }
    }

    private void BulletSetup()
    {
        GameObject bullet = Instantiate(bala);
        Destroy(bullet, 2f);
        bullet.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
        float angle = Mathf.Atan2(normalizedCoords.x, normalizedCoords.y) * Mathf.Rad2Deg;
        bullet.transform.eulerAngles = new Vector3(0,0, -angle);
        bullet.GetComponent<Rigidbody2D>().velocity = (-normalizedCoords * 50);
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

    public static int GetMunicionActual() {

        return municionActual;
    }

    public static bool isRecharging() {
        return onRecargaCooldown;
    }
}

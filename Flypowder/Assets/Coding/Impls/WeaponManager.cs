using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponBase armaEquipada;
    private SFXManager sfx;
    private SpriteRenderer weaponSprite;


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
    private BulletPoolManager bulletPoolManager;

    void Start()
    {
        InitAllVars();
        
    }
    void Update()
    {
        UpdateWeaponToMouseCoords();

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
                GetComponentInParent<PlayerManager>().ShallBeFlippedOnShot(normalizedCoords);
                bulletPoolManager.CheckBulletPool(normalizedCoords);
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

    private void UpdateWeaponToMouseCoords()
    {
        playercoords = transform.position;
        coordsRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        normalizedCoords = (playercoords - coordsRaton);
        normalizedCoords = normalizedCoords.normalized;

        transform.eulerAngles = new Vector3(0, 0, -(Mathf.Atan2(normalizedCoords.x, normalizedCoords.y) * Mathf.Rad2Deg) + 90f);
        if (normalizedCoords.x > 0) { weaponSprite.flipY = false; } else { weaponSprite.flipY = true; }
    }

    public void UpdateWeaponEquipped(WeaponBase weapon)
    {
        armaEquipada = weapon;
        InitAllVars();
    }

    private void InitAllVars()
    {
        StopAllCoroutines();
        disparando = false;
        recargando = false;
        onShotCooldown = false;
        onRecargaCooldown = false;
        weaponSprite = GetComponent<SpriteRenderer>();
        if (armaEquipada != null)
        {
            hasArmaEquipada = true;
            municionActual = armaEquipada.balasCargadorMax;
            weaponSprite.sprite = armaEquipada.spriteArma;
        }
        else
        {
            hasArmaEquipada = false;
        }
        sfx = GameObject.Find("SFXManager").GetComponent<SFXManager>();
        playerRigidBody = GetComponentInParent<Rigidbody2D>();
        bulletPoolManager = GameObject.Find("GameManager").GetComponentInChildren<BulletPoolManager>();
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

        public static int getMunicionActual()
        {

            return municionActual;
        }

        public static bool isRecharging()
        {
            return onRecargaCooldown;
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
}

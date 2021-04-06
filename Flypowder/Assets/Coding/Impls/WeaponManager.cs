using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponBase armaEquipada;
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
            if (PlayerControls.isShooting() && municionActual > 0)
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
                playerRigidBody.AddForce(normalizedCoords * armaEquipada.retroceso * Time.fixedDeltaTime * 50, ForceMode2D.Impulse);

                municionActual--;
                disparando = false;
                Debug.Log(municionActual + "/" + armaEquipada.balasCargadorMax);
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

    private IEnumerator Recargar()
    {
        yield return new WaitForSeconds(armaEquipada.tiempoRecarga);
        municionActual = armaEquipada.balasCargadorMax;
        Debug.Log("Ya se ha recargado");
        Debug.Log(municionActual + "/" + armaEquipada.balasCargadorMax);
        recargando = false;
        onRecargaCooldown = false;
    }

    private IEnumerator EsperarEntreDisparos()
    {
        yield return new WaitForSeconds(armaEquipada.tiempoEntreDisparos);
        Debug.Log("Ya se puede disparar de nuevo");
        onShotCooldown = false;
    }
    private void DebugDatos()
    {
        //Debug.Log("RATON: " + coordsRaton);
        //Debug.Log("JUGADOR: " + playercoords);
        //Debug.Log("RESTA TOTAL NORMALIZED: " + normalizedCoords);
    }
}

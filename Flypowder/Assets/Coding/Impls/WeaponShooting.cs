using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    public float multiplicadorRetroceso;
    private Rigidbody2D playerRigidBody;
    public WeaponBase armaBase;
    private Vector2 coordsRaton;
    private Vector2 playercoords;
    private Vector2 normalizedCoords;
    private bool disparando = false;
    // Update is called once per frame
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (PlayerControls.isShooting())
        {
            playercoords = transform.position;
            coordsRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            normalizedCoords = (playercoords - coordsRaton).normalized;
            disparando = true;
        }
    }

    private void FixedUpdate()
    {
        if (disparando)
        {
            playerRigidBody.AddForce(normalizedCoords * armaBase.retroceso * Time.fixedDeltaTime * 50 * multiplicadorRetroceso, ForceMode2D.Impulse);
            disparando = false;
        }
    }

    private void DebugDatos()
    {
        //Debug.Log("RATON: " + coordsRaton);
        //Debug.Log("JUGADOR: " + playercoords);
        //Debug.Log("RESTA TOTAL NORMALIZED: " + normalizedCoords);
    }
}

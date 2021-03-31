using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    public GameObject player;
    public WeaponBase armaBase;
    private Rigidbody2D rigidbodyPlayer;
    private Vector2 coordsRaton;
    private Vector2 playercoords;
    private Vector2 normalizedCoords;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            playercoords = player.transform.position;
            coordsRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rigidbodyPlayer = player.GetComponent<Rigidbody2D>();
            normalizedCoords = (playercoords - coordsRaton).normalized;

            Debug.Log("RATON: " + coordsRaton);
            Debug.Log("JUGADOR: " + playercoords);
            Debug.Log("RESTA TOTAL NORMALIZED: " + normalizedCoords);


            rigidbodyPlayer.AddForce(normalizedCoords*armaBase.retroceso*0.5f, ForceMode2D.Impulse);

        }
    }
}

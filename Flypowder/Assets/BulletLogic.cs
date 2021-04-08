using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    void Start()
    {
        Physics2D.IgnoreCollision(GameObject.Find("Bung").GetComponent<BoxCollider2D>(), this.GetComponent<BoxCollider2D>(), true);
    }

    void Awake()
    {
        Physics2D.IgnoreCollision(GameObject.Find("Bung").GetComponent<BoxCollider2D>(), this.GetComponent<BoxCollider2D>(), true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Destroy(this.gameObject);
        }
        else 
        { 
            Physics2D.IgnoreCollision(collision.collider, this.GetComponent<BoxCollider2D>(), true);
        }
    }
}

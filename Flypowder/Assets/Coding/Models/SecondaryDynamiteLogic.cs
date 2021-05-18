using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryDynamiteLogic : MonoBehaviour
{
    public SecondaryDynamite dynamite;

    private float contador;
    private float radius;
    private float explosionForce;

    void Start()
    {
        Debug.Log("EXISTO");
        contador = dynamite.detonationTime;
        radius = dynamite.explosionRadius;
        explosionForce = dynamite.explosionForce;
        GetComponent<Rigidbody2D>().mass = dynamite.weight;
        StartCoroutine(Detonate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Detonate()
    {
        yield return new WaitForSeconds(contador);
        GameObject bung = GameObject.Find("BungV2");
        Debug.Log("voy a detonar en " + transform.position.x + ", " + transform.position.y);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);
        if (Vector2.Distance(bung.transform.position, transform.position) < dynamite.explosionRadius) 
        {
            bung.GetComponent<Rigidbody2D>().AddForce(new Vector2(bung.transform.position.x - transform.position.x, bung.transform.position.y - transform.position.y) * explosionForce, ForceMode2D.Impulse);
        }

        foreach (Collider2D collided in hitColliders)
        {
            if (collided.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                collided.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(collided.transform.position.x - transform.position.x, collided.transform.position.y - transform.position.y).normalized * explosionForce, ForceMode2D.Impulse);
            }
        }
        Destroy(this.gameObject);
    }
}

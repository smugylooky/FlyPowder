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
        StartCoroutine(Detonate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Detonate()
    {
        yield return new WaitForSeconds(contador);
        Debug.Log("voy a detonar en " + transform.position.x + ", " + transform.position.y);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach(Collider2D collided in hitColliders)
        {
            if (collided.gameObject.tag == "Player" || collided.gameObject.tag == "pushable")
            {
                collided.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(collided.transform.position.x - transform.position.x, collided.transform.position.y - transform.position.y).normalized * explosionForce, ForceMode2D.Impulse);
            }
        }
        Destroy(this.gameObject);
    }
}

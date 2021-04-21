using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DisableAfterSeconds());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator DisableAfterSeconds()
    {
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
    }
}

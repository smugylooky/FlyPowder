using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public GameObject bala;
    private GameObject bungArmaModel;
    public int limiteBalas;

    private List<GameObject> listaBalas;

    private void Start()
    {
        bungArmaModel = GameObject.Find("ArmaHolder");
        listaBalas = new List<GameObject>();
        for (int i = 0; i < limiteBalas; i++)
        {
            GameObject clonBala = Instantiate(bala);
            clonBala.SetActive(false);
            listaBalas.Add(clonBala);
        }
    }

    public void CheckBulletPool(Vector2 normalizedCoords)
    {
        foreach (GameObject bullet in listaBalas)
        {
            if (bullet.activeSelf) { continue; }
            bullet.transform.position = bungArmaModel.transform.position;
            float angle = Mathf.Atan2(normalizedCoords.x, normalizedCoords.y) * Mathf.Rad2Deg;
            bullet.transform.eulerAngles = new Vector3(0, 0, -angle);
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody2D>().velocity = (-normalizedCoords * 50);
            return;
        }
    }
}

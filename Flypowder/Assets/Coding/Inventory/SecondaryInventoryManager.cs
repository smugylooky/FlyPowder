using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryInventoryManager : MonoBehaviour
{
    private SecondaryObjectBase secondaryObjectEquipado;
    private SecondaryObjectManager invSecondary;
    
    void Start()
    {
        invSecondary = GetComponentInChildren<SecondaryObjectManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "secondaryPickup")
        {
            setNewArma(collision.gameObject.GetComponentInChildren<SecondaryObjectPickup>().getSecondary());
            Destroy(collision.gameObject);
        }
    }

    private void setNewArma(SecondaryObjectBase weapon)
    {
        secondaryObjectEquipado = weapon;
        invSecondary.setSecondaryEquipped(secondaryObjectEquipado);
    }
}

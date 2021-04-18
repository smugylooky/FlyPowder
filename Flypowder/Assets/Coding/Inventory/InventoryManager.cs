using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private WeaponBase armaEquipada;
    private WeaponManager invArmas;
    private bool inside;
    //private AdditionalObject objetoAdicional
    void Start()
    {
        invArmas = GetComponentInChildren<WeaponManager>();
        inside = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "armaPickup")
        {
            if (inside) return;
            WeaponBase armaNueva = collision.gameObject.GetComponent<WeaponObject>().getWeapon();

            if (armaEquipada == null)
            {
                Destroy(collision.gameObject);
            }
            else
            {
               collision.gameObject.GetComponent<WeaponObject>().UpdateWeapon(armaEquipada);
               inside = true;
            }

            setNewArma(armaNueva);

        }
        /*if (collision.gameObject.tag == "armaPickup")
        {
            //GetComponentInChildren<WeaponManager>().UpdateWeaponEquipped();
        }*/
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inside = false;
    }

    private void setNewArma(WeaponBase weapon)
    {
        armaEquipada = weapon;
        invArmas.UpdateWeaponEquipped(armaEquipada);
    }
}

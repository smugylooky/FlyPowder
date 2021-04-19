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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "armaPickup")
        {
            if (inside) { return; }
            inside = true;
            WeaponBase armaNueva = collision.gameObject.GetComponent<WeaponObject>().getWeapon();

            if (armaEquipada == null)
            {
                Destroy(collision.gameObject);
            }
            else
            {
               collision.gameObject.GetComponent<WeaponObject>().UpdateWeapon(armaEquipada);
            }

            setNewArma(armaNueva);
            StartCoroutine(coolDownPickup());
        }
        /*if (collision.gameObject.tag == "armaPickup")
        {
            //GetComponentInChildren<WeaponManager>().UpdateWeaponEquipped();
        }*/
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "armaPickup")
        {
            inside = false;
            StopAllCoroutines();
        }
    }

    private void setNewArma(WeaponBase weapon)
    {
        armaEquipada = weapon;
        invArmas.UpdateWeaponEquipped(armaEquipada);
    }

    private IEnumerator coolDownPickup()
    {
        yield return new WaitForSeconds(1f);
        inside = false;
    }
}

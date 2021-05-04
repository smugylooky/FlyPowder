using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : MonoBehaviour
{
    public WeaponBase armaObjeto;
    private SpriteRenderer renderSprites;
    private int municionActual;
    void Start()
    {
        if (armaObjeto == null)
        {
            Destroy(this);
        }

        renderSprites = GetComponentInChildren<SpriteRenderer>();
        renderSprites.sprite = armaObjeto.objectSprite;
    }

    public WeaponBase getWeapon()
    {
        return armaObjeto;
    }

    public void UpdateWeapon(WeaponBase weapon)
    {
        armaObjeto = weapon;
        renderSprites.sprite = armaObjeto.objectSprite;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryObjectManager : MonoBehaviour
{
    public SecondaryObjectBase secondaryObjectEquipada;
    public GameObject dynamiteObject;
    public GameObject weaponManager;
    private SFXManager sfxManager;
    private SpriteRenderer spriteSecondary;

    private bool disparando;
    private bool hasSecundariaEquipada;

    private void Start()
    {
        sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
        spriteSecondary = GetComponentInChildren<SpriteRenderer>();
        InitAllVars();
    }

    void Update()
    {
        if (secondaryObjectEquipada != null)
        {
            weaponManager.SetActive(false);
            if (PlayerControls.isShooting(0))
            {
                disparando = true;
            }
        }
        else
        {
            weaponManager.SetActive(true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (disparando)
        {
            GameObject dynamiteObjectClone = Instantiate(dynamiteObject);
            dynamiteObjectClone.transform.position = transform.position;
            dynamiteObjectClone.gameObject.GetComponent<Rigidbody2D>().AddForce((Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position).normalized * 30, ForceMode2D.Impulse);
            disparando = false;
            secondaryObjectEquipada = null;
            spriteSecondary.sprite = null;
        }
    }

    public SecondaryObjectBase getSecondaryEquipped()
    {
        return secondaryObjectEquipada;
    }

    public void setSecondaryEquipped(SecondaryObjectBase newSecundaria)
    {
        secondaryObjectEquipada = newSecundaria;
        hasSecundariaEquipada = true;
        InitAllVars();
    }


    public void InitAllVars()
    {
        disparando = false;
        if (hasSecundariaEquipada)
        {
            spriteSecondary.sprite = secondaryObjectEquipada.objectSprite;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryObjectManager : MonoBehaviour
{
    public static SecondaryObjectBase secondaryObjectEquipada;
    private SFXManager sfxManager;
    public SpriteRenderer spriteSecondary;

    private bool disparando;
    private bool hasSecundariaEquipada;

    private void Start()
    {
        sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
    }

    void Update()
    {
        if (secondaryObjectEquipada != null)
        {
            if (PlayerControls.isShooting(0))
            {
                disparando = true;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (disparando)
        {
            //Instanciar dinamita
        }
    }

    public static SecondaryObjectBase getSecondaryEquipped()
    {
        return secondaryObjectEquipada;
    }

    public void setSecondaryEquipped(SecondaryObjectBase newSecundaria)
    {
        secondaryObjectEquipada = newSecundaria;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum formaDisparo { AUTOMATICO,
    SEMIAUTOMATICO,
    CORREDERACERROJO
}



public class WeaponBase : ScriptableObject
{
    /*
        DATOS DE UN ARMA:
        nombre
        descripcion
        balas en el cargador
        balas maximas
        tipo de disparo (automatico, semiautomatico, corredera/cerrojo) COMO SUBCLASES
        tipo de recarga (cargador, bala por bala)
        tiempo de recarga (total o por bala, segun att de arriba)
        retroceso
        sprite asociado
        sonidos al disparar
    */
    public string nombre;
    public string descripcion;
    public int balasCargadorMax;
    public int tipoDisparo;
    public float tiempoRecarga;
    public float tiempoEntreDisparos;
    public float retroceso;
    public Sprite spriteArma;
    public string sfx;
   
}

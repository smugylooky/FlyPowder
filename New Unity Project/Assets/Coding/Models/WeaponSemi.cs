using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Armas/Semiautomatica")]
public class WeaponSemi : WeaponBase {
    private void Awake()
    {
        nombre = "Arma semiautomática";
        descripcion = "Nueva arma semiautomática creada.";
        balasCargadorMax = 8;
        tipoDisparo = (int) formaDisparo.SEMIAUTOMATICO;
        tiempoRecarga = 3.5;
        retroceso = 10f;
    }
}

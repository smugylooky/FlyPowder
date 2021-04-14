using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Armas/Semiautomatica")]
public class WeaponSemi : WeaponBase {
    private void Awake()
    {
        tipoDisparo = (int) formaDisparo.SEMIAUTOMATICO;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ammo : MonoBehaviour
{
    public Text puntosVida;
    public Text actualAmmo;
    public bool hit = false;
    private int hp = 3;
    private int municionRestante;
   
    // Start is called before the first frame update
    void Start()
    {
        puntosVida.text = "" + hp;
        municionRestante = WeaponManager.GetMunicionActual();
        actualAmmo.text = "" + municionRestante;
    }

    // Update is called once per frame
    void Update()
    {
        municionRestante = WeaponManager.GetMunicionActual();
        actualAmmo.text = "" + municionRestante;
        puntosVida.text = "" + hp;
    }
    public void onHitTaken() {

        if (hit) {
            hp -= 10;
            puntosVida.text = "" + hp;
        }
    }
}

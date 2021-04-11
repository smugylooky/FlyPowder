using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class UIManager : MonoBehaviour
{
    public Text puntosVida;
    public Text actualAmmo;
    public bool hit = false;
    private int hp = 3;
    private int municionRestante;

    [SerializeField]
    private GameObject menuPausa;

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

    public void OnPauseClicked() {

        if (Time.timeScale == 0f) 
        {
            Time.timeScale = 1f;
            menuPausa.SetActive(false);
        }

        else
        {
            Time.timeScale = 0f;
            menuPausa.SetActive(true);
        }
    }

    public void OnResumeClicked()
    {

        Time.timeScale = 1f;
        menuPausa.SetActive(false);
    }

    public void OnSalirClicked() {
        Time.timeScale = 1f;
        Application.Quit();

    }
    public void OnMenuClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Inicio");

    }


}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Text puntosVida;
    public Text actualAmmo;
    public Text recargandoText;
    public bool hit = false;
    private int hp = 3;
    private int municionRestante;
    public TextMeshProUGUI timer;
    private float startTime;
    private string mins;
    private string secs;
    SFXManager sfxManager;

    [SerializeField]
    private FlyPowderSceneManager sceneManager;

    [SerializeField]
    private GameObject menuPausa;

    // Start is called before the first frame update
    void Start()
    {
        puntosVida.text = "" + hp;
        municionRestante = WeaponManager.getMunicionActual();
        actualAmmo.text = "" + municionRestante;
        recargandoText.enabled = false;
        startTime = Time.time;
        sfxManager = FindObjectOfType<SFXManager>();
    }

    // Update is called once per frame
    void Update()
    {
        municionRestante = WeaponManager.getMunicionActual();
        actualAmmo.text = "" + municionRestante;
        puntosVida.text = "" + hp;

        float T = Time.time - startTime;
        int tiempoMins = (int)T / 60;
        if (tiempoMins < 10)
        {
            if (tiempoMins > 0)
            {
                mins = "0" + tiempoMins.ToString() + ":";
            }
            else
            {
                mins = "";
            }
        }
        else
        {
            mins = tiempoMins.ToString() + ":";
        }
        float tiempoSegs = T % 60;
        secs = tiempoSegs < 10 ? "0" + tiempoSegs.ToString("f2") : tiempoSegs.ToString("f2");
        timer.SetText(mins + secs);

        if (WeaponManager.isRecharging()) 
        {
            recargandoText.enabled = true;
        }
        else
        {
            recargandoText.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) { OnPauseClicked(); }
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
        sfxManager.playSonidoBoton();
        Time.timeScale = 1f;
        menuPausa.SetActive(false);
    }

    public void OnSalirClicked() {
        sfxManager.playSonidoBoton();
        Time.timeScale = 1f;
        Application.Quit();

    }
    public void OnMenuClicked()
    {
        sfxManager.playSonidoBoton();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Inicio");

    }



}
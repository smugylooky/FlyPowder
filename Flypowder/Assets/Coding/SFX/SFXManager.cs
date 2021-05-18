using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private AudioClip weaponSFX,deathSFX,winSFX,jumpSFX,walkingSFX, shootingSFX, reloadSFX, sonidoCorredizaSFX, sonidoPalancaSFX, sonidoBotonSFX, sonidoCambioMapaSFX;
    private static AudioSource audioSrc;
    private WeaponBase playerWeapon;
    private static SFXManager instance = null;
    public static SFXManager Instance { get { return instance; } }

    private void Awake()
    {
        EnsureSingleton();
        DontDestroyOnLoad(this.gameObject);
    }

    private void EnsureSingleton()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

    }
    // Start is called before the first frame update
    void Start()
    {
        try { playerWeapon = GameObject.Find("BungV2").GetComponentInChildren<WeaponManager>().armaEquipada; }
        catch (Exception e){}
        
        deathSFX = Resources.Load<AudioClip>("death");
        winSFX = Resources.Load<AudioClip>("win");
        jumpSFX = Resources.Load<AudioClip>(SoundVars.SALTO);
        walkingSFX = Resources.Load<AudioClip>("walking");
        shootingSFX = Resources.Load<AudioClip>("shootingDefault");
        reloadSFX = Resources.Load<AudioClip>("reloadDefault");
        sonidoCorredizaSFX = Resources.Load<AudioClip>("sonidoCorrediza");
        sonidoPalancaSFX = Resources.Load<AudioClip>("sonidoPalanca");
        sonidoBotonSFX = Resources.Load<AudioClip>("sonidoBoton");
        sonidoCambioMapaSFX = Resources.Load<AudioClip>("sonidoCambioMapa");
        audioSrc = GetComponent<AudioSource>();
    }

    public void PlayWeapon(string weapon) 
    {
        try
        {
            weaponSFX = Resources.Load<AudioClip>(weapon + SoundVars.DISPARO);
            audioSrc.PlayOneShot(weaponSFX);
        }
        catch (Exception e) { Debug.Log("No se ha podido encontrar el sonido para el arma " + weapon); }
    }

    public void PlayDeath(){
        audioSrc.PlayOneShot(deathSFX);
    }

    public void PlayWin()
    {
        audioSrc.PlayOneShot(winSFX);
    }

    public void PlayJump()
    {
        audioSrc.PlayOneShot(jumpSFX);
    }

    public void PlayWalking()
    {
        audioSrc.PlayOneShot(walkingSFX);
    }

    public void playShootingDefault()
    {
        audioSrc.PlayOneShot(shootingSFX);
    }

    public void playReloadingingDefault()
    {
        audioSrc.PlayOneShot(reloadSFX);
    }

    public void playSonidoCambioMapa()
    {
        audioSrc.PlayOneShot(sonidoCambioMapaSFX);
    }

    public void playSonidoCorrediza()
    {
        audioSrc.PlayOneShot(sonidoCorredizaSFX);
    }

    public void playSonidoPalanca()
    {
        audioSrc.PlayOneShot(sonidoPalancaSFX);
    }

    public void playSonidoBoton()
    {
        audioSrc.PlayOneShot(sonidoBotonSFX);
    }
}

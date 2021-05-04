using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private AudioClip weaponSFX,deathSFX,winSFX,jumpSFX,walkingSFX, shootingSFX, reloadSFX;
    private static AudioSource audioSrc;
    private WeaponBase playerWeapon;
    // Start is called before the first frame update
    void Start()
    {
        playerWeapon = GameObject.Find("BungV2").GetComponentInChildren<WeaponManager>().armaEquipada;
        deathSFX = Resources.Load<AudioClip>("death");
        winSFX = Resources.Load<AudioClip>("win");
        jumpSFX = Resources.Load<AudioClip>(SoundVars.SALTO);
        walkingSFX = Resources.Load<AudioClip>("walking");
        shootingSFX = Resources.Load<AudioClip>("shootingDefault");
        reloadSFX = Resources.Load<AudioClip>("reloadDefault");
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
}

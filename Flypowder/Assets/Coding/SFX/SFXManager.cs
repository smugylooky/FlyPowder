﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private AudioClip weaponSFX,deathSFX,winSFX,jumpSFX,walkingSFX;
    private static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    { 
        deathSFX = Resources.Load<AudioClip>("death");
        winSFX = Resources.Load<AudioClip>("win");
        jumpSFX = Resources.Load<AudioClip>("jump");
        walkingSFX = Resources.Load<AudioClip>("walking");
        audioSrc = GetComponent<AudioSource>();
    }

    public void PlayWeapon(string weapon) 
    {
        try
        {
            weaponSFX = Resources.Load<AudioClip>(weapon);
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
}

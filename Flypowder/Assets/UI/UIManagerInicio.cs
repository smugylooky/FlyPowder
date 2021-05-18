using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerInicio : MonoBehaviour
{
    SFXManager sfxManager;
    // Start is called before the first frame update
    void Start()
    {
        sfxManager = SFXManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnJugarClicked()
    {
        sfxManager.playSonidoBoton();
        Time.timeScale = 1f;
        SceneManager.LoadScene("FirstLevel");


    }
    public void OnSalirClicked()
    {
        sfxManager.playSonidoBoton();
        Time.timeScale = 1f;
        Application.Quit();

    }

}

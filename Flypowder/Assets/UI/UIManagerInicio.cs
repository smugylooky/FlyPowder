using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerInicio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnJugarClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("FirstLevel");


    }
    public void OnSalirClicked()
    {
        Time.timeScale = 1f;
        Application.Quit();

    }

}

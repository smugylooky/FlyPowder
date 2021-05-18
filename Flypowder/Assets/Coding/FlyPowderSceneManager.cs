using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlyPowderSceneManager : MonoBehaviour
{
    private static FlyPowderSceneManager instance = null;
    public static FlyPowderSceneManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        EnsureSingleton();
    }

    private void EnsureSingleton()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void LoadMainMenu()
    {
        LoadScene("Inicio");
    }

    public void LoadFirstLevel()
    {
        LoadScene("FirstLevel");
    }

    public void LoadSecondLevel()
    {
        LoadScene("SecondLevel");
    }

    public void LoadThirdLevel() 
    {
        LoadScene("ThirdLevel");    
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void LoadScene(string scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }
}

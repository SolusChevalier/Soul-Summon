using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{

    private void Start()
    {
        
    }

     void Update()
    {
        

    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ReloadLevel()
    {
        //Loads level1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }

}

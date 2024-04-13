using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        //loads the next scene which is level1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private void Awake()
    {
        //locks the cursor in the center of the screen
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartGame()
    {
        //loads the next scene which is level1
        SceneManager.LoadScene(1);
    }
}
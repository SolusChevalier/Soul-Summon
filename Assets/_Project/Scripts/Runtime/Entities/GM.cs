using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    #region FIELDS

    public int enemiesKilled = 0;
    public int enemiesToKill = 25;

    #endregion FIELDS

    #region UNITY METHODS

    private void Update()
    {
        if (enemiesKilled >= enemiesToKill)
        {
            Debug.Log("You win!");
            SceneManager.LoadScene(0);
        }
    }

    #endregion UNITY METHODS

    #region METHODS

    public void EnemyKilled()
    {
        enemiesKilled++;
    }

    #endregion METHODS
}
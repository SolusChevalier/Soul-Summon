using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    #region FIELDS

    public int TotalEnemyCount { get; private set; }
    public TextMeshProUGUI enemyCountText;  // Reference to the TextMeshPro text

    //private float refreshInterval = 1f;  // Time in seconds to refresh the enemy count
    public GM gm;

    #endregion FIELDS

    #region UNITY METHODS

    private void Awake()
    {
        gm = GameObject.Find("GM").GetComponent<GM>();

        TotalEnemyCount = gm.enemiesToKill;
    }

    private void Update()
    {
        enemyCountText.text = "" + (TotalEnemyCount - gm.enemiesKilled);
    }

    /*private void Start()
    {
        StartCoroutine(RefreshEnemyCountPeriodically());
    }*/

    #endregion UNITY METHODS

    /*#region METHODS

    private IEnumerator RefreshEnemyCountPeriodically()
    {
        while (true)
        {
            UpdateEnemyCount();
            yield return new WaitForSeconds(refreshInterval);
        }
    }

    public void UpdateEnemyCount()
    {
        // Update the TextMeshPro text
        if (enemyCountText != null)
        {
            enemyCountText.text = "" + (TotalEnemyCount - gm.enemiesKilled);
        }
    }

    #endregion METHODS*/
}
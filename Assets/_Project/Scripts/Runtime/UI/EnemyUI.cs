using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    #region FIELDS

    public static EnemyUI Instance { get; private set; }

    public int TotalEnemyCount { get; private set; }
    public TextMeshProUGUI enemyCountText;  // Reference to the TextMeshPro text

    private float refreshInterval = 30f;  // Time in seconds to refresh the enemy count
    public GM gm;

    #endregion FIELDS

    #region UNITY METHODS

    private void Awake()
    {
        gm = GameObject.Find("GM").GetComponent<GM>();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        TotalEnemyCount = gm.enemiesToKill;
    }

    private void Start()
    {
        StartCoroutine(RefreshEnemyCountPeriodically());
    }

    #endregion UNITY METHODS

    #region METHODS

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

    #endregion METHODS
}
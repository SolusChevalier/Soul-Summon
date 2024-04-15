using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region FIELDS

    public int health = 100;
    public int damageTaken = 50;
    public GM gm;

    #endregion FIELDS

    private void Awake()
    {
        health = 100;
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            gm.EnemyKilled();
            Destroy(gameObject);
        }
    }

    #region METHODS

    public void TakeDamage(int dam)
    {
        health -= dam;
    }

    #endregion METHODS
}
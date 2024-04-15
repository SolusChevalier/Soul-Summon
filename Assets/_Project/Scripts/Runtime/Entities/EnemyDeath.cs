using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    #region FIELDS

    public static event System.Action<Vector3> OnDeath;

    private EntityHealth entityHealth;

    #endregion FIELDS

    #region UNITY METHODS

    private void Awake()
    {
        entityHealth = GetComponent<EntityHealth>();
    }

    #endregion UNITY METHODS

    #region METHODS

    private void HandleDeath()
    {
        OnDeath?.Invoke(transform.position);
        Destroy(gameObject);

        // Optionally, spawn loot, play a sound, or trigger any other behavior here.
    }

    #endregion METHODS
}
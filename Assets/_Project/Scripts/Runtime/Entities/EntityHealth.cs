using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : MonoBehaviour
{
    #region FIELDS

    public int maxHealth = 100;
    private int currentHealth;

    // UnityEvent that will be invoked when health changes
    public UnityEvent<int> OnHealthChanged;

    // UnityEvent that will be invoked when the entity dies
    public UnityEvent OnDied;

    #endregion FIELDS

    #region UNITY METHODS

    private void Awake()
    {
        currentHealth = maxHealth;

        OnHealthChanged?.Invoke(currentHealth);
    }

    #endregion UNITY METHODS

    #region METHODS

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Ensure health stays within bounds

        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        OnDied?.Invoke();
    }

    #endregion METHODS
}
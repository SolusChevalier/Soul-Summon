using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitingEnemy : MonoBehaviour
{
    #region FIELDS

    public SkellyLaunching skellyLaunching;

    #endregion FIELDS

    #region UNITY METHODS

    private void Awake()
    {
        skellyLaunching = GetComponentInParent<SkellyLaunching>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy");
            skellyLaunching.HitEnemy(other);
        }
    }

    #endregion UNITY METHODS
}
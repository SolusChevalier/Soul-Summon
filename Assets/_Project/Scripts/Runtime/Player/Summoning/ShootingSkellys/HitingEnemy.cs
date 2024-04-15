using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitingEnemy : MonoBehaviour
{
    #region FIELDS

    public SkellyLaunching skellyLaunching;

    #endregion FIELDS

    #region UNITY METHODS

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            skellyLaunching.HitEnemy(other);
        }
    }

    #endregion UNITY METHODS
}
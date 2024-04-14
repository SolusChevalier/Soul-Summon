using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    #region FIELDS

    public Dictionary<GameObject, HandType> HandObjectPairs = new Dictionary<GameObject, HandType>();

    #endregion FIELDS

    #region UNITY METHODS

    public void Start()
    {
        HandObjectPairs.Clear();
        var hands = GameObject.FindGameObjectsWithTag("Hand");
        foreach (var hand in hands)
        {
            HandObjectPairs.Add(hand, hand.GetComponent<HandDesignation>().HandType);
        }

        SwitchHand(HandType.idle);
    }

    #endregion UNITY METHODS

    #region METHODS

    //methods for switching active hand
    public void SwitchHand(HandType handType)
    {
        foreach (var item in HandObjectPairs.Keys)
        {
            if (HandObjectPairs[item] == handType)
            {
                item.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }
        }
    }

    #endregion METHODS
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;



    public class Summoner : MonoBehaviour
    {
    #region FIELDS

    public float CastingBuffer;



    public List<SummoningSpell> Spells = new List<SummoningSpell>();
    public Dictionary<KeyCode, HandType> keyValuePairs = new Dictionary<KeyCode, HandType>();

    #endregion

    #region UNITY METHODS
    public void Start()
    {
        keyValuePairs.Clear();
        keyValuePairs.Add(KeyCode.UpArrow, HandType.State1);
        keyValuePairs.Add(KeyCode.RightArrow, HandType.State2);
        keyValuePairs.Add(KeyCode.DownArrow, HandType.State3);
        keyValuePairs.Add(KeyCode.LeftArrow, HandType.State4);

    }
    public void Update()
    {
        foreach (var item in keyValuePairs.Keys)
        {
            if (Input.GetKeyDown(item))
            {
                Debug.Log("" + keyValuePairs[item]);
            }
        }
        
    }
    #endregion

    #region METHODS




    #endregion
}

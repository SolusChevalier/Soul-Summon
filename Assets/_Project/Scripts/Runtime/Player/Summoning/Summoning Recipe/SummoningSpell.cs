using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "SummoningSpell", menuName = "ScriptableObjects/SummoningSpell", order = 90)]
    public class SummoningSpell : ScriptableObject
{
    #region FIELDS
    public List<HandType> SpellComponents;
    public string SpellName;
    public int SpellManaCost;

    #endregion

    #region METHODS


    #endregion
}
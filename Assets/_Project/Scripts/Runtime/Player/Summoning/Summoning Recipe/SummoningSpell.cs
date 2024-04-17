using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SummoningSpell", menuName = "ScriptableObjects/SummoningSpell", order = 90)]
public class SummoningSpell : ScriptableObject
{
    #region FIELDS

    public List<HandType> SpellComponents;
    public string SpellName;
    public GameObject SkellyPrefab;
    public AudioClip spellSound;

    #endregion FIELDS

    #region METHODS

    public string GetComponentSpells()
    {
        string spell = "";
        foreach (var item in SpellComponents)
        {
            spell += item.ToString() + " ";
        }
        return spell;
    }

    #endregion METHODS
}
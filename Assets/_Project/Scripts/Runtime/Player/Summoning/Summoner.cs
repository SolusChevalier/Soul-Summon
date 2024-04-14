using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Summoner : MonoBehaviour
{
    #region FIELDS

    public float CastingBuffer;
    public HandManager HandManager;
    public GameObject Camera;
    public bool SpellReady;
    public SummoningSpell loadedSpell;
    public List<SummoningSpell> Spells = new List<SummoningSpell>();
    public List<HandType> CurrentSpell = new List<HandType>();
    public Dictionary<KeyCode, HandType> keyValuePairs = new Dictionary<KeyCode, HandType>();

    #endregion FIELDS

    #region UNITY METHODS

    public void Start()
    {
        keyValuePairs.Clear();
        keyValuePairs.Add(KeyCode.UpArrow, HandType.UpArrow);
        keyValuePairs.Add(KeyCode.RightArrow, HandType.RightArrow);
        keyValuePairs.Add(KeyCode.DownArrow, HandType.DownArrow);
        keyValuePairs.Add(KeyCode.LeftArrow, HandType.LeftArrow);
        SpellReady = false;
        loadedSpell = null;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SpellCastReset();
        }
        if (Input.GetMouseButtonDown(0) && SpellReady)
        {
            shootSkelly();
        }
        foreach (var item in keyValuePairs.Keys)
        {
            if (Input.GetKeyDown(item))
            {
                HandManager.SwitchHand(keyValuePairs[item]);
                CurrentSpell.Add(keyValuePairs[item]);
                checkIfCurrentSpellIsASpell();
            }
        }
    }

    #endregion UNITY METHODS

    #region Methods

    public void shootSkelly()
    {
        //instantiates a skelly
        var spawnPosition = transform.position + transform.forward * 2;
        //flatten the spawn rotation so it just points in the direction of the camera
        var spawnRotation = Quaternion.Euler(0, Camera.transform.rotation.eulerAngles.y, 0);
        GameObject skelly = Instantiate(loadedSpell.SkellyPrefab, spawnPosition, spawnRotation);
        var SkellyMaterial = skelly.GetComponentsInChildren<MeshRenderer>();
        foreach (var item in SkellyMaterial)
        {
            item.material = loadedSpell.SpellMaterial;
        }

        SpellCastReset();
    }

    public void SpellCastReset()
    {
        CurrentSpell.Clear();
        HandManager.SwitchHand(HandType.idle);
        SpellReady = false;
    }

    public void checkIfCurrentSpellIsASpell()
    {
        string currentSpellStr = "";
        foreach (var item in CurrentSpell)
        {
            currentSpellStr += item.ToString() + " ";
        }
        foreach (var spell in Spells)
        {
            if (currentSpellStr == spell.GetComponentSpells())
            {
                Debug.Log("Spell Ready");
                loadSpell(spell);
                return;
            }
            else
            {
                string failLog = "Spell Failed:\nCurrentSpell : " + currentSpellStr + " - " + "Spell Components : " + spell.GetComponentSpells();

                Debug.Log(failLog);
                SpellReady = false;
            }
        }
    }

    public void loadSpell(SummoningSpell spell)
    {
        Debug.Log("Spell Loaded: " + spell.SpellName);
        loadedSpell = spell;
        SpellReady = true;
    }

    #endregion Methods
}
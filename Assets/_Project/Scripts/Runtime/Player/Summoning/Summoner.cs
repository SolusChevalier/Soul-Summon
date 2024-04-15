using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : MonoBehaviour
{
    #region FIELDS

    [Header("Summoning Settings")]
    private HandManager HandManager;

    private GameObject Camera;
    private bool SpellReady;
    private SummoningSpell loadedSpell;
    public List<SummoningSpell> Spells = new List<SummoningSpell>();
    private List<HandType> CurrentSpell = new List<HandType>();
    private Dictionary<KeyCode, HandType> keyValuePairs = new Dictionary<KeyCode, HandType>();

    #endregion FIELDS

    #region UNITY METHODS

    public void Start()
    {
        HandManager = GameObject.FindGameObjectWithTag("HandManager").GetComponent<HandManager>();
        Camera = GameObject.FindGameObjectWithTag("Cam");
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
        var spawnPosition = transform.position + transform.forward * 2;

        var spawnRotation = Quaternion.Euler(0, Camera.transform.rotation.eulerAngles.y, 0);
        GameObject skelly = Instantiate(loadedSpell.SkellyPrefab, spawnPosition, spawnRotation);

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
                loadSpell(spell);
                return;
            }
            else
            {
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
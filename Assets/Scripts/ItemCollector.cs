using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemCollector : MonoBehaviour
{
    
    
    public static float Souls = 0;

    [SerializeField] TextMeshProUGUI soulText;



    private void OnTriggerEnter(Collider other)
    {

        //when the player collects a collectible the counter goes up by one
        if (other.gameObject.CompareTag("Soul"))
        {
            Destroy(other.gameObject);
            Souls++;
            soulText.text = "Souls: " + Souls;



        }
    }
}

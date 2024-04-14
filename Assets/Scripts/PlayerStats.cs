using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{

    float hitPoints = 100f;
    float damage = 10f;
    [SerializeField] TextMeshProUGUI healthText;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hitPoints = hitPoints - damage;
            healthText.text = "PH : " + hitPoints;

            if (hitPoints < 0)
            {
                Die();
            }



        }
    }
    private void Die()
    {
        //makes it look like the player is died by making them invisble and disabling movement
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<PlayerMovement>().enabled = false;

        //If the player is dead the next scene which is the end screen is loaded
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }


    //Unsure if this is needed or not
    public void TakeDamage(int dmg)
    {
        hitPoints = hitPoints - dmg;
        if (hitPoints < 0)
        {
            hitPoints = 0;
        }
    }




}
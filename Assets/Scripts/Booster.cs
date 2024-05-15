using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Booster : MonoBehaviour
{
   private bool collected = false;

    void OnCollisionEnter(Collision collision)
    {
        if (!collected && collision.gameObject.CompareTag("Player"))
        {
            collected = true;
            BoosterBar boostBar = FindObjectOfType<BoosterBar>();

            if (boostBar != null)
            {
                boostBar.UpdateBoost(1); 
            }

            Destroy(gameObject);
        }
    }
   
}

    
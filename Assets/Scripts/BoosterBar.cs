using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BoosterBar : MonoBehaviour
{
   public Slider slider;
   public int boostValue = 0; 
   private int boosterBarThreshold = 10;

    public GameObject pumpkinWeapon;
    public GameObject bananaWeapon;
    private Banana banana;
    private Pumpkin pumpkin;
    public bool pumpkinActive = false;
    //private WeaponController weaponController;
    
    
    void Start(){

        banana = GameObject.Find("Banana").GetComponent<Banana>();
        pumpkin = GameObject.Find("Pumpkin").GetComponent<Pumpkin>();
        //weaponController = GameObject.Find("Weapon Controller").GetComponent<>(WeaponController);

        bananaWeapon.SetActive(false);
        pumpkinWeapon.SetActive(false);
        boostValue = 0;
        boosterBarThreshold = 10;
        pumpkinActive = false;
        slider.maxValue = boosterBarThreshold;
        slider.value = boostValue;
        
    }

    void Update()
    {
    
    }

    public void UpdateBoost(int boostAmount)
    {
        boostValue += boostAmount; // Add the boost amount
        boostValue = Mathf.Clamp(boostValue, 0, boosterBarThreshold); 
        slider.value = boostValue; // Update the UI boost bar
        if (boostValue >= boosterBarThreshold){
            BoostSpawn();
            ResetBoostValue();
            slider.maxValue = boosterBarThreshold;
    }}
   public void ResetBoostValue(){
        boostValue = 0;
        slider.value = boostValue; // Update the UI boost bar
    }
     public void BoostSpawn()
    {
        if (boosterBarThreshold == 10)
            {
                Debug.Log("Unlocking Banana weapon");
                bananaWeapon.SetActive(true);
                boosterBarThreshold = 20; 
                //weaponController.ShootPeriodically(.5f);
            }
            else if (boosterBarThreshold == 20)
            {
                Debug.Log("Unlocking Pumpkin weapon");
                pumpkinWeapon.SetActive(true);
                pumpkinActive = true;
                 
            }
        }
}

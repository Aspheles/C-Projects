using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public float maxHunger, maxThirst, Maxhealth;
    private float hunger, thirst, health;
    public float hungerIncrease, thirstIncrease;

    private void Start()
    {
        health = Maxhealth;
        hunger = maxHunger;
        thirst = maxThirst;     
    }
     void Update()
    {
        // Thirst and Hunger
        HungerAndThirst();
       //Plyaer health management
       if(health <= 0)
        {
            Die();
        }
    }
    public void HungerAndThirst()
    {
        hunger += hungerIncrease * Time.deltaTime;
        thirst += thirstIncrease * Time.deltaTime;
        if(hunger >= maxHunger)
        {
            Die();
        }
        if(thirst >= maxThirst)
        {
            Die();
        }       
    }
    public void Die()
    {
        print("Game Over"); 
    }
    
}

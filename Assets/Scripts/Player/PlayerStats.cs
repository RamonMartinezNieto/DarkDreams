using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] private HealthBar healthBar;

    private int currentHealt = 100;
    public int CurrentHealt
    {
        get
        {
            return currentHealt;
        }
        private set
        {
            currentHealt = value; 

            if(currentHealt > 100){
                currentHealt = 100; 
            } else if(currentHealt <= 0){
                PlayerDie();
            }
        }
    }

    private void PlayerDie(){
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
    }


    public void restHealth(int health)
    {
        CurrentHealt -= health; 

        float Health = CurrentHealt / 100f;
        healthBar.SetSize(Health);

        if (Health < .3f)
        {
            healthBar.SetColor(Color.red);
        }
    }

    public void sumHealth(int health)
    {
        CurrentHealt += health; 

        float Health = CurrentHealt / 100f;
        healthBar.SetSize(Health);

    }
}

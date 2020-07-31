using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] private HealthBar healthBar;

    private int _currentHealt = 100;
    public int CurrentHealt
    {
        get
        {
            return _currentHealt;
        }
        set
        {
            _currentHealt = value; 

            if(_currentHealt <= 0){
                PlayerDie();
            }
        }
    }

    private void PlayerDie(){
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
    }


    public void restHealth(int health)
    {
        Debug.Log("hit hit hit");
        
        CurrentHealt -= health; 

        float Health = CurrentHealt / 100f;
        healthBar.SetSize(Health);

        if (Health < .3f)
        {
            healthBar.SetColor(Color.red);
        }


    }
}

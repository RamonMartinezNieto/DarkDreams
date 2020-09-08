using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] public UIHealthBar healthBar;
    [SerializeField] public UIArmorBar armorBar;

    public Slider sliderHealtBar;
    public Slider sliderArmorBar;

    public SpriteRenderer spriteRendererPlayer;

    private int currentHealt = 100;
    public int CurrentHealt
    {
        get
        {
            return currentHealt;
        }
        private set
        {
            //currentHealt = value; 
            //currentHealt = value; 
            
            //MODE GOOD: 
            currentHealt = 100;

            if (currentHealt >= 100){
                currentHealt = 100; 
            } else if(currentHealt <= 0){
                PlayerDie();
            }
            
            healthBar.SetSize(sliderHealtBar, currentHealt/100f);
        }
    }

    private float _currentArmor = .5f; 
    public float CurrentArmor
    {
        get
        {
            return _currentArmor;
        }
        private set
        {
            _currentArmor = value; 
            
            if(_currentArmor >= 1f){
                _currentArmor = 1f; 
            } else if(_currentArmor <= 0f){
                _currentArmor = 0f; 
            }

            //Update armor bar when currentArmor change (increase).
            armorBar.SetSize(sliderArmorBar,_currentArmor);
        }
    }

    public void restHealth(int damage)
    {
        //TODO: 
        //Check if t he player have armor to absorve damage
        float restHealt = armorBar.GetDamageRestArmorEffect(damage); 
        CurrentHealt -= (int) restHealt;

        if(CurrentHealt > 0)
            StartCoroutine(effectDamage());

        float Health = CurrentHealt / 100f;
    }

    public void sumHealth(int health)
    {
        CurrentHealt += health; 

        float Health = CurrentHealt / 100f;
    }

    private IEnumerator effectDamage() 
    {
        spriteRendererPlayer.color = new Color(1f,.5f,.5f);

        yield return new WaitForSeconds(0.1f);

        spriteRendererPlayer.color = Color.white;
    }

    public void restArmor(float armorDecrease) => CurrentArmor -= armorDecrease; 
    
    public void sumArmor(float armorIncrease) => CurrentArmor += armorIncrease;

    private void PlayerDie() =>  gameObject.SetActive(false); 
    
}

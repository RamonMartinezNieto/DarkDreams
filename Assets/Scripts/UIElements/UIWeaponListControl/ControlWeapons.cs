/**
 * Department: Game Developer
 * File: ControelWeapon.cs
 * Objective: This class pretend control the diferent weapons in the game, to show in a UI weapons.
 * Employee: Ramón Martínez Nieto
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * This class has a list with all weapons in game, all weapons have a important variable "IsActive" to 
 * controll if the weapon is active (in use).
 * 
 * @see Weapons#IsActive
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class ControlWeapons : MonoBehaviour
{
    /**
     * Singleton Instance of ControlWeapons
     */
    public static ControlWeapons Instance = null;

    /**
     * GameObject with the prefab on the IU show the weapons
     */
    [Tooltip("Add GameObject ")]
    public GameObject prefabShowWeapons;

    /**
     * Variable with the prefab of the secondary weapon
     */
    [Tooltip("Add prefab secondary weapon")]
    public GameObject prefabSecondaryWeapons;

    /**
     * Variable to use class UIBulletsSecondatyPrincipalWeapon
     */
    [Tooltip("")]
    public GameObject UIBulletsSecondaryPrincipalWeapon;

    /**
     * Variable to use class UIBulletsSecondaryShotGun
     */
    [Tooltip("")]
    public GameObject UIBulletsSecondaryShotGun;

    /**
     * Prefab with all content of the panel
     */
    [Tooltip("")]
    public GameObject prefabContentPanel;
    
    /**
     * List with all weapons in the game. Use a object WeaponListController to create 
     * a new layout of a weapon.
     * 
     * @see WeaponListController
     */
    List<WeaponListController> listWeaponController = new List<WeaponListController>();

    private bool isTheFirst = true; 

    void Awake()
    {
        //Singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    /**
     * Method to know how much weapons are in possesion
     */
    public int TotalWeaponsInPossesion() { return listWeaponController.Count;  }

    private void ChangeWeaponInUse()
    {
        StaticListWeapons.GetListAllWeapons().ForEach(w => 
        {
            if (w.IsActive)
            {
                w.gameObject.SetActive(true);
            }
            else 
            {
                w.gameObject.SetActive(false);
            }
        });
    }

    /**
     * Method to change what weapon is in use
     */
    public void UpdateActiveWeapon(int number) 
    {
        foreach (WeaponListController wlc in listWeaponController)
        {
            if (wlc.number == number)
            {
                wlc.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1f);
                wlc.GetComponent<RawImage>().color = Color.white;
                wlc.weaponTexture.color = new Color(1f, 1f, 1f, 1f);
            }
            else 
            {
                wlc.GetComponent<RectTransform>().localScale = new Vector3(.8f, .8f, 1f);
                wlc.GetComponent<RawImage>().color = new Color(.7f, .7f, .7f, .8f);
                wlc.weaponTexture.color = new Color(.7f, .7f, .7f, .8f);
            }
        }
    }

    /**
     * Method to add a weapon when the player catch one
     */
    public void AddNewWeaponCatched(GameObject go) 
    {
        Weapons w = go.GetComponentInChildren<Weapons>();

        GameObject newWeaponUI;
        if (isTheFirst)
        {
            newWeaponUI = Instantiate(prefabShowWeapons, prefabContentPanel.transform);
            
           
            isTheFirst = false;
        }
        else {
            newWeaponUI = Instantiate(prefabSecondaryWeapons, prefabContentPanel.transform);
 
        }

        WeaponListController wlc = newWeaponUI.GetComponent<WeaponListController>();
        wlc.weaponTexture.texture = w.weaponSpriteRender.sprite.texture;
        wlc.number = w.NumberThisWeapon;
        wlc.numberWeaponText.text = w.NumberThisWeapon.ToString(); 

        listWeaponController.Add(wlc);
    }

    /**
      * Return what weapon (gameObject) IsActive 
      */
    public int WhereIsTheActiveWeapon() 
    {
        int activeWeapon = 0;
        foreach (Weapons w in StaticListWeapons.GetListAllWeapons()) 
        {
            if (w.GetComponent<Weapons>().IsActive) activeWeapon = w.NumberThisWeapon; 
        }

        return activeWeapon; 
    }

    //Return wich weapon (gameObject) is in possesion 
    private List<Weapons> WhereWheaponsAreInPossesion()
    {
        List<Weapons> weaponsInPossesion = new List<Weapons>(); 
        
        foreach (Weapons w in StaticListWeapons.GetListAllWeapons())
        {
            if (w.GetComponent<Weapons>().IsInPossesion) weaponsInPossesion.Add(w);
        }

        return weaponsInPossesion;
    }
}
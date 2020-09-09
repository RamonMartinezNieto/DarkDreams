using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlWeapons : MonoBehaviour
{
    public static ControlWeapons Instance = null; 

    public GameObject prefabShowWeapons;
    public GameObject prefabSecondaryWeapons;

    public GameObject prefabContentPanel;
    
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


    public void AddNewWeaponCatched(GameObject go) 
    {
        Weapons w = go.GetComponentInChildren<Weapons>();

        GameObject newWeaponUI;
        if (isTheFirst)
        {
            newWeaponUI = Instantiate(prefabShowWeapons, prefabContentPanel.transform) as GameObject;
            isTheFirst = false;
        }
        else {
            newWeaponUI = Instantiate(prefabSecondaryWeapons, prefabContentPanel.transform) as GameObject;
        }

        WeaponListController wlc = newWeaponUI.GetComponent<WeaponListController>();
        wlc.weaponTexture.texture = w.weaponSpriteRender.sprite.texture;
        wlc.number = w.NumberThisWeapon;
        wlc.numberWeaponText.text = w.NumberThisWeapon.ToString(); 

        listWeaponController.Add(wlc);
    }

 

    //Return when weapon (gameObject) IsActive 
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CachableBullet : Cachable
{
    private UIBullets secondaryShootsUI;

    void Awake()
    {
        secondaryShootsUI = GameObject.Find("SecondaryShootsUI").GetComponent<UIBullets>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (UIBullets.CurrentBullets < 5)
            {
                gameObject.SetActive(false);
                secondaryShootsUI.enableBullet(UIBullets.CurrentBullets);
            }
        }
    }

}

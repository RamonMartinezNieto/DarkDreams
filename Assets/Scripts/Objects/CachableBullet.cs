/**
 * Department: Game Developer
 * File: CachableBullet.cs
 * Objective: Specific class of Cachable to create a cachable bullet of the principal weapon.
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;

/**
 *  This class provide a specific methods to control a cachable to add a secondary bullet of the principal weapon.
 *
 * @see Cachable
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class CachableBullet : Cachable
{
    private UIBullets secondaryShootsUI;

    void Awake() => secondaryShootsUI = GameObject.Find("SecondaryShootsUI").GetComponent<UIBullets>();
    
    void OnTriggerEnter2D(Collider2D other) =>  cachtBullet(other);

    private void OnTriggerStay2D(Collider2D other) =>  cachtBullet(other);

    private void cachtBullet(Collider2D other) 
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
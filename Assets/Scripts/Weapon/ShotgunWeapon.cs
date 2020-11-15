/**
 * Department: Game Developer
 * File: ShotgunWeapon.cs   
 * Objective: Specification of the Weapon to create a shotgun weapon.
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;

/**
 * 
 * This is a specification of the weapon to create a new weapon, shot gun weapon.
 * 
 * @see Weapon
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class ShotgunWeapon : Weapons
{
    protected override float TimeDelayShot { get; set; } = .5f; 

    void Start()
    {

        UpdateWeaponPosition(character.GetComponent<Transform>(), gameObject, character, transformWeapon);
        transformWeaponContainer.position = WeaponPosition;
     
        UpdateWiewPivotWeapon(gameObject, character, transformWeaponContainer);

        canShoot = true;
    }


    void Update()
    {
        if (!IsMenuWeapon)
        {
            UpdateWeaponPosition(character.GetComponent<Transform>(), gameObject, character, transformWeapon);

            transformWeaponContainer.position = WeaponPosition;
        }

        UpdateWiewPivotWeapon(gameObject, character, transformWeaponContainer);

        //Check if CanShot is true
        CanShootTiming();

        if (!Pause.GameIsPaused)
        {
            //Hold on fire
            if (Input.GetMouseButton(0) && canShoot)
                Shoting(bulletType1);

            /*
            if (Input.GetButtonDown("Fire1"))
                Shoting(bulletType1);
            */

            if (Input.GetButtonDown("Fire2") && (UIBulletsShotGun.CurrentBullets > 0))
            {
                if (!IsMenuWeapon)
                    NormalShoting(bulletType2);
            }
        }
    }

    /**
     * Override shoting because is a little different, in this case in the shoot are a diferrent individual bullets 
     * with different angles. 
     */
    public override void Shoting(GameObject bulletType)
    {
        //TODO: here is a position of the shoot, need move to ShotPrincipalWeapon, and correct the position
        Vector3 firePosition = transformWeapon.position;
        firePosition.z = -2f;

        for (int i = 0; i < 15; i++)
        {
            Instantiate(bulletType, firePosition, transformWeapon.rotation);
        }
    }

    /**
     * This is a normal shooting, onlin one shot, to use it to create differents shootings 
     */
    public void NormalShoting(GameObject bulletType)
    {
        //TODO: here is a position of the shoot, need move to ShotPrincipalWeapon, and correct the position
        Vector3 firePosition = transformWeapon.position;
        firePosition.z = -2f;

        Instantiate(bulletType, firePosition, transformWeapon.rotation);
    }

}

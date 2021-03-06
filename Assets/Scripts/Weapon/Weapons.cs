﻿/**
 * Department: Game Developer
 * File: Weapon.cs
 * Objective: Abstrac class to controll all weapons.
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;

/**
 * 
 * This class is an abstract class to controll all weapons, all weapons are under this class, this clase 
 * provide basic functionality to controll if the weapon is active, how much weapons have the user, 
 * the position of the weapons (depens of the character), rotation, and all the common basic functionality.
 * 
 * 
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
abstract public class Weapons : MonoBehaviour
{
    /**
     * This variable represent if the user have this weapon in her possesion.
     */
    public bool IsInPossesion { get; set; } = false;

    private bool _isActive;

    /**
     * This variable represent if the user have this weapon and is using in this moment.
     */
    public bool IsActive
    {
        get 
        {
            return _isActive; 
        }
        set 
        {
            this._isActive = value; 
        }
    }

    /**
     * TotalCount of the all weapons
     * 1 Principal Weapon, 2 ShotGun. TODO: Need change this variable with enumarator or other structure
     */
    public static int TotalWeapons { get; set; } = 0;

    /**
     * Number of the weapon (assign in the moment when the user catch the weapon). 
     * It deppend on the order of catching.
     */
    public int NumberThisWeapon = 0;

    /**
     * Transform of the weapon to calculate inital point shot
     */
    [Tooltip("Transform with initial point from shot")]
    public Transform transformWeapon;

    /**
     * Container of the weapon to rotate it
     */
    [Tooltip("Container of the weapon")]
    public Transform transformWeaponContainer;

    /**
     * Principal bullet type of the weapon
     */
    [Tooltip("Specific bullet type infinite armor to shot")]
    public GameObject bulletType1;

    /**
     * Secondary bullet type of the weapon
     */
    [Tooltip("Specific bullet type that only have 5 bullets")]
    public GameObject bulletType2;

    /**
     * SpriteRender of the weapon to calculate corrections in scene, it deppens of the movement' character
     */
    [Tooltip("Weapon Sprite Renderer to calculate the corrections to check correct initial bullet position")]
    public SpriteRenderer weaponSpriteRender;

    /**
     * Variable to asign the weapon to the menu
     */
    [Tooltip("Check bool when the weapoin is present in the menu")]
    public bool IsMenuWeapon;

    /**
     * GameObject of the principal character
     */
    public GameObject character;

    [HideInInspector] public bool canShoot;
    [HideInInspector] public bool canShootManual;

    /**
     * Bas damage of the bullets (this variable can change in unity menu when assing differents shots at weapon) 
     * Only assing this parameter to asing a defect damage
     */
    protected const int baseDamage = 10;

    /**
     * Base distance of the bullets, (this variable can change in unity menu when assing differents shots at weapon) 
     * Only assing this parameter to asing a defect damage
     */
    protected const float baseDistanceBullet = 2.0f;

    [System.NonSerialized()]
    protected Vector3 _weaponPosition;
    protected Vector3 WeaponPosition
    {
        get { return _weaponPosition; }
        set { _weaponPosition = value; }
    }

    [System.NonSerialized()]
    protected bool _isFrontPosition = true;
    protected bool IsFrontPosition
    {
        get { return _isFrontPosition; }
        set { 
            _isFrontPosition = value;
            //Change order depens is in bakc or front
            if (!_isFrontPosition)
            {
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
            }
            else 
            {
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
        }
    }

    [System.NonSerialized()]
    protected bool _isRunRight = true;
    protected bool IsRunRight
    {
        get { return _isRunRight; }
        set { _isRunRight = value; }
    }

    //Variables to correct the position with the charactere in scene
    [System.NonSerialized()]
    protected float _correctionXWeaponPosition = 0.19f;

    protected float CorrectionXWeaponPosition
    {
        get { return _correctionXWeaponPosition; }
        set { _correctionXWeaponPosition = value; }
    }


    [System.NonSerialized()]
    protected float _correctionYWeaponPosition = 0.080f;
    protected float CorrectionYWeaponPosition
    {
        get { return _correctionYWeaponPosition; }
        set { _correctionYWeaponPosition = value; }
    }


    [System.NonSerialized()]
    protected float _correctionZWeaponPosition = -1.0f;
    protected float CorrectionZWeaponPosition
    {
        get { return _correctionZWeaponPosition; }
        set { _correctionZWeaponPosition = value; }
    }


    [System.NonSerialized()]
    protected int _specificDamage;
    protected int SpecificDamage
    {
        set { _specificDamage = value; }
    }

    [System.NonSerialized()]
    protected int _totalDamage;

    protected int TotalDamage
    {
        get { return _specificDamage + baseDamage; }
    }

    [System.NonSerialized()]
    protected float _specificBulletDistance;
    protected float SpecificBulletDistance
    {
        get { return _specificBulletDistance + baseDistanceBullet; }
        set { _specificBulletDistance = value; }
    }

    protected abstract float TimeDelayShot { get; set; }
    protected float TimePassBewtweenShots { get; set; }
    
    protected void CanShootTiming()
    {
        TimePassBewtweenShots += Time.deltaTime;

        if (TimePassBewtweenShots >= TimeDelayShot)
        {
            canShoot = true;
            TimePassBewtweenShots = 0;
        }
        else
            canShoot = false;
    }

    /**
     * Set this weapon how active (using it) 
     */
    public void SetActiveWeapon()
    {
        gameObject.SetActive(true);
    }

    //Methods to places weapon. 
    protected virtual void UpdateWeaponPosition(Transform characterTransform, GameObject weaponObject, GameObject character, Transform transformWeaponContainer)
    {
        //TODO: I don't know why I put this here... O.o ?
        //Vector3 mousePos = Input.mousePosition;
        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 force = Vector2.ClampMagnitude(new Vector2((mousePos.x - transform.position.x), (mousePos.y - transform.position.y)), baseDistanceBullet);

        RunDirections rd = character.GetComponent<MovementPlayer>().CurrentRun;

        if (rd.Equals(RunDirections.RunE))
        {
            SetWeaponVariables(0.15f, -0.1f, -1.0f, true, true, false);
        }
        if (rd.Equals(RunDirections.RunSE))
        {
            SetWeaponVariables(0.15f, -0.15f, -1.0f, true, true, false);
        }
        else if (rd.Equals(RunDirections.RunW))
        {
            SetWeaponVariables(-0.15f, -0.1f, -1.0f, false, true, true);
        }
        else if (rd.Equals(RunDirections.RunSW))
        {
            SetWeaponVariables(-0.15f, -0.15f, -1.0f, false, true, true);
        }
        else if (rd.Equals(RunDirections.RunNE))
        {
            SetWeaponVariables(0.13f, -0.055f, 0.10f, true, false, false);
        }
        else if (rd.Equals(RunDirections.RunNW))
        {
            SetWeaponVariables(-0.13f, -0.055f, 0.10f, false, false, true);
        }
        else if (rd.Equals(RunDirections.RunS))
        {
            SetWeaponVariables(0.10f, -0.12f, -1.0f, true, true, false);
        }
        else if (rd.Equals(RunDirections.RunN))
        {
            SetWeaponVariables(0.12f, -0.070f, -1.0f, false, false, false);
        }


        WeaponPosition = new Vector3(characterTransform.position.x + CorrectionXWeaponPosition, characterTransform.position.y + CorrectionYWeaponPosition, CorrectionZWeaponPosition);

    }

    private void SetWeaponVariables(float weaponXPos, float weaponYPos, float WeaponsZPos, bool runRigth, bool isFront, bool flip)
    {
        CorrectionXWeaponPosition = weaponXPos;
        CorrectionYWeaponPosition = weaponYPos;
        CorrectionZWeaponPosition = WeaponsZPos;
        IsRunRight = runRigth;
        IsFrontPosition = isFront;
        weaponSpriteRender.flipX = flip;
    }

    protected virtual void UpdateWiewPivotWeapon(GameObject weaponObject, GameObject character, Transform weaponContainerTransform )
    {
        Transform weaponTransform = weaponObject.GetComponent<Transform>();

        SpriteRenderer spriteRendererWeapon = weaponTransform.GetComponent<SpriteRenderer>();

        Vector3 mousePos = CrossHair.getMousePosition();

        Vector2 direction = new Vector2(mousePos.x - weaponContainerTransform.position.x, mousePos.y - weaponContainerTransform.position.y);


        if (IsRunRight)
        {
            if (weaponTransform.rotation.z <= 0.7f && weaponTransform.rotation.z >= -0.7f)
            {
                spriteRendererWeapon.flipY = false;
            }
            else
            {
                spriteRendererWeapon.flipY = true;
            }
        }
        else
        {
            if (weaponTransform.rotation.z <= 0.7f && weaponTransform.rotation.z >= -0.7f)
            {
                spriteRendererWeapon.flipY = false;
                spriteRendererWeapon.flipX = false;
            }
            else
            {
                spriteRendererWeapon.flipY = true;
                spriteRendererWeapon.flipX = false;
            }
        }

        weaponContainerTransform.right = direction;
    }

    /**
     * Method to shoot a bullet. This method is virtual to override in the specification if the shoot is different. 
     * 
     * @param GameObject bulletType
     */
    public virtual void Shoting(GameObject bulletType)
    {
        //TODO: here is a position of the shoot, need move to ShotPrincipalWeapon, and correct the position
        Vector3 firePosition = transformWeapon.position;
        firePosition.z = -2f;

        GameObject newShot = Instantiate(bulletType, firePosition, transformWeapon.rotation);
    }



    //TODO: Only to debug
    protected void DebugRayCast(Transform weapon)
    {
        if (Input.mousePosition.x > 0.0f)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 force = Vector2.ClampMagnitude(new Vector2((mousePos.x - transform.position.x), (mousePos.y - transform.position.y)), baseDistanceBullet);

            Debug.DrawRay(weapon.position, force, Color.red);

        }
    }
}
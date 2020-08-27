using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

abstract public class Weapons : MonoBehaviour
{
    [Tooltip("Transform with initial point from shot")]
    public Transform transformWeapon;

    [Tooltip("Specific bullet type infinite armor to shot")]
    public GameObject bulletType1;

    [Tooltip("Specific bullet type that only have 5 bullets")]
    public GameObject bulletType2;

    [Tooltip("Weapon Sprite Renderer to calculate the corrections to check correct initial bullet position")]
    public SpriteRenderer weapon;

    [Tooltip("Check bool when the weapoin is present in the menu")]
    public bool IsMenuWeapon;

    public GameObject character;

    [HideInInspector] public bool canShoot;

    protected const int baseDamage = 10;

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
        weapon.GetComponent<SpriteRenderer>().flipX = flip;
    }

    protected virtual void UpdateWiewPivotWeapon(GameObject weaponObject, GameObject character)
    {
        Transform weaponTransform = weaponObject.GetComponent<Transform>();

        SpriteRenderer spriteRendererWeapon = weaponTransform.GetComponent<SpriteRenderer>();

        Vector3 mousePos = CrossHair.getMousePosition();

        Vector2 direction = new Vector2(mousePos.x - weaponTransform.position.x, mousePos.y - weaponTransform.position.y);

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

        weaponTransform.right = direction;
    }

    public void Shoting(GameObject bulletType)
    {
        canShoot = false; 
        
        //TODO: here is a position of the shoot, need move to ShotPrincipalWeapon, and correct the position
        Vector3 firePosition = transformWeapon.position;
        firePosition.z = -2f;
        GameObject newShot = Instantiate(bulletType, firePosition, transformWeapon.rotation);

        canShoot = true; 
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

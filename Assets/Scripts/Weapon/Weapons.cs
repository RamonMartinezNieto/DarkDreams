using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Weapons : MonoBehaviour
{
    [Tooltip("Transform with initial point from shot")]
    public Transform transformWeapon;

    [Tooltip("Specific bullet to shot")]
    public GameObject bulletType;

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
        set { _isFrontPosition = value; }
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
    protected virtual void updateWeaponPosition(Transform characterTransform, GameObject weaponObject, GameObject character, Transform transformWeaponContainer)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 force = Vector2.ClampMagnitude(new Vector2((mousePos.x - transform.position.x), (mousePos.y - transform.position.y)), baseDistanceBullet);

        RunDirections rd = character.GetComponent<MovementPlayer>().CurrentRun;
        float zPosition = -1.0f;


        // TODO: Need to refactor this part
        if (rd.Equals(RunDirections.RunE) || rd.Equals(RunDirections.RunSE))
        {
            CorrectionXWeaponPosition = 0.19f;
            CorrectionYWeaponPosition = 0.15f;
            zPosition = -1.0f;
            IsFrontPosition = true;
            IsRunRight = true;
            weaponObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (rd.Equals(RunDirections.RunW) || rd.Equals(RunDirections.RunSW))
        {
            CorrectionXWeaponPosition = -0.19f;
            CorrectionYWeaponPosition = 0.15f;
            zPosition = -1.0f;
            IsFrontPosition = true;
            IsRunRight = false;
            weaponObject.GetComponent<SpriteRenderer>().flipX = true;

        }
        else if (rd.Equals(RunDirections.RunNE))
        {
            CorrectionXWeaponPosition = 0.19f;
            CorrectionYWeaponPosition = 0.190f;
            zPosition = 0.10f;
            IsFrontPosition = false;
            IsRunRight = true;
            weaponObject.GetComponent<SpriteRenderer>().flipX = false;

        }
        else if (rd.Equals(RunDirections.RunNW))
        {
            CorrectionXWeaponPosition = -0.19f;
            CorrectionYWeaponPosition = 0.190f;
            zPosition = 0.10f;
            IsFrontPosition = false;
            IsRunRight = false;
            weaponObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (rd.Equals(RunDirections.RunS))
        {
            CorrectionXWeaponPosition = 0.100f;
            CorrectionYWeaponPosition = 0.160f;
            IsFrontPosition = true;
            IsRunRight = true;
            zPosition = -1.0f;


            weaponObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (rd.Equals(RunDirections.RunN))
        {
        }
        

        WeaponPosition = new Vector3(characterTransform.position.x + CorrectionXWeaponPosition, characterTransform.position.y + CorrectionYWeaponPosition, zPosition);

    }

    // TODO ********************************
    //Need  to apply this method 
    private void setWeaponVariables(float weaponXPos, float weaponYPos, float WeaponsZPos, bool run, bool front, bool flip, GameObject weapon)
    {
        CorrectionXWeaponPosition = weaponXPos;
        CorrectionYWeaponPosition = weaponYPos;
        CorrectionZWeaponPosition = WeaponsZPos;
        IsRunRight = run;
        IsFrontPosition = front;
        weapon.GetComponent<SpriteRenderer>().flipX = flip;
    }



    protected virtual void updateWiewPivotWeapon(GameObject weaponObject, GameObject character)
    {

        Transform weaponTransform = weaponObject.GetComponent<Transform>();
        Transform transformCharacter = character.GetComponent<Transform>();

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
                spriteRendererWeapon.flipX = true;
            }
            else
            {
                spriteRendererWeapon.flipY = true;
                spriteRendererWeapon.flipX = false;
            }
        }

        weaponTransform.right = direction;
    }

    public void Shoting()
    {
        canShoot = false; 

        Vector3 firePosition = transformWeapon.position;
        firePosition.x += weapon.bounds.size.x / 2;
        firePosition.y += 0.051f;
        firePosition.z = -2f;

        GameObject newShot = Instantiate(bulletType, firePosition, transformWeapon.rotation);

        canShoot = true; 
    }



    //Only to debug
    protected void debugRayCast(Transform weapon)
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

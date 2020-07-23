using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Weapons : MonoBehaviour
{

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
    protected bool IsFronPosition
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

        RunDirections rd = RunDirections.RunS;
        float zPosition = -1.0f;

        if (rd.Equals(RunDirections.RunE) || rd.Equals(RunDirections.RunSE))
        {
            CorrectionXWeaponPosition = 0.19f;
            CorrectionYWeaponPosition = 0.15f;
            zPosition = -1.0f;
            IsFronPosition = true;
            IsRunRight = true;
            weaponObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (rd.Equals(RunDirections.RunW) || rd.Equals(RunDirections.RunSW))
        {
            CorrectionXWeaponPosition = -0.19f;
            CorrectionYWeaponPosition = 0.15f;
            zPosition = -1.0f;
            IsFronPosition = true;
            IsRunRight = false;
            weaponObject.GetComponent<SpriteRenderer>().flipX = true;

        }
        else if (rd.Equals(RunDirections.RunNE))
        {
            CorrectionXWeaponPosition = 0.19f;
            CorrectionYWeaponPosition = 0.190f;
            zPosition = 0.10f;
            IsFronPosition = false;
            IsRunRight = true;
            weaponObject.GetComponent<SpriteRenderer>().flipX = false;

        }
        else if (rd.Equals(RunDirections.RunNW))
        {
            CorrectionXWeaponPosition = -0.19f;
            CorrectionYWeaponPosition = 0.190f;
            zPosition = 0.10f;
            IsFronPosition = false;
            IsRunRight = false;
            weaponObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (rd.Equals(RunDirections.RunS))
        {
            CorrectionXWeaponPosition = 0.100f;
            CorrectionYWeaponPosition = 0.160f;
            IsFronPosition = true;
            IsRunRight = true;
            zPosition = -1.0f;


            weaponObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (rd.Equals(RunDirections.RunN))
        {
            CorrectionXWeaponPosition = -0.120f;
            CorrectionYWeaponPosition = 0.140f;
            zPosition = 0.10f;
            IsRunRight = false;
            IsFronPosition = false;
            weaponObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        WeaponPosition = new Vector3(characterTransform.position.x + CorrectionXWeaponPosition, characterTransform.position.y + CorrectionYWeaponPosition, zPosition);

    }



    protected virtual void updateWiewPivotWeapon(GameObject weaponObject, GameObject character)
    {

        Transform weaponTransform = weaponObject.GetComponent<Transform>();
        Transform transformCharacter = character.GetComponent<Transform>();

        SpriteRenderer spriteRendererWeapon = weaponTransform.GetComponent<SpriteRenderer>();

        Vector3 mousePos = CrossHair.getMousePosition();

        Vector2 direction;


        direction = new Vector2(mousePos.x - weaponTransform.position.x, mousePos.y - weaponTransform.position.y);


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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Shot : MonoBehaviour, IShooting
{

    public GameObject shootContainer;

    [Tooltip("Shot velocity.")]
    public float shotVelocity = 2.5f;

    [Tooltip("Weapon damage.")]
    public int damage = 5;

    public virtual void MovingShot()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * shotVelocity;
    }

    public void DestroyShotAnimation()
    {
        //Debug.Log("DestroyAnimation in Shot");
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger)
        {
            if (other.gameObject.tag.Equals("Enemy"))
            {
                //When Shot hit the enemy
                Enemy enemy = other.GetComponent<Enemy>();

                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }

                DestroyShotAnimation();
                Destroy(shootContainer);

            }
            else if (!other.gameObject.tag.Equals("Player"))
            {
                DestroyShotAnimation();
                Destroy(shootContainer);
            }
        }
       
    }


    //Set arrow angle,  be carefoul, the angle that changes is the angle of the arrow container 
    public virtual void SetShotAngle(Vector3 objectiveTransform)
    {
        Quaternion _loookRotation = Quaternion.LookRotation((objectiveTransform - transform.position).normalized);
        _loookRotation.x = 0.0f; _loookRotation.y = 0.0f;

        shootContainer.GetComponent<Transform>().rotation = _loookRotation;

    }


}

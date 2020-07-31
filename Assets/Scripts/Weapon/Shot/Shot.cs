using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Shot : MonoBehaviour, IShooting
{

    protected float _shotVelocity;
    public float ShotVelocity { get; }


    protected int _shotDamage;
    public int ShotDamage { get; }

    public void MovingShot()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * _shotVelocity;
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
                    enemy.TakeDamage(_shotDamage);
                }

                DestroyShotAnimation();

            }
            else if (!other.gameObject.tag.Equals("Player"))
            {
                DestroyShotAnimation();
            }
        }
    }


}

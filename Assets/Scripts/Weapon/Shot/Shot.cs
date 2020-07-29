using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Shot : MonoBehaviour, IShooting
{

    protected float _shotVelocity;
    public float ShotVelocity { get;  }


    protected int _shotDamage;
    public int ShotDamage { get;  }

    public void MovingShot(){
        this.gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * _shotVelocity;
    }

    public void DestroyAnimation(){
        Debug.Log("DestroyAnimation in Shot");
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(!other.gameObject.tag.Equals("Player")){
            
            DestroyAnimation();

            Destroy(gameObject);
        }
    }


}

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : Shot
{
    
    //private int damage;

    private Transform playerTransform;


    
    void Awake()
    {
        //damage = GameObject.Find("SkeletonArcher").GetComponent<EnemySkeletonArcher>().Damage;
        playerTransform = GameObject.Find("Player").GetComponent<Rigidbody2D>().transform;
        
       // _shotDamage = damage;

    }

    private void Update()
    {
        //********************************************
        //NEED TO CHANGE THIS!!!!!!!!!!!!!!!!!!!!!
        //********************************************
            
        if (transform.position.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = true;
        }
    }

    void Start()
    {
        MovingShot(ArrowDirection(playerTransform));

        //SetArrowAngle(playerTransform);
        SetShotAngle(playerTransform.position); 
    }

    //sobrecharging method (orginal en Shot)
    public void MovingShot(Vector3 direction) {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = direction * shotVelocity;  
    }


    //Overriding OnTriggetEnter2D because Shot is maked to player shots. 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger)
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                //When Shot hit the enemy
                PlayerStats player = other.GetComponent<PlayerStats>();

                if (player != null)
                {
                    player.restHealth(damage); 
                }

                DestroyShotAnimation();
                Destroy(shootContainer);

            }
            else if (!other.gameObject.tag.Equals("Enemy"))
            {
                DestroyShotAnimation();
                Destroy(shootContainer);
            }
        }
    }


    //Check direction of the Arrrow takes
    private Vector3 ArrowDirection(Transform playerTransform)
    {
        Vector3 arrowDir = new Vector3(0.0f, 0.0f, 0.0f);

        if (playerTransform.position.x < 0)
        {
            arrowDir.x = (playerTransform.position.x - gameObject.transform.position.x) - 0.1f;
        }
        else
        {
            arrowDir.x = (playerTransform.position.x - gameObject.transform.position.x) + 0.1f;
        }


        if (playerTransform.position.y < 0)
        {
            arrowDir.y = (playerTransform.position.y - gameObject.transform.position.y) - 0.1f;
        }
        else
        {
            arrowDir.y = (playerTransform.position.y - gameObject.transform.position.y) + 0.1f;
        }

        return arrowDir;
    }

  
}

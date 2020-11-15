/**
 * Department: Game Developer
 * File: ArrowShot.cs 
 * Objective: Control of the enemy's arrow
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;
using System.Collections;

/**
 * 
 * This class is a specification of the shot arrow. 
 * The arrows are a fucking disturbing in my brain, 
 * 
 * PLEASE DO NOT TOUCH THIS CLASS FOR ANY REASON, seriously this class is easily broken
 * 
 * @see Shot
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class ArrowShot : Shot
{

    void Start()
    {
        if (player != null)
        {
            MovingShot(ArrowDirection(playerTransform));

            
            SetShotAngle(playerTransform.position, .2f);

            if (shootContainer.transform.position.x > playerTransform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipY = true;
            }
        }
    }

    /**
     * Oberride method (orginal en Shot)
     * 
     * @see Shot#MovingShot
     */
    public void MovingShot(Vector3 direction)
    {
        timeDuration += Time.time;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = direction * shotVelocity;
    }

    /**
     * Override of the SetShotAngel, arrows have a special form to get the correct angle. Fuck me to write this! 
     */
    public override void SetShotAngle(Vector3 objectiveTransform, float variationOfY = .0f)
    {
        objectiveTransform.y += variationOfY;

        Vector3 vect = new Vector3(transform.position.x, transform.position.y, 1f);

        Quaternion _loookRotation = Quaternion.LookRotation((objectiveTransform - vect).normalized);
        _loookRotation.x = 0.0f; _loookRotation.y = 0.0f;

        shootContainer.GetComponent<Transform>().rotation = _loookRotation;
    }

    //Overriding OnTriggetEnter2D because Shot is maked to player shots. 
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("GroundPlayerDetector"))
        {
            //When Shot hit the Player
            PlayerStats player = FindObjectOfType<PlayerStats>();

            if (player != null)
            {
                player.restHealth(damage);
            }
            StartCoroutine(DestroyShotAnimation());
        }
        else if (!other.gameObject.CompareTag("Enemy") &&
        !other.gameObject.CompareTag("GroundEnemyDetector"))
        {
            if (!other.isTrigger)
            {
                StartCoroutine(DestroyShotAnimation());
            }
        }
    }


    //Check direction of the Arrrow takes
    private Vector3 ArrowDirection(Transform playerTransform)
    {
        Vector3 arrowDir = new Vector3(0.0f, 0.0f, 0.0f);

        if (playerTransform.position.x < 0)
        {
            arrowDir.x = (playerTransform.position.x - gameObject.transform.position.x);
        }
        else
        {
            arrowDir.x = (playerTransform.position.x - gameObject.transform.position.x);
        }


        if (playerTransform.position.y < 0)
        {
            arrowDir.y = (playerTransform.position.y - gameObject.transform.position.y + 0.2f);
        }
        else
        {
            arrowDir.y = (playerTransform.position.y - gameObject.transform.position.y + 0.2f);
        }

        return arrowDir;
    }

    /**
     * Override of DestroyShotAnimation 
     * 
     * @return IEnumerator
     */
    public override IEnumerator DestroyShotAnimation()
    {
        yield return new WaitForSeconds(0);
        Destroy(gameObject);
        Destroy(shootContainer);
    }

}
using UnityEngine;

public class ArrowShot : Shot
{
    
    //private int damage;

    private Transform playerTransform;

    private GameObject player;

    void Awake()
    {
        //damage = GameObject.Find("SkeletonArcher").GetComponent<EnemySkeletonArcher>().Damage;
        player = GameObject.Find("Player");

        if (player == null) Destroy(this);
        else playerTransform = player.GetComponent<Rigidbody2D>().transform;
        

       // _shotDamage = damage;

    }

    void Start()
    {
        if (player != null)
        {
            MovingShot(ArrowDirection(playerTransform));

            //SetArrowAngle(playerTransform);
            SetShotAngle(playerTransform.position, .2f);

            if (shootContainer.transform.position.x > playerTransform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipY = true;
            }
        }
    }

    //sobrecharging method (orginal en Shot)
    public void MovingShot(Vector3 direction) {
        timeDuration += Time.time;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = direction * shotVelocity;  
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
                DestroyShotAnimation();
                Destroy(shootContainer);
            }
            else if (!other.gameObject.CompareTag("Enemy") &&
            !other.gameObject.CompareTag("GroundEnemyDetector"))
            {
                 if (!other.isTrigger)
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


}

using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    private GameObject player;

    private bool _vision; 
    public bool Vision 
    {
        get { return this._vision;  }

        private set { this._vision = value;  } 
    }

        
    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        PlayerDetect(player.GetComponent<Rigidbody2D>());
    }

    private void PlayerDetect(Rigidbody2D player)
    {
        float a = Vector2.Distance(transform.position, player.position);
        if (a < 2)
        {
            Vision = true;
        }
        else {
            Vision = false; 
        }
    }

    /*
    private float RayToPlayer(Rigidbody2D player)
    {
        Vector3 currentPos = transform.position;
        Vector3 playerPos = player.position;
        //Correction from pivot
        playerPos.y += 0.20f;

        RaycastHit2D ray = Physics2D.Raycast(currentPos, playerPos);

        Debug.DrawLine(currentPos, playerPos, Color.blue);
        
        return Vector2.Distance(transform.position, playerPos); 
    }*/

}

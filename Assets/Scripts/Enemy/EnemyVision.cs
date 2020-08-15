using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    private bool _vision; 
    public bool Vision 
    {
        get { return this._vision;  }

        private set { this._vision = value;  } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) Vision = true;         
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) Vision = false; 
    }
}

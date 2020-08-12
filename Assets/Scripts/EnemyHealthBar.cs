using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealthBar : MonoBehaviour
{
    private Transform bar; 

    private void Start() => bar = transform.Find("Bar"); 
    
    public void SetSize(float sizeNorm) => bar.localScale = new Vector3(sizeNorm, 1f); 
    
    public void SetColor(Color color) => bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    

}

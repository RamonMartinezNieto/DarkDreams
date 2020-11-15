/**
 * Department: Game Developer
 * File: EnemyHealthBar.cs
 * Objective: Control Health bar of the enemy.
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;

/**
 * Enemy Bar controller.  
 * 
 * The bar is a transform with the name "Bar", please if don't run check the name 
 * of the GameObject that contains the Green bar (health) an change the name to Bar.
 * 
 * The GameObject that contains the bar is a children of the complete GameObject "HealthBar". 
 * 
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class EnemyHealthBar : MonoBehaviour
{
    
    [HideInInspector]
    private Transform bar; 

    private void Start() => bar = transform.Find("Bar"); 
    

    /**
     * Method to change the value of the Bar, the value is from 0f to 1f. 
     * 
     * @param sizeNorm as float 
     */
    public void SetSize(float sizeNorm) => bar.localScale = new Vector3(sizeNorm, 1f);

    /**
     * Method to change the color of the Bar.
     * 
     * @param color as Color  
     */
    public void SetColor(Color color) => bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    

}

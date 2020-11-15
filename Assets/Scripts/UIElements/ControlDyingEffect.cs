/**
 * Department: Game Developer
 * File: ControlDyingEffect.cs
 * Objective: Control a layer to put on when the enemy has little life.
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;
using UnityEngine.UI;

/**
 * 
 * This class control a layer to put on when the enemy has little life. 
 * 
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class ControlDyingEffect : MonoBehaviour
{
    /**
     * Variable with the playerStats
     */
    [Tooltip("Add gameObject with the class playerStats")]
    public PlayerStats playerStats; 

    private RawImage dyingEffect;

    private void Awake()
    {
        dyingEffect = GetComponent<RawImage>(); 
    }

    private void Start()
    {
        dyingEffect.color = new Color(1f, 1f, 1f, 0f);
    }

    void Update()
    {
        var healt = playerStats.CurrentHealt;

        if (healt <= 50)
        {
            float alpha = 0.6f - ((float)healt / 100);
            dyingEffect.color = new Color(1f, 1f, 1f, alpha);
        }
        else 
        {
            dyingEffect.color = new Color(1f, 1f, 1f, 0f);
        } 
    }
}
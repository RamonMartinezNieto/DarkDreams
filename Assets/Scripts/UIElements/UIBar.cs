/**
 * Department: Game Developer
 * File: UIBar.cs
 * Objective: Have a abstract class to control the bars in the UI
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine.UI;
using UnityEngine;

/**
 * This class offer methods to control differents aspects in the ui bars.  
 * 
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public abstract class UIBar : MonoBehaviour
{

    /**
     * Add script PlayerStats (this script is in the scene in a GameObject)
     */
    [Tooltip("Add script PlayerStats")]
    public PlayerStats playerStats;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    /**
     * Mthod to change the size of the bar and changeColor if is necesary 
     * 
     * @see ChangeColor
     */
    public void SetSize(Slider slider, float sizeNorm)
    {
        slider.value = sizeNorm;
        ChangeColor(sizeNorm);
    }

    /**
     * Method to change color of  the bar
     */
    public void SetColor(string bar, UnityEngine.Color color) => GameObject.Find(bar).GetComponent<Image>().color = color;
    
    /**
     * Abstract change Color (to override) deppends of the size
     */
    public abstract void ChangeColor(float sizeNorm);

    /**
     * Abstract change value of  the bar 
     */
    public abstract void SetValueInt(float value);
}
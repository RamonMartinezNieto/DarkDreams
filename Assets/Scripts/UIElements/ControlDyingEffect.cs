using UnityEngine;
using UnityEngine.UI;

public class ControlDyingEffect : MonoBehaviour
{
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

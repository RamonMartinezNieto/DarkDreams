using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBullets : MonoBehaviour
{
    //Todo change set to private
    public static int CurrentBullets { get; private set; } = 5; 

    public Sprite disableBulletTexture;

    public Sprite enableBulletTexture;

    public RawImage[] canvasBullets;

    private void Start()
    {
        CurrentBullets = 5; 
    }

    public void disableBullet(int index) 
    {
        canvasBullets[index].texture = disableBulletTexture.texture;
        CurrentBullets--; 
    }

    public void enableBullet(int index) 
    {
        canvasBullets[index].texture = enableBulletTexture.texture;
        CurrentBullets++; 
    }
    
}

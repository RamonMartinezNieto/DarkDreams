/**
 * Department: Game Developer
 * File: CrossHair.cs
 * Objective: Control the crossHair
 * Employee: Ramón Martínez Nieto
 */
using UnityEngine;

/**
 *  Class to control the Crosshair and know the position and angle.
 * 
 *  @author Ramón Martínez Nieto
 *  @version 1.0.0
 */
public class CrossHair : MonoBehaviour
{
    /**
     * Transform of the GameObject's CrossHair
     */
    public Transform crosshairTransform;

    /**
     * Sprite with the cross hair blue. 
     */
    [Tooltip("Sprite with the cross hair blue")]
    public Sprite crossHairBlue;
    /**
     * Sprite with the cross hair red. 
     */
    [Tooltip("Sprite with the cross hair red")]
    public Sprite crossHairRed;
    /**
     * Sprite with the cross hair white. 
     */
    [Tooltip("Sprite with the cross hair white")]
    public Sprite crossHairWhite;

    private SpriteRenderer crossHairSpriteRender;

    private void Awake()
    {
        crossHairSpriteRender = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //detect mouse position
        crossHairPosition(crosshairTransform);
    }

    private void Start()
    {
        ChangeCrossHair();
        Cursor.visible = false;
    }

    /**
     * Method to update the cross hair, depends of the PlayerPrefs. Call this method 
     * when the user change the cross hair in the menus. 
     */
    public void ChangeCrossHair() 
    {
        string crossHair = PlayerPrefs.GetString("crossHair");

        switch (crossHair) 
        {
            case "blue":
                crossHairSpriteRender.sprite = crossHairBlue;        
                break;
            case "red":
                crossHairSpriteRender.sprite = crossHairRed;
                break;
            case "white":
                crossHairSpriteRender.sprite = crossHairWhite;
                break;
            default:
                crossHairSpriteRender.sprite = crossHairWhite;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision is CapsuleCollider2D) && (collision.tag == "Enemy"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red; 
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision is CapsuleCollider2D) && (collision.tag == "Enemy"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    
    private void crossHairPosition(Transform corsshariTransform)
    {
        //crosshairTransform.localPosition = mice; 
        
        corsshariTransform.position = getMousePosition();

    }

    /**
     * Method to know the position of the cross hair in the scene. 
     */
    public static Vector3 getMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
    }

    //Return number of the direction from object position
    // 0: S // 1 : SE // 2:E // 3:NE // 4:N // 5: NW // 6:W // 7:SW 
    /**
     * Method to know the angle betwween the position of
     * the crosshair and the position passed by parameters.
     * 
     * @param Vecator3 other object position
     * @return float || 0: S || 1 : SE || 2:E || 3:NE || 4:N || 5: NW || 6:W || 7:SW  ||
     */
    public float CrossHairAngle(Vector3 objectPosition){
        
        Vector3 crossHairDir = CrossHairDirection();

        Vector2 normDir = objectPosition - crossHairDir;
        float step = 360f / 8;
        float halfstep = step / 2;
        
        float angle = Vector2.SignedAngle(Vector2.up, normDir);

        angle += halfstep;
        
        if (angle < 0){
            angle += 360;
        } 

        float stepCount = angle / step;
                
        return Mathf.FloorToInt(stepCount);
    }

    /**
     * Check direction of the crosshair
     */
    private Vector3 CrossHairDirection(){
        Vector3 crossHairDir = new Vector3(0.0f,0.0f,0.0f);

        if(crosshairTransform.position.x < 0){
            crossHairDir.x = crosshairTransform.position.x - 0.1f; 
        } else {
            crossHairDir.x = crosshairTransform.position.x + 0.1f; 
        }

        if(crosshairTransform.position.y < 0 ){
            crossHairDir.y = crosshairTransform.position.y - 0.1f; 
        } else {
            crossHairDir.y = crosshairTransform.position.y + 0.1f; 
        }
        return crossHairDir;
    }
}
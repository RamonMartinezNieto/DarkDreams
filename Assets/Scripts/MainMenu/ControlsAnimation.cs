/**
 * Department: Game Developer
 * File: ControlsAnimation.cs
 * Objective: Method to create an animation in the Controls panel 
 * Employee: Ramón Martínez Nieto
 */
using System.Collections;
using UnityEngine;

/**
 * 
 * This class create a complete animation in the controls panel. 
 * To show to the user all controls that they has in the gamne.
 * 
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class ControlsAnimation : MonoBehaviour
{

    private Color KeyCapPulsed = new Color(1f, .6f, 0f, 1f);
    private Color KeyCapNoPulsed = new Color(.5f, .75f, .90f, 1f);

    /**
     * SpriteRender with the key W  
     */
    public SpriteRenderer keyW;
    /**
     * SpriteRender with the key A 
     */
    public SpriteRenderer keyA;

    /**
     * SpriteRender with the key S  
     */
    public SpriteRenderer keyS;

    /**
     * SpriteRender with the key D  
     */
    public SpriteRenderer keyD;

    /**
     * Animatior of the mini player in the W posiion
     */
    public Animator playerW;

    /**
     * Animatior of the mini player in the W posiion
     */
    public Animator playerA;

    /**
     * Animatior of the mini player in the S posiion
     */
    public Animator playerS;

    /**
     * Animatior of the mini player in the D posiion
     */
    public Animator playerD;

    /**
     * Variable to control the time between animations
     */
    public float timeRun;

    /**
     * GameObject of the panel control 
     */
    public GameObject panelControl;

    /**
     *  GameObject with the esc view
     */
    public GameObject escView;

    /**
     *  Sprite render with the key ESC
     */
    public SpriteRenderer keyESC; 

    /**
     * Sprite Renderer with the mouse in the panel 
     */
    public SpriteRenderer mouse;

    /**
     * GameObject of the weapon that the character has 
     */
    public GameObject principalWeapon;

    /**
     * Method to start all animations, depends of the Coroutine.
     * the coroutines are in an infinite loop until they leave the panel
     */
    public void StartKeysAnimation() 
    {
        ResetAllKey();

        //Start Coroutine
        StartCoroutine(RunInControl(playerW, keyW, RunDirections.RunN, RunDirections.IdleN, timeRun));
        
        StartCoroutine(MouseControl());

        StartCoroutine(EscViewControl());
    }

    /**
     * Method to reset all animations 
     */
    public void ResetAllKey() 
    {
        playerW.Play(RunDirections.IdleN.ToString());
        playerA.Play(RunDirections.IdleW.ToString());
        playerS.Play(RunDirections.IdleS.ToString());
        playerD.Play(RunDirections.IdleE.ToString());

        keyW.color = KeyCapNoPulsed;
        keyA.color = KeyCapNoPulsed;
        keyS.color = KeyCapNoPulsed;
        keyD.color = KeyCapNoPulsed;

        keyESC.color = KeyCapNoPulsed;
        escView.transform.localScale = new Vector3(.0f, .0f);
    }

    /**
     * Pas parametres first movement 
     */
    private IEnumerator RunInControl(Animator playerAnim, SpriteRenderer key, RunDirections run, RunDirections idle, float time) 
    {
        if (!panelControl.activeSelf) yield break;

        key.color = KeyCapPulsed;
        playerAnim.Play(run.ToString());

        yield return new WaitForSeconds(time);

        if (!panelControl.activeSelf) yield break;
        
        key.color = KeyCapNoPulsed;
        playerAnim.Play(idle.ToString());

        switch (knowWhatIsNext(run)) 
        {
            case 1:
                StartCoroutine(RunInControl(playerW, keyW, RunDirections.RunN, RunDirections.IdleN, timeRun));
                break;
            case 2:
                StartCoroutine(RunInControl(playerD, keyD, RunDirections.RunE, RunDirections.IdleE, timeRun));
                break;
            case 3:
                StartCoroutine(RunInControl(playerS, keyS, RunDirections.RunS, RunDirections.IdleS, timeRun));
                break;
            case 4:
                StartCoroutine(RunInControl(playerA, keyA, RunDirections.RunW, RunDirections.IdleW, timeRun));
                break;
        }
        
    }

    private IEnumerator MouseControl()
    {
        
        yield return new WaitForSeconds(1f);

        Sprite s = Resources.Load<Sprite>("Sprites/UIElements/mousePulseLeft");
        mouse.sprite = s;

        string pathPrefab = $"Prefabs/Shots/ShootMenu";
        principalWeapon.GetComponent<PrincipalWeapon>().Shoting(Resources.Load(pathPrefab, typeof(GameObject)) as GameObject); 
        
        yield return new WaitForSeconds(2.5f);

        s = Resources.Load<Sprite>("Sprites/UIElements/mouseEdit");
        mouse.sprite = s;

        yield return new WaitForSeconds(.8f);

        s = Resources.Load<Sprite>("Sprites/UIElements/mousePulseRight");
        mouse.sprite = s;

        pathPrefab = $"Prefabs/Shots/SecondaryShootMenu";
        principalWeapon.GetComponent<PrincipalWeapon>().Shoting(Resources.Load(pathPrefab, typeof(GameObject)) as GameObject);

        yield return new WaitForSeconds(2.5f);

        s = Resources.Load<Sprite>("Sprites/UIElements/mouseEdit");
        mouse.sprite = s;
        
        StartCoroutine(MouseControl());
    }

    private IEnumerator EscViewControl() 
    {
        keyESC.color = KeyCapPulsed;
        for (int i = 0; i < 24; i++) 
        {
            escView.transform.localScale += new Vector3(0.005f,0.005f);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.5f);

        keyESC.color = KeyCapNoPulsed;
        for (int a = 0; a < 24; a++)
        {
            escView.transform.localScale -= new Vector3(0.005f, 0.005f);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.4f);

        StartCoroutine(EscViewControl());
    }

    private int knowWhatIsNext(RunDirections run) 
    {
        int next = 0;
        
        if (run.Equals(RunDirections.RunN)) next = 2; 
        else if (run.Equals(RunDirections.RunE)) next = 3;
        else if (run.Equals(RunDirections.RunS)) next = 4;
        else if (run.Equals(RunDirections.RunW)) next = 1;

        return next; 
    }
}

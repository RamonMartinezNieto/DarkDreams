using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ControlsAnimation : MonoBehaviour
{

    Color KeyCapPulsed = new Color(1f, .6f, 0f, 1f);
    Color KeyCapNoPulsed = new Color(.5f, .75f, .90f, 1f);

    public SpriteRenderer keyW;
    public SpriteRenderer keyA;
    public SpriteRenderer keyS;
    public SpriteRenderer keyD;

    public Animator playerW;
    public Animator playerA;
    public Animator playerS;
    public Animator playerD;

    public float timeRun;

    public GameObject panelControl;

    public GameObject escView;
    public SpriteRenderer keyESC; 

    /**** Variables to mouse events ***/
    public SpriteRenderer mouse;
    public GameObject principalWeapon; 

    public void StartKeysAnimation() 
    {
        
        ResetAllKey();

        //Start Coroutine
        StartCoroutine(RunInControl(playerW, keyW, RunDirections.RunN, RunDirections.IdleN, timeRun));
        
        StartCoroutine(MouseControl());

        StartCoroutine(EscViewControl());
    }

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

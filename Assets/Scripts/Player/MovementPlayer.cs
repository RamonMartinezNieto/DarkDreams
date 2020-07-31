using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementPlayer : MonoBehaviour
{

    private Rigidbody2D rbody;

    [Tooltip("Velocity, only change to test diferents speeds")]
    public float speed = 2f;



    public Transform playerTransform;

    public GameObject crossHair;

    //Animator para reproducir la animación que corresponda, están numeradas
    private Animator animator;
    //Guardo la última dirección para pasar a las posicioines idle

    private DirectionMovement directionMovement;

    private RunDirections _currentRun;
    public RunDirections CurrentRun
    {
        get { return _currentRun; }
        set { this._currentRun = value; }
    }

    private RunDirections currentIdle = RunDirections.IdleS;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
        directionMovement = GetComponentInChildren<DirectionMovement>();
    }

    void FixedUpdate()
    {
        movement();
    }


    private void movement()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 currentPos = rbody.position;
        Vector2 inputVector = new Vector2(inputX, inputY);
        //inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * speed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        rbody.MovePosition(newPos);

        //Get Current Run and Current Idle 
        CurrentRun = gameObject.GetComponent<DirectionMovement>().CurrentDir;
        currentIdle = gameObject.GetComponent<DirectionMovement>().lastIdle;


        //Play animations depends if the character moves or not 
        if (currentPos.Equals(newPos))
        {
            animator.Play(currentIdle.ToString());
        }
        else
        {
            //Set Direction and play  
            CurrentRun = CurrentViewFromCroshair();

//TODO - Need to Set new last idle

            animator.Play(CurrentRun.ToString());
            
        }


    }


    //Check the currentView between the player and crossHair
    private RunDirections CurrentViewFromCroshair()
    {
        RunDirections currentView = RunDirections.RunS;

        var crossDir = crossHair.GetComponent<CrossHair>().CrossHairAngle(playerTransform.position);

        switch (crossDir)
        {
            case 0:
                currentView = RunDirections.RunS;
                break;
            case 1:
                currentView = RunDirections.RunSE;
                break;
            case 2:
                currentView = RunDirections.RunE;
                break;
            case 3:
                currentView = RunDirections.RunNE;
                break;
            case 4:
                currentView = RunDirections.RunN;
                break;
            case 5:
                currentView = RunDirections.RunNW;
                break;
            case 6:
                currentView = RunDirections.RunW;
                break;
            case 7:
                currentView = RunDirections.RunSW;
                break;
        }

        return currentView;
    }

}

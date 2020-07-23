using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementPlayer : MonoBehaviour
{

    private Rigidbody2D rbody;

    [Tooltip("Velocity, only change to test diferents speeds")]
    public float speed = 1f;

    public Transform playerTransform;

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
            //Si el ratón apunta hacia atrás, el personaje se girará y apuntará hacia esa dirección.
            // Si Va hacia el Oeste (w)
            if (CurrentRun.Equals(RunDirections.RunE) && (CrossHair.getMousePosition().x < playerTransform.position.x))
            {
                CurrentRun = RunDirections.RunW;
            }

            //Si va hacia el Este 
            else if (CurrentRun.Equals(RunDirections.RunW) && (CrossHair.getMousePosition().x > playerTransform.position.x))
            {
                CurrentRun = RunDirections.RunE;
            }

            //Si va hacia el sur
            else if (CurrentRun.Equals(RunDirections.RunS) && (CrossHair.getMousePosition().y > playerTransform.position.y) && CurrentRun.Equals(RunDirections.RunS))
            {
                CurrentRun = RunDirections.RunN;
            }

            //Si va hacia el Norte
            else if (CurrentRun.Equals(RunDirections.RunN) && (CrossHair.getMousePosition().y < playerTransform.position.y) && CurrentRun.Equals(RunDirections.RunN))
            {
                CurrentRun = RunDirections.RunS;
            }
            //Run NE direction
            else if (CurrentRun.Equals(RunDirections.RunNE))
            {
                if ((CrossHair.getMousePosition().x < playerTransform.position.x) &&
                (CrossHair.getMousePosition().y > playerTransform.position.y))
                {
                    CurrentRun = RunDirections.RunNW;
                }
                else if ((CrossHair.getMousePosition().x < playerTransform.position.x) &&
                (CrossHair.getMousePosition().y < playerTransform.position.y))
                {
                    CurrentRun = RunDirections.RunSW;
                }
                else if ((CrossHair.getMousePosition().x > playerTransform.position.x) &&
              (CrossHair.getMousePosition().y < playerTransform.position.y))
                {
                    CurrentRun = RunDirections.RunSE;
                }

            }
            //Run NW direction
            else if (CurrentRun.Equals(RunDirections.RunNW))
            {
                if ((CrossHair.getMousePosition().x > playerTransform.position.x) &&
                (CrossHair.getMousePosition().y > playerTransform.position.y))
                {
                    CurrentRun = RunDirections.RunNE;
                }
                else if ((CrossHair.getMousePosition().x > playerTransform.position.x) &&
                (CrossHair.getMousePosition().y < playerTransform.position.y))
                {
                    CurrentRun = RunDirections.RunSE;
                }
                else if ((CrossHair.getMousePosition().x < playerTransform.position.x) &&
              (CrossHair.getMousePosition().y < playerTransform.position.y))
                {
                    CurrentRun = RunDirections.RunSW;
                }

            }
            //Run SW direction
            else if (CurrentRun.Equals(RunDirections.RunSW))
            {
                if ((CrossHair.getMousePosition().x > playerTransform.position.x) &&
                (CrossHair.getMousePosition().y > playerTransform.position.y))
                {
                    CurrentRun = RunDirections.RunNE;
                }
                else if ((CrossHair.getMousePosition().x > playerTransform.position.x) &&
                (CrossHair.getMousePosition().y < playerTransform.position.y))
                {
                    CurrentRun = RunDirections.RunSE;
                }
                else if ((CrossHair.getMousePosition().x < playerTransform.position.x) &&
              (CrossHair.getMousePosition().y > playerTransform.position.y))
                {
                    CurrentRun = RunDirections.RunNW;
                }
            }
            else if (CurrentRun.Equals(RunDirections.RunSE))
            {
                if ((CrossHair.getMousePosition().x > playerTransform.position.x) &&
                (CrossHair.getMousePosition().y > playerTransform.position.y))
                {
                    CurrentRun = RunDirections.RunNE;
                }
                else if ((CrossHair.getMousePosition().x < playerTransform.position.x) &&
                (CrossHair.getMousePosition().y < playerTransform.position.y))
                {
                    CurrentRun = RunDirections.RunSW;
                }
                else if ((CrossHair.getMousePosition().x < playerTransform.position.x) &&
              (CrossHair.getMousePosition().y > playerTransform.position.y))
                {
                    CurrentRun = RunDirections.RunNW;
                }
            }

            animator.Play(CurrentRun.ToString());
        }


    }

}

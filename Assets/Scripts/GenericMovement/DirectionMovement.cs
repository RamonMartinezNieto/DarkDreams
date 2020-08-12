using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionMovement : MonoBehaviour
{

    int lastDirection;


    [Tooltip("Transform of the object that you need the direction.")]
    public Transform transformCharacter;

    private Vector2 currentPos;
    private Vector2 newPos;

    private RunDirections _currentDir;

    public RunDirections CurrentDir
    {
        get { return _currentDir; }
    }

    private RunDirections _lastIdle;

    public RunDirections lastIdle
    {
        get { return _lastIdle; }
    }




    void Awake()
    {

        //Initial direction
        _lastIdle = RunDirections.IdleS;

        //Get initial position
        currentPos = new Vector2(transformCharacter.position.x, transformCharacter.position.y);
        newPos = new Vector2(transformCharacter.position.x, transformCharacter.position.y);

    }



    void FixedUpdate()
    {
        //Update new position
        newPos = new Vector2(transformCharacter.position.x, transformCharacter.position.y);

        if (!currentPos.Equals(newPos))
        {
            checkDirection(currentPos, newPos);

            //Update current position of the object
            currentPos = newPos;
            
        }
    }


    //Check current directión, current dirrection uses to check last dir too. 
    private void checkDirection(Vector2 currentPos, Vector2 newPos)
    {

        if (newPos.x > currentPos.x && newPos.y == currentPos.y)
        {
            _currentDir = RunDirections.RunE;
            _lastIdle = RunDirections.IdleE;

        }
        else if (newPos.x < currentPos.x && newPos.y == currentPos.y)
        {
            _currentDir = RunDirections.RunW;
            _lastIdle = RunDirections.IdleW;

        }
        else if (newPos.x == currentPos.x && newPos.y > currentPos.y)
        {
            _currentDir = RunDirections.RunN;
            _lastIdle = RunDirections.IdleN;

        }
        else if (newPos.x == currentPos.x && newPos.y < currentPos.y)
        {
            _currentDir = RunDirections.RunS;
            _lastIdle = RunDirections.IdleS;

        }
        else if (newPos.x > currentPos.x && newPos.y > currentPos.y)
        {
            _currentDir = RunDirections.RunNE;
            _lastIdle = RunDirections.IdleNE;

        }
        else if (newPos.x < currentPos.x && newPos.y > currentPos.y)
        {
            _currentDir = RunDirections.RunNW;
            _lastIdle = RunDirections.IdleNW;

        }
        else if (newPos.x > currentPos.x && newPos.y < currentPos.y)
        {
            _currentDir = RunDirections.RunSE;
            _lastIdle = RunDirections.IdleSE;

        }
        else if (newPos.x < currentPos.x && newPos.y < currentPos.y)
        {
            _currentDir = RunDirections.RunSW;
            _lastIdle = RunDirections.IdleSW;

        }

    }




}

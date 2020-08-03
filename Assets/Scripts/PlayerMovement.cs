using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float horizontalMoveSpeed = 10f;
    public float forwardMoveSpeed = 20f;
    public float laneDistance = 4f;

    private int desiredLane = 1;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))      //If Player goes to right side.
        {
            MoveLane(true);
        }
        else if (Input.GetKeyDown(KeyCode.A)) //If Player goes to left side
        {
            MoveLane(false);
        }

        //Calculate where we should end up in the future
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if(desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }else if(desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * horizontalMoveSpeed;
        moveVector.y = 0f;
        moveVector.z = forwardMoveSpeed;

        controller.Move(moveVector * Time.deltaTime);
    }

    private void MoveLane(bool goRight)
    {
        desiredLane += (goRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }


}

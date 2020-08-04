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

    public Transform groundCheck;
    public float groundSphereRadius = 0.4f;
    public LayerMask groundMask;

    public float horizontalMoveSpeed = 10f;
    public float forwardMoveSpeed = 50f;
    public float maxForwardSpeed = 110f;
    public float laneDistance = 4f;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    private int desiredLane = 1;

    private Vector3 velocity;
    public bool isGrounded;

    void Start()
    {

    }

    void Update()
    {
        forwardMoveSpeed += 0.1f * Time.deltaTime;
        forwardMoveSpeed = Mathf.Clamp(forwardMoveSpeed, 0f, maxForwardSpeed);

        //Creates a tiny sphere bellow player to check for collisions with the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundSphereRadius, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.D))      //If Player goes to right side.
        {
            MoveLane(true);
        }
        else if (Input.GetKeyDown(KeyCode.A)) //If Player goes to left side
        {
            MoveLane(false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
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
        moveVector.z = forwardMoveSpeed;

        controller.Move(moveVector * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void MoveLane(bool goRight)
    {
        desiredLane += (goRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }


}

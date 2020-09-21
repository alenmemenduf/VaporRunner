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
    private CapsuleCollider collider;

    public Transform groundCheck;
    public float groundSphereRadius = 0.4f;
    public LayerMask groundMask;

    public float horizontalMoveSpeed = 10f;
    public float forwardMoveSpeed = 50f;
    public float maxForwardSpeed = 110f;
    public float speedIncreaseRate = 0.2f;
    public float laneDistance = 4f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    private Vector3 velocity;
    private int desiredLane = 1;
    public float cameraRotationAngle = 10f;

    public bool isGrounded;
    public Animator handsAnimator;


    private bool isSliding = false;
    private float originalHeight;
    public float reducedHeight;
    private float initialFOV;

    private float cameraFOVInterpolation = 0.0f;
    public float heightInterpolation = 0.0f;

    void Start()
    {
        collider = GetComponentInChildren<CapsuleCollider>();
        originalHeight = collider.height;
        initialFOV = Camera.main.fieldOfView;
    }

    void Update()
    {
        forwardMoveSpeed += speedIncreaseRate * Time.deltaTime;
        forwardMoveSpeed = Mathf.Clamp(forwardMoveSpeed, 0f, maxForwardSpeed);

        //Creates a tiny sphere bellow player to check for collisions with the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundSphereRadius, groundMask);


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

        if (Input.GetKeyDown(KeyCode.LeftControl))
            slide();
        else if (Input.GetKeyUp(KeyCode.LeftControl))
            getUpFromSlide();

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
        controller.height = collider.height;

        slideEffect();
        handsAnimator.SetBool("isSliding", isSliding);
    }

    private void MoveLane(bool goRight)
    {
        desiredLane += (goRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }

    private void slide()
    {
        isSliding = true;
        cameraFOVInterpolation = 0;
        heightInterpolation = 0;
        
    }
    private void getUpFromSlide()
    {
        isSliding = false;
        cameraFOVInterpolation = 0;
        heightInterpolation = 0;
    }

    private void slideEffect()
    {
        if(isSliding){
            Camera.main.transform.Rotate(Camera.main.transform.forward * cameraRotationAngle);

            Camera.main.fieldOfView = Mathf.Lerp(initialFOV, initialFOV + 6, cameraFOVInterpolation);
            collider.height = Mathf.Lerp(collider.height, reducedHeight, heightInterpolation);

            cameraFOVInterpolation += 0.1f;
            heightInterpolation += 0.1f;
            //collider.height = reducedHeight;
        }
        else{
           Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, initialFOV, cameraFOVInterpolation);
            collider.height = Mathf.Lerp(collider.height, originalHeight, heightInterpolation);

            cameraFOVInterpolation += 0.1f;
            heightInterpolation += 0.1f;
        }
    }


}

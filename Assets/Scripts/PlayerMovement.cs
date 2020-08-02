using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float maxHorizontalMovement = 4f;
    public float moveSpeed = 2f;
    bool isLerping = false;
    Vector3 move = Vector3.zero;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   
        if (Input.GetKeyDown(KeyCode.A) && !isLerping)
        {
            if (transform.position.x != -maxHorizontalMovement)
            {
                move = Vector3.right * -maxHorizontalMovement;
                isLerping = true;

            }
        }
        else if (Input.GetKeyDown(KeyCode.D) && !isLerping)
        {
            if (transform.position.x != maxHorizontalMovement)
            {
              move =  Vector3.right * maxHorizontalMovement;
              isLerping = true;
            }
            
        }
        if (isLerping)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + move, Time.deltaTime * moveSpeed);
        }

        if(transform.position.x <= -maxHorizontalMovement || transform.position.x >= maxHorizontalMovement)
        {
            isLerping = false;
        }


    }
}

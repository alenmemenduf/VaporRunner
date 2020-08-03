using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PM2 : MonoBehaviour
{
    public CharacterController controller;
    public float maxHorizontalMovement = 4f;
    public float moveSpeed = 2f;
    bool isLerping = false;

    Vector3 initialPosition = Vector3.zero;
    Vector3 move = Vector3.zero;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isLerping)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (transform.position.x >= -maxHorizontalMovement)
                {
                    move = Vector3.right * -maxHorizontalMovement;
                    initialPosition = transform.position;
                    isLerping = true;

                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (transform.position.x <= maxHorizontalMovement)
                {
                    move = Vector3.right * maxHorizontalMovement;
                    initialPosition = transform.position;
                    isLerping = true;
                }

            }
        }

        if (isLerping)
        {
            transform.position = Vector3.Lerp(transform.position, initialPosition + move, Time.deltaTime * moveSpeed);
            Debug.Log(initialPosition + move);
        }

        if (transform.position.x <= -maxHorizontalMovement || transform.position.x >= maxHorizontalMovement || transform.position.x >= 3.9 || transform.position.x <= -3.9)
        {
            isLerping = false;
            move = Vector3.zero;
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -maxHorizontalMovement, maxHorizontalMovement), transform.position.y, transform.position.z);


    }
}

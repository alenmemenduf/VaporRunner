using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed = 10f;
    bool isLerping = false;
    Vector3 move = Vector3.zero;
    private int lineIndex = 1;      // player should always go to lines[lineIndex] on x-axis position;
    private Vector3[] lines = {new Vector3(-4.0f,0.0f,0.0f),    // lines are the x coordinates of each line in the game, pretty hardcoded.
                               new Vector3(0.0f,0.0f,0.0f),
                               new Vector3(4.0f,0.0f,0.0f)};
    Vector3 targetPosition;     // The position the player tries to move at
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isLerping)
        {
            if (lineIndex != 0)
            {
                lineIndex--;
                targetPosition = new Vector3(lines[lineIndex].x, transform.position.y, transform.position.z);
                isLerping = true;
            }
        } // If player presses A and the player is not currently lerping on the X-axis, then, the player will move to the line on the left
        else if (Input.GetKeyDown(KeyCode.D) && !isLerping)
        {
            if(lineIndex != lines.Length - 1)
            {
                lineIndex++;
                targetPosition = new Vector3(lines[lineIndex].x, transform.position.y, transform.position.z);
                isLerping = true;
            }
        } // Same as above but on the opposite dirrection

        if (isLerping)
        {
            if (transform.position != targetPosition && Math.Abs(targetPosition.x - transform.position.x) <= 0.1f)
                transform.position = targetPosition;
            else
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        }
        /**
          * If the isLerping boolean just flipped to true, while the player is moving(lerping), 
          * check weather he ALMOST arrived, if he did, just make it arrive. Basically rounds the number after a certain threshold
          * Else, move another tenee tiny step.
          */

        if (transform.position == targetPosition) // He arrived, therefore he is not moving anymore.
            isLerping = false;
    }
}

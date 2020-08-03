using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObstacleController : MonoBehaviour
{
    public Vector3 initialPosition;
    public Vector3 targetPosition;
    public float speed = 2.5f;
    public bool rewind = false;

    private bool playerFound = false;
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerFound && !rewind)
        {
            MoveToTarget();
        }else if (rewind)
        {
            MoveToInitPos();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerFound = true;
        }
    }

    private void MoveToTarget()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, targetPosition, step);
    }

    private void MoveToInitPos()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, initialPosition, step);
        playerFound = false;
    }

    public void Rewind()
    {
        rewind = true;
    }
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindObstacle : MonoBehaviour
{
    public Vector3 targetPosition;
    public bool isRewinding = false;
    public float speed = 4f;

    List<Vector3> positions;    //Keeps track of previous positions
    private Vector3 initialPosition;
    private bool isPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.parent.position;
       
        positions = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRewinding)
        {
            Rewind();
        }else if (isPlayer && transform.parent.position != targetPosition) //If player inside trigger and object hasn't reach it's final position you can record.
        {
            float step = speed * Time.deltaTime;
            targetPosition.z = transform.parent.position.z;
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, targetPosition, step);
            Record();
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
    }
    public void StopRewind()
    {
        isRewinding = false;
    }

    void Record()
    {
        positions.Insert(0, transform.parent.position);
    }

    void Rewind()
    {
        if (positions.Count > 0)    //Makes sure index doesn't go out of bounds.
        {
            transform.parent.position = positions[0];
            positions.RemoveAt(0);

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }
}

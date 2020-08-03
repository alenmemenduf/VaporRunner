using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RewindObject();
        }
    }

    void RewindObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Rewindable"))
            {
                RewindObstacle obstacle = hit.transform.GetComponent<RewindObstacle>();
                obstacle.StartRewind();
            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionController : MonoBehaviour
{

    public float rayLength = 4f;
    public LayerMask layerMask;

    void Start()
    {
        
    }

    void Update()
    {
        Transform cam = Camera.main.transform;

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) 
        {
            RewindObject();
        }
    }

    void RewindObject()
    {
        Transform cam = Camera.main.transform;

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction, Color.green);
  
        if (Physics.Raycast(ray, out hit, rayLength, layerMask))
        {
            RewindObstacle obstacle = hit.transform.GetComponent<RewindObstacle>();
            obstacle.StartRewind();
        }
    }
}

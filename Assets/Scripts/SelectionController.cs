using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionController : MonoBehaviour
{

    public float rayLength = 4f;
    public LayerMask layerMask;
    public Material originalMaterial;

    public Color originalColor;
    public Color selectedColor;
    public float blinkingSpeed;

    private float blinkStartTime;
    private Renderer selectionRenderer;
    void Start()
    {
        blinkStartTime = Time.time;
    }

    void Update()
    {
        Transform cam = Camera.main.transform;

    
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position,cam.transform.forward, out hit, rayLength, layerMask))
        {
            Transform selection = hit.transform;
            RewindObstacle obstacle = selection.GetChild(0).GetComponent<RewindObstacle>();
            selectionRenderer = selection.GetComponent<Renderer>();

            float time = (Mathf.Sin(Time.time * blinkingSpeed) + 1);
            selectionRenderer.material.color = Color.Lerp(originalColor, selectedColor, time);

            if (Input.GetMouseButtonDown(0))
            {
                RewindObject(obstacle);
            }
        }
       
      

       
    }

    void RewindObject(RewindObstacle obstacle)
    {
        obstacle.StartRewind();
    }
}

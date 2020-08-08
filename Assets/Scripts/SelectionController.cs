using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionController : MonoBehaviour
{

    public float rayLength = 4f;
    public LayerMask layerMask;
    public Material originalMaterial;

    public ScoreManager scoreManager;

    public Color originalColor;
    public Color selectedColor;
    public float blinkingSpeed;

    public int scoreBonus;

    public Transform globeEnd;

    private Renderer selectionRenderer;
    private float blinkStartTime;


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

            selectionRenderer.material.color = selectedColor;

            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(globeEnd.GetComponent<RotationScript>().RewindGlobe());
                scoreManager.increaseScore(scoreBonus);
                RewindObject(obstacle);
           
            }
        }
        else
        {
            if (selectionRenderer)
            {
                selectionRenderer.material.color = originalColor;
            }
        }
       
       
    }

    void RewindObject(RewindObstacle obstacle)
    {
        obstacle.StartRewind();
    }
}

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
    public AudioSource laserAudio;
    public LineRenderer laserLine;

    public Color[] laserColors;

    private Renderer selectionRenderer;
    private float blinkStartTime;
    private WaitForSeconds shotDuration = new WaitForSeconds(.06f);


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
            laserLine.SetPosition(0, globeEnd.position+Vector3.forward * 0.2f);

            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(ShotEffect());
                laserLine.SetPosition(1, hit.point);
                scoreManager.increaseScore(scoreBonus);
                RewindObject(obstacle);
           
            }
        }
        else
        {
            laserLine.SetPosition(0, globeEnd.position);
            if (selectionRenderer)
            {
                selectionRenderer.material.color = originalColor;
            }
        }
       
       
    }

    void RewindObject(RewindObstacle obstacle)
    {
        obstacle.StartRewind();
        StartCoroutine(globeEnd.GetComponent<RotationScript>().RewindGlobe());
    }

    private IEnumerator ShotEffect()
    {
        laserAudio.Play();
        laserLine.enabled = true;
        laserLine.material.color = laserColors[Random.Range(0, laserColors.Length)];
        yield return shotDuration;
        laserLine.enabled = false;
    }
}

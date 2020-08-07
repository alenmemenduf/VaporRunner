using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    private WaitForSeconds rewindDuration = new WaitForSeconds(0.2f);
    public float rotationSpeed = 1f;
    public float rewindSpeedMultiplier = 5f;
    private bool isRewinding;
    Vector3 direction;
    void Start()
    {
        isRewinding = false;

    }
    void Update()
    {
        if (isRewinding)
        {
            transform.Rotate(Vector3.down * (rewindSpeedMultiplier * rotationSpeed * Time.deltaTime));
        }
        else
        {
            transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
        }
    }
    public IEnumerator RewindGlobe()
    {
        isRewinding = true;
        yield return rewindDuration;
        isRewinding = false;
    }
}

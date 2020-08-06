using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static int score;
    public float increaseRate;
    public Transform player;

    private Vector3 initialPosition;

    void Awake()
    {
        initialPosition = player.transform.position;
        Debug.Log(initialPosition);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateScore();
    }

    public void CalculateScore()
    {
        score = (int) (Vector3.Distance(player.position, initialPosition) * increaseRate);
    }
}

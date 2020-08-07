using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public int score;
    public float increaseRate;
    public Transform player;

    public Animator scoreBonusAnimator;
    private Vector3 initialPosition;

    void Awake()
    {
        initialPosition = player.transform.position;
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
    
    public void increaseScore(int bonus)
    {
        initialPosition.z -= bonus * 4;
        scoreBonusAnimator.SetTrigger("Bonus");
    }
}

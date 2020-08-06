using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;
    public Transform player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().isDead && !isGameOver)
        {
            OnGameOver();
        }
    }

    public void OnGameOver()
    {
        isGameOver = true;
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;
    }
}

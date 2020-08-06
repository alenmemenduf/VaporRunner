using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 0;
    public float corridorLength = 12f;
    public int numberOfCorridors = 20;
    public int numberOfObstacles = 10;
    public Transform playerTransform;
    public PlayerMovement player;
    public float obstacleDespawnOffset = 3f;
    public float zPositionOfFirstObject = 40f;
    public float distanceBetweenObstacles = 2f;
    public float distanceIncreaseRate = 0.001f;

    private bool firstObjectSpawned = false;
    private List<GameObject> activeCorridors = new List<GameObject>();   //This list contains tilePrefabs[0] which should be our corridor prefab.
    private List<GameObject> obstacles = new List<GameObject>();    //This list contains every other prefab, our obstacles.
    private float distanceIncreased = 0f;
    void Start()
    {
        for (int i=0;i<numberOfCorridors;i++)
            spawnCorridor();
        for (int i = 0; i < numberOfObstacles; i++)
            spawnObstacle();
    }// We spawn a couple of corridors / obstacles at first
    void Update()
    {
        if (playerTransform.position.z - corridorLength > zSpawn - (numberOfCorridors * corridorLength))    //When the player passes a corridor, spawn a corridor, then delete the one it already passed
        {
            spawnCorridor();
            deleteCorridor();
        }
        if (playerTransform.position.z > obstacles[0].transform.position.z + obstacleDespawnOffset)    // if the player passed an obstacle by some offset, destroy it and spawn another one at the end
        {
            spawnObstacle();
            deleteObstacle();
        }
        if (player.forwardMoveSpeed < player.maxForwardSpeed)
        {
            distanceIncreased = distanceIncreaseRate * player.forwardMoveSpeed;
        }
    }
    public void spawnCorridor()
    {
        GameObject ourGameObject = Instantiate(tilePrefabs[0], new Vector3(0f,0.0f,zSpawn), transform.rotation);
        activeCorridors.Add(ourGameObject);
        zSpawn += corridorLength;
    }
    private void deleteCorridor()
    {
        Destroy(activeCorridors[0]);
        activeCorridors.RemoveAt(0);
    }
    private void deleteObstacle()
    {
        Destroy(obstacles[0]);
        obstacles.RemoveAt(0);
    }
    private void spawnObstacle()
    {
        int randomIndex = UnityEngine.Random.Range(1, tilePrefabs.Length);
        Vector3 distanceAhead = new Vector3(0, 0, distanceBetweenObstacles);
        if (!firstObjectSpawned)
        {
            obstacles.Add(Instantiate(tilePrefabs[randomIndex],
                              tilePrefabs[randomIndex].transform.position + new Vector3(0, 0, zPositionOfFirstObject),      // We add these 2 vectors because some prefabs aren't particularily well centered :). We spawn the first object at some Z distance from start.
                              tilePrefabs[randomIndex].transform.rotation));
            firstObjectSpawned = true;
        }
        else
            obstacles.Add(Instantiate(tilePrefabs[randomIndex],
                                  tilePrefabs[randomIndex].transform.position + new Vector3(0, 0, obstacles[obstacles.Count-1].transform.position.z + distanceBetweenObstacles + distanceIncreased),    // same as above, this time, however, we use ( "the position on z of the last object" + distance affected by speed) instead of the base case.
                                  tilePrefabs[randomIndex].transform.rotation));
            
    }
}

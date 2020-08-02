using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 24;
    public float tileLength = 12f;
    public int numberOfTiles = 1;

    public Transform playerTransform;

    void Start()
    {
        SpawnTile(0);
    }
    void Update()
    {
        if(playerTransform.position.z > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(0);
        }
    }
    public void SpawnTile(int tileIndex)
    {
        Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        zSpawn += tileLength;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 12f;
    public int numberOfTiles = 20;
    private List<GameObject> activeTiles = new List<GameObject>();
    public Transform playerTransform;

    void Start()
    {
        for(int i=0;i<numberOfTiles;i++)
            spawnTile(0);
    }
    void Update()
    {
        if(playerTransform.position.z - tileLength > zSpawn - (numberOfTiles * tileLength))
        {
            spawnTile(0);
            deleteTile();
        }
    }
    public void spawnTile(int tileIndex)
    {
        GameObject ourGameObject = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(ourGameObject);
        zSpawn += tileLength;
    }
    private void deleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}

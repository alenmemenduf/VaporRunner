using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 12f;
    public int numberOfTiles = 20;
    private List<GameObject> activeTiles = new List<GameObject>();
    public Transform playerTransform;
    public PlayerMovement player;

    void Start()
    {
        for(int i=0;i<numberOfTiles;i++)
            spawnTile();
    }
    void Update()
    {
        if (playerTransform.position.z - tileLength > zSpawn - (numberOfTiles * tileLength))
        {
            spawnTile();
            deleteTile();
        }
    }
    public void spawnTile()
    {
        GameObject ourGameObject = Instantiate(tilePrefabs[0], new Vector3(0f,0.0f,zSpawn), transform.rotation);
        List<GameObject> obstacles = new List<GameObject>(3);
            int randomIndex = Random.Range(1, tilePrefabs.Length);
            obstacles.Add(Instantiate(tilePrefabs[randomIndex],
                                      ourGameObject.transform.position + tilePrefabs[randomIndex].transform.position + Vector3.forward * Random.Range(1,7),
                                      tilePrefabs[randomIndex].transform.rotation,
                                        ourGameObject.transform));
            
        activeTiles.Add(ourGameObject);
        zSpawn += tileLength;
    }
    private void deleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private void speedRandomizer(){
        Debug.Log(player.forwardMoveSpeed);
    }
}

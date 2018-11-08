using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour {

    public Terrain worldTerrain;
    public LayerMask terrainLayer;
    public static float terrainLeft, terrainRight, terrainTop, terrainBottom, terrainWidth, terrainLength, terrainHeight;

    public List<GameObject> objects = new List<GameObject>();

    private void Awake()
    {
        terrainLeft = worldTerrain.transform.position.x;
        terrainBottom = worldTerrain.transform.position.z;
        terrainWidth = worldTerrain.terrainData.size.x;
        terrainLength = worldTerrain.terrainData.size.z;
        terrainHeight = worldTerrain.terrainData.size.y;
        terrainRight = terrainLeft + terrainWidth;
        terrainTop = terrainBottom + terrainLength;

        InstantiateRandomPosition(objects, 0f);
    }

    public void InstantiateRandomPosition(List<GameObject> objects, float addedHeight)
    {

        int i = 0;
        float _terrainHeight = 0;
        RaycastHit hit;
        float randomPositionX, randomPositionY, randomPositionZ;
        Vector3 randomPosition = Vector3.zero;

        do
        {
            i++;
            randomPositionX = Random.Range(terrainLeft, terrainRight);
            randomPositionZ = Random.Range(terrainBottom, terrainTop);

            if (Physics.Raycast(new Vector3(randomPositionX, 500f, randomPositionZ), Vector3.down,out hit, Mathf.Infinity, terrainLayer))
            {
                _terrainHeight = hit.point.y;
            }

            randomPositionY = _terrainHeight + addedHeight;

            randomPosition = new Vector3(randomPositionX, randomPositionY, randomPositionZ);

            
            Instantiate(objects[Random.Range(0,objects.Count)], randomPosition, Quaternion.identity);
            
            

        } while (i < objects.Count);

    }
}

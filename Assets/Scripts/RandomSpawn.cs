using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour {

    public Terrain worldTerrain;
    public LayerMask terrainLayer;
    public static float terrainLeft, terrainRight, terrainTop, terrainBottom, terrainWidth, terrainLength, terrainHeight;
    bool objectsSpawning;
    public List<GameObject> powers = new List<GameObject>();
    public List<GameObject> items = new List<GameObject>();
    public List<GameObject> enemySpawns = new List<GameObject>();

    float _terrainHeight = 0;
    RaycastHit hit;
    float randomPositionX, randomPositionY, randomPositionZ;
    Vector3 randomPosition = Vector3.zero;

    int count;

    private void Awake()
    {
        
        terrainLeft = worldTerrain.transform.position.x;
        terrainBottom = worldTerrain.transform.position.z;
        terrainWidth = worldTerrain.terrainData.size.x;
        terrainLength = worldTerrain.terrainData.size.z;
        terrainHeight = worldTerrain.terrainData.size.y;
        terrainRight = terrainLeft + terrainWidth;
        terrainTop = terrainBottom + terrainLength;

        InstantiateRandomPosition(powers, items, enemySpawns);
    }

    public Vector3 GetRandomPos(float addedHeight)
    {
        randomPositionX = Random.Range(terrainLeft + 20, terrainRight - 20);
        randomPositionZ = Random.Range(terrainBottom + 20, terrainTop - 20);

        if (Physics.Raycast(new Vector3(randomPositionX, 500f, randomPositionZ), Vector3.down, out hit, Mathf.Infinity, terrainLayer))
        {
            _terrainHeight = hit.point.y;
        }

        randomPositionY = _terrainHeight + addedHeight;

        randomPosition = new Vector3(randomPositionX, randomPositionY, randomPositionZ);

        return randomPosition;
    }

    public void InstantiateRandomPosition(List<GameObject> powers, List<GameObject> items, List<GameObject> enemies)
    {

        for (int k = 0; k < enemies.Count; k++)
        {

            StartCoroutine(SpawnEnemies(enemies));

        }

        for (int i = 0; i < items.Count; i++)
        {

            StartCoroutine(SpawningObjects(items));

        }

        for (int j = 0; j < powers.Count; j++)
        {
    
            StartCoroutine(SpawningObjects(powers));

        }

    }

    IEnumerator SpawnEnemies(List<GameObject> enemyList)
    {

        yield return new WaitForSeconds(2f);

        Instantiate(enemyList[Random.Range(0, enemyList.Count)], GetRandomPos(0f), Quaternion.identity);

    }

    IEnumerator SpawningObjects(List<GameObject> objects)
    {
        //while(GameStatus.theGameIsOn == true) {
        for (int j = 0; j < 10; j++)
        {

            yield return new WaitForSeconds(1f);

            foreach (var item in objects)
            {          
               
                objectsSpawning = true;
                GameObject obj = Instantiate(objects[Random.Range(0, objects.Count)], GetRandomPos(0f), Quaternion.identity) as GameObject;
                //Debug.Log("Spawned Object " + obj);

                yield return new WaitForSeconds(10f);

                objectsSpawning = false;

                if(item.GetComponent<PowerPickUp>())
                {
                    obj.GetComponent<MeshRenderer>().enabled = false;
                    obj.GetComponent<ParticleSystem>().Stop();
                    obj.GetComponent<Collider>().enabled = false;
                    Destroy(obj, 25f);
                } else
                {
                    Destroy(obj);
                }

                //Debug.Log("objects vanish" + obj);
            }
            //}
        }
    }
}

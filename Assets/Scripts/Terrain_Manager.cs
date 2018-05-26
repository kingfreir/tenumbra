using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain_Manager : MonoBehaviour
{

    public List<GameObject> terrainPrefabs;
    public GameObject Player;

    private GameObject[] spawnedTerrain;
    private int currentIndex, nextPosX;

    private int MAX_TERRAIN_OBJECTS_ALLOWED;

    private const int TERRAIN_SIZE = 32;
    private const float CULLING_DISTANCE = 80.0f;

	// Use this for initialization
	void Start ()
    {
        //  Variable initializations.
        nextPosX = -64;
        MAX_TERRAIN_OBJECTS_ALLOWED = terrainPrefabs.Count;
        spawnedTerrain = new GameObject[MAX_TERRAIN_OBJECTS_ALLOWED];

        //  Initialize the terrain objects in the world.
        Initialize();
    }
	
	void Update ()
    {
        UpdateTerrainPosition();        
	}

    /// <summary>
    /// 
    /// </summary>
    private void Initialize()
    {
        for (int i = 0; i < MAX_TERRAIN_OBJECTS_ALLOWED; i++)
        {
            Vector3 pos = new Vector3();
            pos.x = nextPosX;
            pos.y = 0;
            pos.z = 0;

            int prefabIndex = UnityEngine.Random.Range(0, terrainPrefabs.Count - 1);

            GameObject go = Instantiate(terrainPrefabs[prefabIndex]);
            go.transform.position = pos;
            spawnedTerrain[i] = go;
            
            nextPosX += 32;
        }
        currentIndex = 0;
    }

    /// <summary>
    /// 
    /// </summary>
    private void UpdateTerrainPosition()
    {
        float posX = Player.transform.position.x - CULLING_DISTANCE;
        GameObject g = spawnedTerrain[currentIndex];

        //  Check if the Terrain object crossed the limit
        if(g.transform.position.x < posX)
        {
            g.transform.position = 
                new Vector3 (nextPosX,
                g.transform.position.y,
                g.transform.position.z);

            nextPosX += 32;

            currentIndex++;
            if (currentIndex == spawnedTerrain.Length) currentIndex = 0;
        }
    }
}

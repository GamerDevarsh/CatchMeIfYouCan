using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    private Transform player;
    public GameObject planePrefab;
    public float generationDistance = 10f;
    public float destroyDistance = 20f;

    private float lastGeneratedPosition;

    private void Start()
    {
        player = GameManager.Instance.playerGameObject.transform;
        lastGeneratedPosition = player.position.z;
    }

    private void Update()
    {
        // Calculate distance between player and last generated terrain
        float distanceToLastTerrain = player.position.z - lastGeneratedPosition;

        // Generate new terrain if distance exceeds generation distance
        if (distanceToLastTerrain >= generationDistance)
        {
            InstantiateTerrain();
        }

        // Destroy old terrain if distance exceeds destroy distance
        if (distanceToLastTerrain >= destroyDistance)
        {
            DestroyTerrain();
        }
    }

    private void InstantiateTerrain()
    {
        Vector3 spawnPosition = new Vector3(0f, 0f, lastGeneratedPosition + generationDistance);
        Instantiate(planePrefab, spawnPosition, Quaternion.identity);
        lastGeneratedPosition += generationDistance;
    }

    private void DestroyTerrain()
    {
        GameObject[] terrainObjects = GameObject.FindGameObjectsWithTag("Terrain");

        foreach (GameObject terrain in terrainObjects)
        {
            if (terrain.transform.position.z < player.position.z - destroyDistance)
            {
                Destroy(terrain);
            }
        }
    }
}


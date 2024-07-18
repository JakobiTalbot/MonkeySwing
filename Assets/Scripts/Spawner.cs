using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Vector3 spawnOffset = new Vector3(50f, 0f, 0f);
    [SerializeField]
    private Vector2 ySpawnMinMax = new Vector2(-8f, 8f);
    [Header("Pickups")]
    [SerializeField]
    private float distanceBetweenPickupSpawns = 100f;
    [SerializeField]
    private GameObject[] pickups;
    [Header("Hazards")]
    [SerializeField]
    private float distanceBetweenHazardSpawns = 100f;
    [SerializeField]
    private GameObject[] hazards;


    private float furthestX;
    private float distanceTravelledSinceLastPickupSpawn = 0f;
    private float distanceTravelledSinceLastHazardSpawn = 0f;

    private void Start()
    {
        furthestX = transform.position.x;
    }

    private void Update()
    {
        // don't bother if player hasn't moved forward
        if (transform.position.x <= furthestX)
            return;

        float deltaX = transform.position.x - furthestX;
        distanceTravelledSinceLastPickupSpawn += deltaX;
        distanceTravelledSinceLastHazardSpawn += deltaX;

        // spawn pickup if travelled set distance
        if (distanceTravelledSinceLastPickupSpawn > distanceBetweenPickupSpawns)
            SpawnPickup();
        if (distanceTravelledSinceLastHazardSpawn > distanceBetweenPickupSpawns)
            SpawnHazard();

        furthestX = transform.position.x;
    }

    private void SpawnPickup()
    {
        distanceTravelledSinceLastPickupSpawn = 0f;

        // generate pickup spawn position
        Vector3 spawnPos = transform.position + spawnOffset;
        spawnPos.y += Random.Range(ySpawnMinMax.x, ySpawnMinMax.y);
        // get index of pickup to spawn
        int i = Random.Range(0, pickups.Length);

        Instantiate(pickups[i], spawnPos, pickups[i].transform.rotation);
    }

    private void SpawnHazard()
    {
        distanceTravelledSinceLastHazardSpawn = 0f;

        // generate hazard spawn position
        Vector3 spawnPos = transform.position + spawnOffset;
        spawnPos.y += Random.Range(ySpawnMinMax.x, ySpawnMinMax.y);
        // get index of hazard to spawn
        int i = Random.Range(0, pickups.Length);

        Instantiate(hazards[i], spawnPos, hazards[i].transform.rotation);
    }
}
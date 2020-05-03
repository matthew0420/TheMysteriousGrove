using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    private GameObject playerObject;

    public int currentTime;
    public int currentCreatures = 0;
    public int maxCreaturesThatCanSpawn = 10;
    public int spawnTime = 1000;

    //stuff for spawning rabbit
    public GameObject rabbitObject;
    public GameObject newRabbit;
    public Collider2D[] colliders;
    public float spawnRadius;
    public bool canSpawnHere = false;
    public Vector3 spawnPos;

    void Start()
    {
        Invoke("FindPlayer", 0.2f);
    }

    private void FixedUpdate()
    {
        if (currentTime <= spawnTime)
        {
            currentTime++;
        }
        if (currentTime > spawnTime && currentCreatures <= maxCreaturesThatCanSpawn)
        {
            SpawnRabbit();
        }
    }

    public void FindPlayer()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    public void SpawnRabbit()
    {
        float spawnPosX = playerObject.transform.position.x + Random.Range(-5f, 5f);
        float spawnPosY = playerObject.transform.position.y + Random.Range(-5f, 5f);
        spawnPos = new Vector3(spawnPosX, spawnPosY, 0);
        canSpawnHere = PreventSpawnOverlap(spawnPos);

        if (canSpawnHere == true)
        {
            newRabbit = Instantiate(rabbitObject, spawnPos, Quaternion.identity) as GameObject;
            currentCreatures++;
            currentTime = 0;
        }
        else
        {
            SpawnRabbit();
        }
    }

    public bool PreventSpawnOverlap(Vector3 spawnPos)
    {
        colliders = Physics2D.OverlapCircleAll(spawnPos, spawnRadius);

        if (colliders.Length == 0)
        {
            return true;
        }

        return false;
    }

}

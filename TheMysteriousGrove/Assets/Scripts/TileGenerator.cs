using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
//Ideally, this script is going to only be called when the game initially starts for the
//first time, with no previous save data available. terrain and player pos will be 
//spawned in from then on with a yet to be established save system
public class TileGenerator : MonoBehaviour
{
    //Stuff for spawning terrain
    //-------------------------------------------
    [Range(0, 100)]
    public int iniChance;

    [Range(1, 8)]
    public int birthLimit;

    [Range(1, 8)]
    public int deathLimit;

    [Range(1, 10)]
    public int numR;

    private int count = 0;

    private int[,] terrainMap;
    public Vector3Int tmapSize;

    public Tilemap topMap;
    public Tilemap topMapWater;
    public Tilemap botMap;
    public RuleTile topTile;
    public RuleTile topTileWater;
    public Tile botTile;

    int width;
    int height;
    //-------------------------------------------

    //Stuff for spawning player
    //-------------------------------------------
    public GameObject playerObject;
    public GameObject newPlayer;
    public Collider2D[] colliders;
    public float spawnRadius;
    public bool canSpawnHere = false;
    public Vector3 spawnPos;

    //-------------------------------------------


    private void Start()
    {
        DoSim(numR);
        DoSim(numR);
        DoSim(numR);
        Invoke("SpawnPlayer", 0.1f);
    }

    public void SpawnPlayer()
    {
        float spawnPosX = Random.Range(-15.0f, 15.0f);
        float spawnPosY = Random.Range(-15.0f, 15.0f);
        spawnPos = new Vector3(spawnPosX, spawnPosY, 0);
        canSpawnHere = PreventSpawnOverlap(spawnPos);

        if (canSpawnHere == true)
        {
            newPlayer = Instantiate(playerObject, spawnPos, Quaternion.identity) as GameObject;
        }
        else
        {
            SpawnPlayer();
        }
    }

    public bool PreventSpawnOverlap(Vector3 spawnPos)
    {
        colliders = Physics2D.OverlapCircleAll(spawnPos, spawnRadius);

        if (colliders.Length < 1)
        {
            Debug.Log("Spawn here!");
            return true;
        }

        return false;
    }

    public void DoSim(int numR)
    {
        clearMap(false);
        width = tmapSize.x;
        height = tmapSize.y;

        if (terrainMap == null)
        {
            terrainMap = new int[width, height];
            initPos();
        }

        for (int i = 0; i < numR; i++)
        {
            terrainMap = genTilePos(terrainMap);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (terrainMap[x, y] == 1)
                    topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTile);
                //botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), botTile);
                if (Random.Range(0, 1001) < 998)
                {
                    botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), botTile);
                }
                else
                {
                    topMapWater.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTileWater);
                }
            }
        }
    }

    public int [,] genTilePos(int[,] oldMap)
    {
        int[,] newMap = new int[width, height];
        int neighb;
        BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighb = 0;
                foreach (var b in myB.allPositionsWithin)
                {
                    if (b.x == 0 && b.y == 0) continue;
                    if (x+b.x >= 0 && x+b.x < width && y+b.y >= 0 && y+b.y < height)
                    {
                        neighb += oldMap[x + b.x, y + b.y];
                    }
                    else
                    {
                        neighb++;
                    }
                }

                if (oldMap[x, y] == 1)
                {
                    if (neighb < deathLimit) newMap[x, y] = 0;
                    else
                    {
                        newMap[x, y] = 1;
                    }
                }

                if (oldMap[x, y] == 0)
                {
                    if (neighb > birthLimit) newMap[x, y] = 1;
                    else
                    {
                        newMap[x, y] = 0;
                    }
                }
            }
        }

        return newMap;
    }

    public void initPos()
    {
        for (int x = 0; x < width; x++)
        {

            for (int y = 0; y < height; y++)
            {
                terrainMap[x, y] = Random.Range(1, 101) < iniChance ? 1 : 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //generates landscape
            //DoSim(numR);
        }

        if (Input.GetMouseButtonDown(1))
        {
            //clears current landscape
            //clearMap(true);
        }

        if (Input.GetMouseButtonDown(2))
        {
            //clears current landscape
            SaveAssetMap();
            count++;
        }

       // colliders = Physics2D.OverlapCircleAll(spawnPos, spawnRadius);
    }

    public void SaveAssetMap()
    {
        string saveName = "tmapXY_" + count;
        var mf = GameObject.Find("Grid");

        if (mf)
        {
            var savePath = "Assets/" + saveName + ".prefab";
            if(PrefabUtility.CreatePrefab(savePath,mf))
            {
                EditorUtility.DisplayDialog("Tilemap saved", "Your Tilemap was saved under" + savePath, "Continue");
            }
            else
            {
                EditorUtility.DisplayDialog("Tilemap NOT saved", "An Error occured while trying to save Your Tilemap under" + savePath, "Continue");
            }
        }
    }

    public void clearMap(bool complete)
    {
        topMap.ClearAllTiles();
        topMapWater.ClearAllTiles();
        botMap.ClearAllTiles();

        if (complete)
        {
            terrainMap = null;
        }
    }
}

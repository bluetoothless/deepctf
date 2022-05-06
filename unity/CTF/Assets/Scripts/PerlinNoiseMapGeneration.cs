using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public enum biomType
{
    grass,
    accelerate,
    desert,
    lake
}

public class Biom
{
    public int counter = 0;
    public int chance;
    public GameObject prefab;
    public GameObject tiles;
    public biomType type;
    public Biom(int ch, GameObject pr, GameObject til, biomType t)
    {
        chance = ch;
        prefab = pr;
        tiles = til;
        type = t;
    }
}

public class Tile
{
    public int xCenter;
    public int yCenter;
    public biomType biom = biomType.grass;
    public int domain = -1;

    public Tile(int x, int y)
    {
        xCenter = x;
        yCenter = y;
    }

    public void changeBiom(biomType b)
    {
        biom = b;
    }
}

public class PerlinNoiseMapGeneration : MonoBehaviour
{
    public GameObject lakePrefab;
    public GameObject lakeTiles;
    public GameObject acceleratePrefab;
    public GameObject accelerateTiles;
    public GameObject desertPrefab;
    public GameObject desertTiles;
    public int lakesProcentage = 20;
    public int accelerateSurfaceProcentage = 10;
    public int desertsProcentage = 35;
    private int nextDomain = 0;
    private int height;
    private int width;
    private int TileScale;
    private int mapSize;
    private List<Tile> tiles;

    public const int chanceEnhacerAdder = 5;

    private void Awake()
    {
        GameObject field = gameObject;
        TileScale = (int)lakePrefab.transform.localScale.x;
        height = (int)field.transform.localScale.x * 10; // 360
        width = (int)field.transform.localScale.z * 10; // 400
        mapSize = (height * width) / (TileScale * TileScale);
    }


    // Start is called before the first frame update
    void Start()
    {


        Stopwatch sw = new Stopwatch();
        sw.Start();

        GenerateMap();
        FindDomains();

        sw.Stop();
        UnityEngine.Debug.Log("Seconds = " + sw.Elapsed);

        /*
        foreach (Biom biom in bioms)
        {
            scale = Random.Range(2f, 3f);
            offsetX = Random.Range(0f, 99999f);
            offsetY = Random.Range(0f, 99999f);

            int i = 0;  // height counter
            int j = 0;  // width counter

            for (int current_tile_height = (height / 2 - height) + (TileScale / 2); current_tile_height < height / 2; current_tile_height += TileScale)
            {
                for (int current_tile_width = (width / 2 - width) + (TileScale / 2); current_tile_width < width / 2; current_tile_width += TileScale)
                {
                    float sample = Mathf.PerlinNoise(
                        ((float)(current_tile_height + (height / 2)) / height) * scale + offsetX,
                        ((float)(current_tile_width + (width / 2)) / width) * scale + offsetY);

                    sample *= 100;

                    if (sample < biom.chance)
                    {
                        GameObject newTile = Instantiate(biom.prefab, new Vector3(current_tile_height, 0, current_tile_width), Quaternion.identity);
                        newTile.transform.SetParent(biom.tiles.transform);
                    }
                    j++;
                }
                i++;
            }
        }
        */


        /*
        for (int current_tile_height = (height/2 - height) + (TileScale/2); current_tile_height < height/2; current_tile_height += TileScale)
        {
            for (int current_tile_width = (width/2 - width) + (TileScale/2); current_tile_width < width/2; current_tile_width += TileScale)
            {
                float sample = Mathf.PerlinNoise(
                    ((float)(current_tile_height + (height / 2)) / height) * scale + offsetX,
                    ((float)(current_tile_width + (width / 2)) / width) * scale + offsetY);

                sample *= 100;

                if (sample < lakes)
                {
                    currentPrefab = lakePrefab;
                    currentTiles = lakeTiles;
                }
                else if (sample < lakes + accelerate)
                {
                    currentPrefab = acceleratePrefab;
                    currentTiles = accelerateTiles;
                }
                else if (sample < lakes + accelerate + desert)
                {
                    currentPrefab = desertPrefab;
                    currentTiles = desertTiles;
                }
                else
                {
                    continue;
                }

                GameObject newTile = Instantiate(currentPrefab, new Vector3(current_tile_height, 0, current_tile_width), Quaternion.identity);
                newTile.transform.SetParent(currentTiles.transform);
            }
        }
        */

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateMap()
    {
        // PERLIN NOISE MAP GENERATION

        tiles = new List<Tile> { };

        for (int current_tile_height = (height / 2 - height) + (TileScale / 2); current_tile_height < height / 2; current_tile_height += TileScale)
        {
            for (int current_tile_width = (width / 2 - width) + (TileScale / 2); current_tile_width < width / 2; current_tile_width += TileScale)
            {
                tiles.Add(new Tile(current_tile_height, current_tile_width));
            }
        }

        var tilesCopy = new List<Tile>(tiles);

        Biom lakes = new(lakesProcentage, lakePrefab, lakeTiles, biomType.lake);
        Biom accelerate = new(accelerateSurfaceProcentage, acceleratePrefab, accelerateTiles, biomType.accelerate);
        Biom desert = new(desertsProcentage, desertPrefab, desertTiles, biomType.desert);

        var bioms = new List<Biom> { accelerate, desert, lakes };

        float scale;
        float offsetX;
        float offsetY;
        scale = Random.Range(2f, 3f);
        foreach (Biom biom in bioms)
        {
            if (biom.chance == 0)
            {
                continue;
            }
            offsetX = Random.Range(0f, 99999f);
            offsetY = Random.Range(0f, 99999f);

            int chanceEnhancer = -(biom.chance/3);
            bool notEnoughTiles = true;

            while (notEnoughTiles)
            {
                // iterating in reverse, so I can remove a tile from list(tiles) in a run time
                for (int i = tilesCopy.Count - 1; i >= 0; i--)
                {
                    Tile tile = tilesCopy[i];

                    float sample = Mathf.PerlinNoise(
                        (float)tile.xCenter / height * scale + offsetX,
                        (float)tile.yCenter / width * scale + offsetY);

                    sample *= 100;

                    if (sample < biom.chance + chanceEnhancer)
                    {
                        // put newTile in game
                        tile.changeBiom(biom.type);
                        GameObject newTile = Instantiate(biom.prefab, new Vector3(tile.xCenter, 0, tile.yCenter), Quaternion.identity);
                        newTile.transform.SetParent(biom.tiles.transform);
                        // delete Tile from List, so it won't be consider in next loop

                        tilesCopy.Remove(tile);
                        // check if enough tiles already in game
                        biom.counter++;
                        if (biom.counter >= mapSize * biom.chance / 100)
                        {
                            notEnoughTiles = false;
                            break;
                        }
                    }
                }
                chanceEnhancer += chanceEnhacerAdder;
            }
        }
    }

    // using FloodFill algorithm
    void FindDomains()
    {
        bool somethingAdded = false;
        Queue<int> queue = new Queue<int>();

        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].domain == -1)
            {
                somethingAdded = true;
                addToDomain(queue, i);
            }

            while (queue.Count > 0)
            {
                int next = queue.Dequeue();

                if (tiles[next].domain == -1)
                {
                    addToDomain(queue, next);
                }
            }
            if (somethingAdded)
            {
                somethingAdded = false;
                nextDomain++;
            }        
        }
    }

    void addToDomain(Queue<int> queue, int index)
    {
        tiles[index].domain = nextDomain;
        addNeighborsToQueue(queue, index);
    }

    void addToQueueIfMatches(Queue<int> queue, int index, int indexChange)
    {
        if ((tiles[index].biom == biomType.lake && tiles[index + indexChange].biom == biomType.lake)
            || (tiles[index].biom != biomType.lake && tiles[index + indexChange].biom != biomType.lake)
            && (tiles[index + indexChange].domain == -1))
        {
            queue.Enqueue(index + indexChange);
        }
    }

    void addNeighborsToQueue(Queue<int> queue, int startingIndex)
    {
        // zast¹piæ wyliczonymi sta³ymi

        // for far left tiles
        if ((startingIndex - 99) % 100 == 0)
        {
            addToQueueIfMatches(queue, startingIndex, -1);
        }
        // for far right tiles
        else if (startingIndex % 100 == 0) 
        {
            addToQueueIfMatches(queue, startingIndex, 1);
        }
        else
        {
            addToQueueIfMatches(queue, startingIndex, -1);
            addToQueueIfMatches(queue, startingIndex, 1);
        }

        // for far up tiles
        if (startingIndex >= 8900)
        {
            addToQueueIfMatches(queue, startingIndex, -100);
        }
        // for far down tiles
        else if (startingIndex <= 99) 
        {
            addToQueueIfMatches(queue, startingIndex, 100);
        }
        else
        {
            addToQueueIfMatches(queue, startingIndex, -100);
            addToQueueIfMatches(queue, startingIndex, 100);
        }
    }



    //      THIS IS RECURSVE SOLUTION FOR FLOOD FILL ALGORITHM
    //      IT RESOLVED IN STACK OVERFLOW, SO IT NEEDED TO BE CHANGED TO ITERATIVE SOLUTON

/*
// using FloodFill algorithm
void FindDomains()
{
    for (int i = 0; i < tiles.Count; i++)
    {
        if (tiles[i].domain == -1)
        {
            addToDomain(i);
            nextDomain++;
        }
    }
}

void addToDomain(int index)
{
    tiles[index].domain = nextDomain;
    addNeighborsToDomain(index);
}

void addToDomainIfMatches(int index, int indexChange)
{
    if ((tiles[index].biom == biomType.lake && tiles[index + indexChange].biom == biomType.lake)
        || (tiles[index].biom != biomType.lake && tiles[index + indexChange].biom != biomType.lake)
        && (tiles[index + indexChange].domain == -1))
    {
        addToDomain(index + indexChange);
    }
}

void addNeighborsToDomain(int startingIndex)
{
    // zast¹piæ wyliczonymi sta³ymi

    // for far left tiles
    if ((startingIndex - 99) % 100 == 0)
    {
        addToDomainIfMatches(startingIndex, -1);
    }
    // for far right tiles
    else if (startingIndex % 100 == 0) 
    {
        addToDomainIfMatches(startingIndex, 1);
    }
    else
    {
        addToDomainIfMatches(startingIndex, -1);
        addToDomainIfMatches(startingIndex, 1);
    }

    // for far up tiles
    if (startingIndex >= 8900)
    {
        addToDomainIfMatches(startingIndex, -100);
    }
    // for far down tiles
    else if (startingIndex <= 99) 
    {
        addToDomainIfMatches(startingIndex, 100);
    }
    else
    {
        addToDomainIfMatches(startingIndex, -100);
        addToDomainIfMatches(startingIndex, 100);
    }
*/
}

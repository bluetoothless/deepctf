using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Biom
{
    public int counter = 0;
    public int chance;
    public GameObject prefab;
    public GameObject tiles;
    public Biom(int ch, GameObject pr, GameObject til)
    {
        chance = ch;
        prefab = pr;
        tiles = til;
    }
}

public class Tile
{
    public int xCenter;
    public int yCenter;

    public Tile(int x, int y)
    {
        xCenter = x;
        yCenter = y;
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

    public const int chanceEnhacerAdder = 5;

    // Start is called before the first frame update
    void Start()
    {
        Stopwatch sw = new Stopwatch();

        sw.Start();


        GameObject field = gameObject;
        int TileScale = (int)lakePrefab.transform.localScale.x;

        //field.transform.position = Vector3.zero;

        // PERLIN NOISE MAP GENERATION

        int height = (int)field.transform.localScale.x * 10; // 360
        int width = (int)field.transform.localScale.z * 10; // 400

        var tiles = new List<Tile> {};

        for (int current_tile_height = (height / 2 - height) + (TileScale / 2); current_tile_height < height / 2; current_tile_height += TileScale)
        {
            for (int current_tile_width = (width / 2 - width) + (TileScale / 2); current_tile_width < width / 2; current_tile_width += TileScale)
            {
                tiles.Add(new Tile(current_tile_height, current_tile_width));
            }
        }

        // hardcoded map %
        Biom lakes = new(30, lakePrefab, lakeTiles);
        Biom accelerate = new(30, acceleratePrefab, accelerateTiles);
        Biom desert = new(30, desertPrefab, desertTiles);

        var bioms = new List<Biom> { accelerate, desert, lakes };
        // bool[,] mapMatrix = new bool[height, width];
        int mapSize = (height * width) / (TileScale * TileScale) ;

        float scale;
        float offsetX;
        float offsetY;
        scale = Random.Range(2f, 3f);
        foreach (Biom biom in bioms)
        {
            offsetX = Random.Range(0f, 99999f);
            offsetY = Random.Range(0f, 99999f);

            int chanceEnhancer = 0;
            bool notEnoughTiles = true;

            while (notEnoughTiles)
            {
                // iterating in reverse, so I can remove a tile from list(tiles) in a run time
                for (int i = tiles.Count - 1; i >= 0; i--)
                {
                    Tile tile = tiles[i];

                    float sample = Mathf.PerlinNoise(
                        (float)tile.xCenter / height * scale + offsetX,
                        (float)tile.yCenter / width * scale + offsetY);

                    sample *= 100;

                    if (sample < biom.chance + chanceEnhancer)
                    {
                        // put newTile in game
                        GameObject newTile = Instantiate(biom.prefab, new Vector3(tile.xCenter, 0, tile.yCenter), Quaternion.identity);
                        newTile.transform.SetParent(biom.tiles.transform);
                        // delete Tile from List, so it won't be consider in next loop
                        tiles.Remove(tile);
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

        sw.Stop();

        UnityEngine.Debug.Log("Seconds = " + sw.Elapsed + "Scale = ");


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
}

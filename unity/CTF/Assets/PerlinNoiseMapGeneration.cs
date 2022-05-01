using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Biom
{
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

public class PerlinNoiseMapGeneration : MonoBehaviour
{
    public GameObject lakePrefab;
    public GameObject lakeTiles;
    public GameObject acceleratePrefab;
    public GameObject accelerateTiles;
    public GameObject desertPrefab;
    public GameObject desertTiles;

    // Start is called before the first frame update
    void Start()
    {
        Stopwatch sw = new Stopwatch();

        sw.Start();


        GameObject field = gameObject;
        int TileScale = 4;

        //field.transform.position = Vector3.zero;

        // PERLIN NOISE MAP GENERATION

        int width = (int)field.transform.localScale.z * 10; // 400
        int height = (int)field.transform.localScale.x * 10; // 360

        // hardcoded map %
        Biom lakes = new(70, lakePrefab, lakeTiles);
        Biom accelerate = new(70, acceleratePrefab, accelerateTiles);
        Biom desert = new(70, desertPrefab, desertTiles);

        var bioms = new List<Biom> { accelerate, desert, lakes };
        bool[,] mapMatrix = new bool[360, 400];

        float scale;
        float offsetX;
        float offsetY;

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

        sw.Stop();

        UnityEngine.Debug.Log("Elapsed= " + sw.ElapsedTicks + "/// Seconds = " + sw.Elapsed);

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

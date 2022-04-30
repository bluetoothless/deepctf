using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameObject field = gameObject;
        int TileScale = 4;

        //field.transform.position = Vector3.zero;

        // PERLIN NOISE MAP GENERATION

        int width = (int)field.transform.localScale.z * 10; // 400
        int height = (int)field.transform.localScale.x * 10; // 360

        // hardcoded map %
        int lakes = 30;
        int accelerate = 10;
        int desert = 10;

        GameObject currentPrefab;
        GameObject currentTiles;

        float scale = 3f;


        for (int current_tile_height = (height/2 - height) + (TileScale/2); current_tile_height < height/2; current_tile_height += TileScale)
        {
            for (int current_tile_width = (width/2 - width) + (TileScale/2); current_tile_width < width/2; current_tile_width += TileScale)
            {
                float sample = Mathf.PerlinNoise(
                    ((float)(current_tile_height + (height / 2)) / height) * scale,
                    ((float)(current_tile_width + (width / 2)) / width) * scale);

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


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

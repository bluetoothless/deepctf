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
    public int tilesMapListIndex;
    public biomType biom = biomType.grass;
    public int domain = -1;

    public Tile(int x, int y, int i)
    {
        xCenter = x;
        yCenter = y;
        tilesMapListIndex = i;
    }

    public void changeBiom(biomType b)
    {
        biom = b;
    }
}

public class Domain : IEnumerable<Tile>
{
    public float sizeInRelationToMapBesidesLakes;
    public int domain = -1;
    public List<Tile> tilesList = new List<Tile>();

    public Domain(int domainNumber)
    {
        domain = domainNumber;
    }

    public void Add(Tile t)
    {
        tilesList.Add(t);
    }
   
    public int Count
    {
        get { return tilesList.Count; }
    }

    public float Chance
    {
        get { return sizeInRelationToMapBesidesLakes * 100; }
    }

    // indeksator
    public Tile this[int i]
    {
        get { return tilesList[i]; }
        set { tilesList[i] = value; }
    }

    // enumerators
    // For IEnumerable<Tile>
    public IEnumerator<Tile> GetEnumerator()
    {
        return tilesList.GetEnumerator(); 
    }

    // For IEnumerable
    IEnumerator IEnumerable.GetEnumerator() 
    {
        return GetEnumerator();
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
    public GameObject blueBase;
    public GameObject redBase;
    public GameObject bases;

    public int lakesProcentage = 20;
    public int accelerateSurfaceProcentage = 10;
    public int desertsProcentage = 35;
    private int nextDomain = 0;
    private int height;
    private int width;
    private int TileScale;
    private int mapSize;
    private List<Tile> tiles = new List<Tile>();
    private List<Domain> domains = new List<Domain>();

    public const int chanceEnhacerAdder = 5;

    private void Awake()
    {
        GameObject field = gameObject;
        TileScale = (int)lakePrefab.transform.localScale.x;
        height = (int)field.transform.localScale.x * 10; // 360
        width = (int)field.transform.localScale.z * 10; // 400
        mapSize = (height * width) / (TileScale * TileScale);
        //lakesProcentage = PlayerPrefs.GetInt("lakesPercent");
        //accelerateSurfaceProcentage = PlayerPrefs.GetInt("accSurfacesPercent");
        //desertsProcentage = PlayerPrefs.GetInt("desertsPercent");
    }


    // Start is called before the first frame update
    void Start()
    {


        Stopwatch sw = new Stopwatch();
        sw.Start();

        GenerateMap();
        FindDomains();
        PlaceBases();

        sw.Stop();
        UnityEngine.Debug.Log("Seconds = " + sw.Elapsed);

        // DEBUG__PrintTiles();

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

        for (int current_tile_height = (height / 2 - height) + (TileScale / 2), i = 0; current_tile_height < height / 2; current_tile_height += TileScale)
        {
            for (int current_tile_width = (width / 2 - width) + (TileScale / 2); current_tile_width < width / 2; current_tile_width += TileScale, i++)
            {
                tiles.Add(new Tile(current_tile_height, current_tile_width, i));
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
        Domain domain = new Domain(-1);        // never used object, just because below in for loop there is a need to make it
        Queue<int> queue = new Queue<int>();

        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].domain == -1)
            {
                somethingAdded = true;
                domain = new Domain(nextDomain);
                addToDomain(queue, i, domain);
            }

            while (queue.Count > 0)
            {
                int next = queue.Dequeue();

                if (tiles[next].domain == -1)
                {
                    addToDomain(queue, next, domain);
                }
            }
            if (somethingAdded)
            {
                somethingAdded = false;
                domains.Add(domain);       // add list of tiles that have the same domain, to domains list
                nextDomain++;
            }        
        }
    }

    void addToDomain(Queue<int> queue, int index, Domain domain)
    {
        tiles[index].domain = nextDomain;
        domain.Add(tiles[index]);
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

    void PlaceBases()
    {
        List<Domain> noLakeDomains = new List<Domain>();
        int noLakeTilesCounter = 0;
        // CHOOSE WHAT DOMAIN TO PLACE BASES INTO
        foreach (Domain domain in domains)
        {
            if (domain.tilesList[0].biom != biomType.lake)
            {
                noLakeDomains.Add(domain);
                noLakeTilesCounter += domain.Count;
            }
        }

        chooseDomain:                       // GOTO

        // If no domains, generate new map
        if (noLakeDomains.Count == 0)
        {
            Start();
            return;
        }

        foreach (Domain domain in noLakeDomains)
        {
            domain.sizeInRelationToMapBesidesLakes = (float)domain.Count / noLakeTilesCounter;
        }
        Domain domainNow = null;
        int choice = Random.Range(0, 100);
        float chanceSum = 0f;

        foreach (Domain domain in noLakeDomains)
        {
            chanceSum += domain.Chance;
            if (chanceSum >= choice)
            {
                domainNow = domain;
                break;
            }
        }
        // to make sure there are no problems with false float rounding
        if (domainNow == null)
        {
            domainNow = noLakeDomains[noLakeDomains.Count - 1];
        }

        // Check for 3x3 possible spaces in selected domain
        // Later, boundry tiles can be excluded from this search        -- PERFORMANCE THING
        List<Tile> possibleBaseLocations = new List<Tile>();

        foreach (Tile t in domainNow)
        {
            // zast¹piæ wyliczonymi sta³ymi
            int i = t.tilesMapListIndex;
            
            if (((i - 99) % 100 == 0) || // for far left tiles
                    (i % 100 == 0) ||   // for far right tiles
                    (i >= 8900) ||      // for far up tiles 
                    (i <= 99))          // for far down tiles
            {
                continue;
            }
            else
            {
                // check if 3x3 has the same domain
                if ((tiles[i].domain == tiles[i + 1].domain) &&         // left
                        (tiles[i].domain == tiles[i - 1].domain) &&     // right
                        (tiles[i].domain == tiles[i + 100].domain) &&   // up
                        (tiles[i].domain == tiles[i - 100].domain) &&   // down
                        (tiles[i].domain == tiles[i - 101].domain) &&   // down right
                        (tiles[i].domain == tiles[i - 99].domain) &&    // down left
                        (tiles[i].domain == tiles[i + 101].domain) &&   // up right
                        (tiles[i].domain == tiles[i + 99].domain))      // up left
                {
                    possibleBaseLocations.Add(t);
                }
            }
        }

        if (possibleBaseLocations.Count < 2)
        {
            noLakeDomains.Remove(domainNow);
            noLakeTilesCounter -= domainNow.Count;
            goto chooseDomain;
        }

        // random 3x3 space ??
        // FOR NOW JUST 1x1 RANDOM TILE
        // MAYBE PLACE FOR BETTER ALGORITHM

        int choice1 = Random.Range(0, possibleBaseLocations.Count);
        int choice2 = Random.Range(0, possibleBaseLocations.Count);
        while ((choice1 == choice2) ||
                (Vector3.Distance(
                    new Vector3(possibleBaseLocations[choice1].xCenter, 0, possibleBaseLocations[choice1].yCenter),
                    new Vector3(possibleBaseLocations[choice2].xCenter, 0, possibleBaseLocations[choice2].yCenter)) < 4))       // minimum 4 distance between bases
        {
            choice2 = Random.Range(0, domainNow.Count);
        }

        // place them
        GameObject newBlueBase = Instantiate(blueBase, new Vector3(possibleBaseLocations[choice1].xCenter, 0, possibleBaseLocations[choice1].yCenter), Quaternion.identity);
        newBlueBase.GetComponent<BlueBaseScript>().CenterTile = possibleBaseLocations[choice1];
        newBlueBase.transform.SetParent(bases.transform);
        GameObject newRedBase = Instantiate(redBase, new Vector3(possibleBaseLocations[choice2].xCenter, 0, possibleBaseLocations[choice2].yCenter), Quaternion.identity);
        newRedBase.GetComponent<RedBaseScript>().CenterTile = possibleBaseLocations[choice2];
        newRedBase.transform.SetParent(bases.transform);
    }

    public List<Tile> GetTilesList()
    {
        return tiles;
    }

    void DEBUG__PrintTiles()
    {
        foreach (Tile t in tiles)
        {
            UnityEngine.Debug.Log("X:: "+t.xCenter+" Y:: "+t.yCenter+" DOMAIN::: "+t.domain);
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

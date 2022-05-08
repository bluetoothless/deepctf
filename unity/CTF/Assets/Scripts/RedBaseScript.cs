using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBaseScript : MonoBehaviour
{
    public GameObject RedAgentPrefab;
    public int NumberOfRedAgents = 10;
    public Tile CenterTile;
    private GameObject field;
    private GameObject redAgents;
    private List<Tile> tiles;

    // Start is called before the first frame update
    void Start()
    {
        field = GameObject.Find("Field (10 x scale)");
        redAgents = GameObject.Find("RedAgents");
        tiles = field.GetComponent<PerlinNoiseMapGeneration>().GetTilesList();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnGameStart()
    {
        // for every Agent
        for (int i = 0; i < NumberOfRedAgents; i++)
        {
            if (CheckIfCanSpawnAt(CenterTile.tilesMapListIndex + 1))             // right
            {
                SpawnAgentAt(CenterTile.tilesMapListIndex + 1);
            }
            else if (CheckIfCanSpawnAt(CenterTile.tilesMapListIndex - 1))       //left
            {
                SpawnAgentAt(CenterTile.tilesMapListIndex - 1);
            }
            else if (CheckIfCanSpawnAt(CenterTile.tilesMapListIndex + 100))     //up
            {
                SpawnAgentAt(CenterTile.tilesMapListIndex + 100);
            }
            else if (CheckIfCanSpawnAt(CenterTile.tilesMapListIndex - 100))     //down
            {
                SpawnAgentAt(CenterTile.tilesMapListIndex - 100);
            }
            else
            {
                new WaitForSeconds(1);                                          // wait 1 second
            }
        }
    }

    void SpawnAgentAt(int index)
    {
        GameObject newRedAgent = Instantiate(RedAgentPrefab, new Vector3(tiles[index].xCenter, 0, tiles[index].yCenter), Quaternion.identity);
        newRedAgent.transform.SetParent(redAgents.transform);
    }

    bool CheckIfCanSpawnAt(int index)
    {
        foreach (GameObject redAgent in redAgents.transform)
        {
            if (Vector3.Distance(redAgent.transform.position, new Vector3(tiles[index].xCenter, 0, tiles[index].yCenter)) < 1f)
            {
                return false;
            }
        }
        return true;
    }
}

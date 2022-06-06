using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBaseScript : MonoBehaviour
{
    public GameObject BlueAgentPrefab;
    public int NumberOfBlueAgents = 10;
    public Tile CenterTile;
    private GameObject blueAgents;
    private List<Tile> tiles;

    // Start is called before the first frame update
    void Start()
    {
        blueAgents = GameObject.Find("BlueAgents");
        tiles = PerlinNoiseMapGeneration.GetTilesList();
    }

    public void OnGameStart()
    {
        // for every Agent
        for (int i = 0; i < NumberOfBlueAgents; i++)
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
        GameObject newBlueAgent = Instantiate(BlueAgentPrefab, new Vector3(tiles[index].xCenter, 0, tiles[index].yCenter), Quaternion.identity);
        newBlueAgent.transform.SetParent(blueAgents.transform);
    }

    bool CheckIfCanSpawnAt(int index)
    {
        foreach (Transform blueAgent in blueAgents.transform)
        {
            if (Vector3.Distance(blueAgent.transform.position, new Vector3(tiles[index].xCenter, 0, tiles[index].yCenter)) < 1f)
            {
                return false;
            }
        }
        return true;
    }
}

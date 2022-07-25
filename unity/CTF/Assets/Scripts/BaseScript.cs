using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{

    public GameObject AgentPrefab;
    public int NumberOfAgents = 10;
    public Tile CenterTile;
    private GameObject Agents;
    private List<Tile> tiles;
    private bool isRed;

    // Start is called before the first frame update
    void Start()
    {
        Agents = GameObject.find(isRed ? "RedAgents" : "BlueAgents");
        tiles = PerlinNoiseMapGeneration.GetTilesList();
    }

    public void OnGameStart()
    {
        // for every Agent
        for (int i = 0; i < NumberOfAgents; i++)
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

    public virtual void SpawnAgentAt(int index);

    bool CheckIfCanSpawnAt(int index)
    {
        foreach (Transform agent in Agents.transform)
        {
            if (Vector3.Distance(agent.transform.position, new Vector3(tiles[index].xCenter, 0, tiles[index].yCenter)) < 1f)
            {
                return false;
            }
        }
        return true;
    }

}

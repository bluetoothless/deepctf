using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.Barracuda;
using System.IO;
using UnityEditor;
using System;
using Unity.Barracuda.ONNX;

public abstract class BaseBaseScript : MonoBehaviour
{
    public GameObject AgentPrefab;
    public Tile CenterTile;
    private GameObject Agents;
    private List<Tile> tiles;
    private bool isRed;
    private float agentSpawnHeight = 2.5f;
    public SimpleMultiAgentGroup m_AgentGroup;

    [HideInInspector]
    public bool isBaseSet;

    void Awake()
    {
        isBaseSet = false;
        m_AgentGroup = new SimpleMultiAgentGroup();
    }


    public void OnGameStart()
    {
        Agents = GameObject.Find(isRed ? "RedAgents" : "BlueAgents");
        tiles = PerlinNoiseMapGeneration.GetTilesList();

        Debug.Log("OGS: spawning egants");
        // for every Agent
        for (int i = 0; i < GameManager.EnvContr.NumberOfAgents; i++)
        {
            Debug.Log("OGS: trying spawn agent no "+i);
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
                Debug.Log("OGS: can't spawn");
                //new WaitForSeconds(1);                                          // wait 1 second
            }
        }
        isBaseSet = true;

    }

    public void SpawnAgentAt(int index)
    {
        GameObject agent = Instantiate(AgentPrefab, new Vector3(tiles[index].xCenter, agentSpawnHeight, tiles[index].yCenter), Quaternion.identity);
        agent.transform.SetParent(Agents.transform);
        if (GameManager.IsSpectatorMode)
        {
            SetNeuralNetworkModelForAgent(agent);
        }
        if (isRed)
        {
            GameManager.AddRedAgent(agent);
        }
        else
        {
            GameManager.AddBlueAgent(agent);
        }
        m_AgentGroup.RegisterAgent(agent.GetComponent<AgentMovementWSAD>());
    }

    public bool CheckIfCanSpawnAt(int index)
    {
        Debug.Log("CICS: "+index);
        foreach (Transform agent in Agents.transform)
        {
            Debug.Log("CICS: " + index + " " + agent + " at " + agent.transform.position);
            if (agent.gameObject.activeSelf && Vector3.Distance(agent.transform.position, new Vector3(tiles[index].xCenter, agentSpawnHeight, tiles[index].yCenter)) < 1f)
            {
                return false;
            }
        }
        return true;
    }

    public void setIsRed(bool isRed=true)
    {
        this.isRed = isRed;
    }

    private void SetNeuralNetworkModelForAgent(GameObject agent)
    {
        var neuralNetworkPath = PlayerPrefs.GetString("neuralNetworkPath");

        var asset = ScriptableObject.CreateInstance<NNModel>();
        asset.modelData = ScriptableObject.CreateInstance<NNModelData>();
        var onnxModelConverter = new ONNXModelConverter(true);
        var model = onnxModelConverter.Convert(neuralNetworkPath);

        using (var memoryStream = new MemoryStream())
        using (var writer = new BinaryWriter(memoryStream))
        {
            ModelWriter.Save(writer, model);
            asset.modelData.Value = memoryStream.ToArray();
        }

        asset.name = GameManager.EnvContr.behaviorName;
        var nnModel = asset;
        agent.GetComponent<AgentMovementWSAD>().SetModel(GameManager.EnvContr.behaviorName, nnModel);
    }
}

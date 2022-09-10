using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static List<GameObject> RedAgents = new List<GameObject> { };
    public static List<GameObject> BlueAgents = new List<GameObject> { };
    public static SimpleMultiAgentGroup redAgentGroup;
    public static SimpleMultiAgentGroup blueAgentGroup;

    public static void AddRedAgent(GameObject agent)
    {
        RedAgents.Add(agent);
    }

    public static void AddBlueAgent(GameObject agent)
    {
        BlueAgents.Add(agent);
    }

    public static void RemoveRedAgent(GameObject agent)
    {
        RedAgents.Remove(agent);
        GameObject.Destroy(agent);
    }

    public static void RemoveBlueAgent(GameObject agent)
    {
        BlueAgents.Remove(agent);
        GameObject.Destroy(agent);
    }

    public static bool IsAnyRed()
    {
        return RedAgents.Count > 0;
    }

    public static bool IsAnyBlue()
    {
        return BlueAgents.Count > 0;
    }

    public static bool isAny()
    {
        return IsAnyRed() || IsAnyBlue();
    }

    public static void AddRewardTeam(float reward, string color)
    {
        if (color == "blue") 
        { 
            blueAgentGroup.AddGroupReward(reward);
        }
        else
        {
            redAgentGroup.AddGroupReward(reward);
        }
    }

    public static void Kill(GameObject agent)
    {
        RewardValuesScript.getRewardValues();
        agent.GetComponent<AgentMovementWSAD>().AddRewardAgent(RewardValuesScript.rewards["agentDead"]);
        agent.SetActive(false);
        string color = agent.GetComponent<AgentComponentsScript>().color;

        if (color == "red")
        {
            RemoveRedAgent(agent);
        }
        else
        {
            RemoveBlueAgent(agent);
        }
        CheckIfLost(color);
    }

    private static void CheckIfLost(string color)
    {
        bool isRed = color == "red";
        bool aliveAgents = isRed ? GameManager.IsAnyRed() : GameManager.IsAnyBlue();
       
        if (!aliveAgents) // if all agents from team died
        {

            RewardValuesScript.getRewardValues();
            if (!isRed)
            {
                Debug.Log("Team red wins!");
                AddRewardTeam(RewardValuesScript.rewards["gameLost"], "blue");
                AddRewardTeam(RewardValuesScript.rewards["gameWon"], "red");
                EndEpisode();

            }
            else
            {
                Debug.Log("Team blue wins!");
                AddRewardTeam(RewardValuesScript.rewards["gameLost"], "red");
                AddRewardTeam(RewardValuesScript.rewards["gameWon"], "blue");
                EndEpisode();
            }
        }
    }

    public static void EndEpisode()
    {
        List<GameObject> tmp = new List<GameObject> { };
        foreach (GameObject agent in RedAgents)
        {
            tmp.Add(agent);   
        }
        RedAgents.Clear();

        foreach (GameObject agent in BlueAgents)
        {
            tmp.Add(agent);
        }
        BlueAgents.Clear();
        foreach (GameObject agent in tmp)
        {
            GameObject.Destroy(agent);
        }
        blueAgentGroup.EndGroupEpisode();
        redAgentGroup.EndGroupEpisode();
        //Odtad nowa mapa i start gry
        Scene sceneMain = SceneManager.GetActiveScene();
        GameObject interfaceCamera = sceneMain.GetRootGameObjects()[7].gameObject;
        StartGameScript startGameScript = interfaceCamera.GetComponentInChildren<StartGameScript>();
        startGameScript.StartGame();
    }

    /*
    public static void AddRewardTeam(float reward, string color)
    { 
        if (color == "blue")
        {
            foreach (GameObject agent in BlueAgents)
            {
                agent.GetComponent<AgentMovementWSAD>().AddRewardAgent(reward);
            }
        }
        else
        {
            foreach (GameObject agent in RedAgents)
            {
                agent.GetComponent<AgentMovementWSAD>().AddRewardAgent(reward);
            }
        }
    }
    */
}

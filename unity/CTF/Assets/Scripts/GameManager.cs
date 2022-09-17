using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;


public static class GameManager
{
    public static List<GameObject> RedAgents = new List<GameObject> { };
    public static List<GameObject> BlueAgents = new List<GameObject> { };
    public static SimpleMultiAgentGroup redAgentGroup;
    public static SimpleMultiAgentGroup blueAgentGroup;
    public static BaseBaseScript blueBaseScript;
    public static BaseBaseScript redBaseScript;

    public static bool IsSpectatorMode = false;

    public static EnvController EnvContr;

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;


public class AgentsList
{

    private List<Agent> RedAgents;
    private List<Agent> BlueAgents;

    public void AddRedAgent(Agent agent)
    {
        RedAgents.Add(agent);
    }

    public void AddBlueAgent(Agent agent)
    {
        BlueAgents.Add(agent);
    }

    public void RemoveRedAgent(Agent agent)
    {
        RedAgents.Remove(agent);
    }

    public void RemoveBlueAgent(Agent agent)
    {
        BlueAgents.Remove(agent);
    }

    bool IsAnyRed()
    {
        return RedAgents.Count > 0;
    }

    bool IsAnyBlue()
    {
        return BlueAgents.Count > 0;
    }

    bool isAny()
    {
        return IsAnyRed() || IsAnyBlue();
    }

}

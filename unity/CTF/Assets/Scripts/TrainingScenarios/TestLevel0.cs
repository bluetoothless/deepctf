using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevel0 : TestLevel
{
    public override void Run(Transform t)
    {
        return;
    }

    
    public override void Spawn(List<GameObject> team)
    {
        foreach (var agent in team)
        {
            agent.transform.position = new Vector3(agent.transform.position.x, 20, agent.transform.position.z);
        }
    }

}
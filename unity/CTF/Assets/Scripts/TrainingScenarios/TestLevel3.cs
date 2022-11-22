using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevel3 : TestLevel
{
    public override (bool, bool, bool, bool) Run()
    {
        bool W = true;
        bool S = false;
        bool A = Random.Range(0,100) > 33;
        bool D = false;
        return (W, S, A, D);
    }

    public override void Spawn()
    {
        foreach (var agent in GameManager.BlueAgents)
        {
            agent.transform.position = new Vector3(agent.transform.position.x, 20, agent.transform.position.z);
        }

        var blueBaseScript = GameObject.Find("Blue Base(Clone)").GetComponent<BlueBaseScript>();

        for (int i = 0; i < Random.Range(4, 10); i++)
        {
            int index;
            do
            {
                index = Random.Range(0,8899);
                if (blueBaseScript.CheckIfCanSpawnAt(index))
                    break;
            } while (true);

            blueBaseScript.SpawnAgentAt(index);
        }
    }
}

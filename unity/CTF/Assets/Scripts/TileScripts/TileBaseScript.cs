using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBaseScript : MonoBehaviour
{
    private float speedChangeModifier;
    private string logMessage;

    public void SetSpeedChangeModifier(float speedChangeModifier)
    {
        this.speedChangeModifier = speedChangeModifier;
    }

    public void SetLogMessage(string logMessage)
    {
        this.logMessage = logMessage;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "AgentBiomCollider")
        {
            return;
        }

        var agentBiomCollider = other.gameObject;
        Debug.Log(logMessage);
        var agent = agentBiomCollider.transform.parent.gameObject;
        agent.GetComponent<AgentMovementWSAD>().ChangeSpeedModifier(speedChangeModifier);
    }

}

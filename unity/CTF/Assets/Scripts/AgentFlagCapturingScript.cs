using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentFlagCapturingScript : MonoBehaviour
{
    private GameObject OwnBase;
    private void OnTriggerEnter(Collider collidingObject)
    {
        if (collidingObject.tag == "Agent")
        {
            GameObject agentFlag = gameObject.GetComponent<AgentComponentsScript>().AgentFlag;
            if (agentFlag.activeSelf)
            {
                string agentColor = gameObject.GetComponent<AgentComponentsScript>().color;
                string collidingAgentColor = collidingObject.GetComponent<AgentComponentsScript>().color;
                if (agentColor != collidingAgentColor)
                {
                    var agent = gameObject.gameObject.transform.parent.gameObject;
                    var collidingAgent = collidingObject.gameObject.transform.parent.gameObject;
                    var rewardValues = agent.GetComponent<RewardValuesScript>();
                    rewardValues.getRewardValues();
                    agent.GetComponent<AgentMovementWSAD>().AddRewardAgent(rewardValues.rewards["flagStolenFromAgent"]);
                    collidingAgent.GetComponent<AgentMovementWSAD>().AddRewardAgent(rewardValues.rewards["flagRetrievedFromAgent"]);

                    OwnBase = agentColor == "blue" ? GameObject.Find("Red Base(Clone)") : GameObject.Find("Blue Base(Clone)");
                    OwnBase.GetComponent<ReturnFlagScript>().returnFlagFromAgent(agentFlag, agent);
                }
            }
        }
    }

}

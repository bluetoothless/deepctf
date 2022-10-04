using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentCollisions : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        string agentColor = gameObject.GetComponent<AgentComponentsScript>().color;
        if (other.tag == "Agent")
        {
            RewardValuesScript.getRewardValues();
            string collidingAgentColor = other.GetComponent<AgentComponentsScript>().color;
            if (agentColor == collidingAgentColor)
            {
                gameObject.transform.parent.gameObject.GetComponent<AgentMovementWSAD>().AddRewardAgent(RewardValuesScript.rewards["agentTouchesAgentSameColor"]);
                GameManager.AddRewardTeam(RewardValuesScript.rewards["agentTouchesAgentSameColor_team"], agentColor);
            }
        }
        else if (other.tag == "Wall")
        {
            RewardValuesScript.getRewardValues();
            gameObject.transform.parent.gameObject.GetComponent<AgentMovementWSAD>().AddRewardAgent(RewardValuesScript.rewards["agentTouchesWall"]);
            GameManager.AddRewardTeam(RewardValuesScript.rewards["agentTouchesWall_team"], agentColor);
        }
    }
}

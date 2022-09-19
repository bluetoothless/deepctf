using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCapturingScript : MonoBehaviour
{
    [SerializeField] string baseColor;
    [SerializeField] GameObject FlagInBase;
    [SerializeField] GameObject EnemyFlagInBase;
    private GameObject OtherBase;

    private void passTheFlag(GameObject object1, GameObject object2) {
        bool isActive = object1.activeSelf;
        object1.SetActive(!isActive);
        isActive = object2.activeSelf;
        object2.SetActive(!isActive);
    }
    private void win (string color, GameObject object1, GameObject object2, GameObject collidingAgent)
    {
        bool isActive = object1.activeSelf;
        object1.SetActive(!isActive);
        isActive = object2.activeSelf;
        object2.SetActive(!isActive);
        // team wins
        Debug.Log("Team " + color + " wins!");
        if (color == "blue")
        {
            GameManager.AddRewardTeam(RewardValuesScript.rewards["gameLost"], "red");
            GameManager.AddRewardTeam(RewardValuesScript.rewards["gameWon"], "blue");
            GameManager.EnvContr.textBlueWin.SetActive(true);
            GameManager.EnvContr.EndEpisode();
        }
        else
        {
            GameManager.AddRewardTeam(RewardValuesScript.rewards["gameLost"], "blue");
            GameManager.AddRewardTeam(RewardValuesScript.rewards["gameWon"], "red");
            GameManager.EnvContr.textRedWin.SetActive(true);
            GameManager.EnvContr.EndEpisode();
        }
    }

    private void OnTriggerEnter(Collider collidingObject)
    {
        if (collidingObject.tag == "Agent")
        {
            if (baseColor == "blue")
                OtherBase = GameObject.Find("Red Base(Clone)");
            else
                OtherBase = GameObject.Find("Blue Base(Clone)");

            var collidingAgent = collidingObject.gameObject.transform.parent.gameObject;

            GameObject AgentFlag = collidingAgent.GetComponent<AgentComponentsScript>().AgentFlag;
            bool agentColorEqualsBaseColor = collidingAgent.GetComponent<AgentComponentsScript>().color == baseColor;
            bool agentHoldsEnemyFlag = AgentFlag.activeSelf;

            RewardValuesScript.getRewardValues();

            if (agentColorEqualsBaseColor)
            {
                if (!agentHoldsEnemyFlag) 
                    return; 

                if (FlagInBase.activeSelf) // if the agent touches own base, holds enemy's flag, and own flag is in own base
                {
                    collidingAgent.GetComponent<AgentMovementWSAD>().AddRewardAgent(RewardValuesScript.rewards["flagDelivered"]);
                    win(collidingAgent.GetComponent<AgentComponentsScript>().color, AgentFlag, FlagInBase, collidingAgent);
                }
                else                       // if the agent touches own base, holds enemy's flag, and own flag is not in own base
                {
                    collidingAgent.GetComponent<AgentMovementWSAD>().AddRewardAgent(RewardValuesScript.rewards["flagDelivered"]);
                    passTheFlag(EnemyFlagInBase, AgentFlag);
                }
            }
            else
            {
                if (!AgentFlag.activeSelf && FlagInBase.activeSelf) // if the agent touches enemy's base, does not hold enemy's flag, and enemy's flag is in enemy's base
                {
                    collidingAgent.GetComponent<AgentMovementWSAD>().AddRewardAgent(RewardValuesScript.rewards["flagCaptured"]);
                    passTheFlag(FlagInBase, AgentFlag);
                }
                else if (EnemyFlagInBase.activeSelf)                // if the agent touches enemy's base, and own flag is in enemy's base
                {
                    collidingAgent.GetComponent<AgentMovementWSAD>().AddRewardAgent(RewardValuesScript.rewards["flagCaptured"]);
                    OtherBase.GetComponent<ReturnFlagScript>().returnFlagFromBase(collidingAgent, EnemyFlagInBase);
                }
            }
        }
    }
}

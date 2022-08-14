using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadlyWaterScript : MonoBehaviour
{
    private GameObject OwnBase;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AgentBiomCollider")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Kill Agent (get back to base)");
            var agentBiomCollider = other.gameObject;
            var agent = agentBiomCollider.transform.parent.gameObject;

            GameObject agentFlag = agent.GetComponent<AgentComponentsScript>().AgentFlag;
            if (agentFlag.activeSelf)
            {
                string agentColor = agent.GetComponent<AgentComponentsScript>().color;
                OwnBase = agentColor == "blue" ? OwnBase = GameObject.Find("Red Base(Clone)") : OwnBase = GameObject.Find("Blue Base(Clone)");
                OwnBase.GetComponent<ReturnFlagScript>().returnFlagFromAgent(agentFlag, agent);
            }

            agent.GetComponent<AgentMovementWSAD>().Kill();
        }
    }
}

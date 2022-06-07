using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentFlagCapturingScript : MonoBehaviour
{
    private GameObject OwnBase;
    private void OnTriggerEnter(Collider colidingObject)
    {
        if (colidingObject.tag == "Agent")
        {
            GameObject agentFlag = gameObject.GetComponent<AgentComponentsScript>().AgentFlag;
            if (agentFlag.activeSelf)
            {
                string agentColor = gameObject.GetComponent<AgentComponentsScript>().color;
                OwnBase = agentColor == "blue" ? OwnBase = GameObject.Find("Red Base(Clone)") : OwnBase = GameObject.Find("Blue Base(Clone)");
                OwnBase.GetComponent<ReturnFlagScript>().returnFlagFromAgent(agentFlag, agentColor);
            }
        }
    }

}

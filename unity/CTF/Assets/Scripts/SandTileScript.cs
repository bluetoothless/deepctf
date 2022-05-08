using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandTileScript : MonoBehaviour
{
    public float speedChangeModifier = 0.5f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AgentBiomCollider")
        {
            var agentBiomCollider = other.gameObject;
            Debug.Log("SAND!! Slow the Agent!");
            var agent = agentBiomCollider.transform.parent.gameObject;
            agent.GetComponent<AgentMovementWSAD>().changeSpeedModifier(speedChangeModifier);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

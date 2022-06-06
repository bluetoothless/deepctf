using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadlyWaterScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AgentBiomCollider")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Kill Agent (get back to base)");
            var agentBiomCollider = other.gameObject;
            var agent = agentBiomCollider.transform.parent.gameObject;
            agent.GetComponent<AgentMovementWSAD>().Kill();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class AgentMovementWSAD : Agent
{
    public float rotateSpeed = 180f;
    public float forwardSpeed = 600f;
    public float backSpeed = 450f;
    private float speedModifier = 1f;



    public void ChangeSpeedModifier(float newModified)
    {
        speedModifier = newModified;
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }
}

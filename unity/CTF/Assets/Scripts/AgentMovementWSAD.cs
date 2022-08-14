using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class AgentMovementWSAD : Agent
{
    public float rotateSpeed = 180f;
    public float forwardSpeed = 600f;
    public float backSpeed = 450f;
    private float speedModifier = 1f;
    public GameObject[] agents;
    private List<GameObject> teamBlue = new List<GameObject>{};
    private List<GameObject> teamRed = new List<GameObject>{};

    void Start()
    {
        GetTeams();
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        var forwardAxis = actionBuffers.DiscreteActions[0];
        var rotateAxis = actionBuffers.DiscreteActions[1];

        Rigidbody rb = GetComponent<Rigidbody>();
        switch (forwardAxis)
        {
            case 1: //do przodu
                rb.AddForce(transform.rotation * Vector3.forward * forwardSpeed * Time.deltaTime * speedModifier, ForceMode.VelocityChange);
                break;
            case 2: //do ty³u
                rb.AddForce(transform.rotation * Vector3.back * backSpeed * Time.deltaTime * speedModifier, ForceMode.VelocityChange);
                break;
        }

        switch (rotateAxis)
        {
            case 1: //w lewo
                transform.Rotate(0, -rotateSpeed * Time.deltaTime * speedModifier, 0, Space.World);
                break;
            case 2: // w prawo
                transform.Rotate(0, rotateSpeed * Time.deltaTime * speedModifier, 0, Space.World);
                break;
        }


    }

    public override void CollectObservations(VectorSensor sensor)
    {

    }


    private void raysPerception()
    {
        int layerMask = 1 << 6;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log(this.name + "Did Hit: " + hit.distance + hit.collider);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        bool W = Input.GetKey(KeyCode.W);
        bool S = Input.GetKey(KeyCode.S);
        bool A = Input.GetKey(KeyCode.A);
        bool D = Input.GetKey(KeyCode.D);

        /*
        bool W = true ? Random.Range(0, 10) > 1 : false;
        bool S = true ? Random.Range(0, 2) == 1 : false;
        bool A = true ? Random.Range(0, 10) > 2 : false;
        bool D = true ? Random.Range(0, 20) > 1 : false;*/

        // Faster forward than back
        Rigidbody rb = GetComponent<Rigidbody>();
        if (W)
            rb.AddForce(transform.rotation * Vector3.forward * forwardSpeed * Time.deltaTime * speedModifier, ForceMode.VelocityChange);
        else if (S)
            rb.AddForce(transform.rotation * Vector3.back * backSpeed * Time.deltaTime * speedModifier, ForceMode.VelocityChange);
        else
            rb.velocity = Vector3.zero;


        if (A)
            transform.Rotate(0, -rotateSpeed * Time.deltaTime * speedModifier, 0, Space.World);
        if (D)
            transform.Rotate(0, rotateSpeed * Time.deltaTime * speedModifier, 0, Space.World);


        speedModifier = 1f;


        raysPerception();
    }


    public void ChangeSpeedModifier(float newModified)
    {
        speedModifier = newModified;
    }

    public void AddRewardAgent(float reward)
    {
        AddReward(reward);
    }

    public void AddRewardTeam(float reward, string color)
    {
        if (color == "blue")
        {
            foreach (GameObject agent in teamBlue)
            {
                agent.GetComponent<AgentMovementWSAD>().AddRewardAgent(reward);
            }
        }
        else
        {
            foreach (GameObject agent in teamRed)
            {
                agent.GetComponent<AgentMovementWSAD>().AddRewardAgent(reward);
            }
        }
    }

    public void Kill()
    {
        var rewardValues = gameObject.GetComponent<RewardValuesScript>();
        rewardValues.getRewardValues();
        AddRewardAgent(rewardValues.rewards["agentDead"]);
        gameObject.SetActive(false);

        CheckIfLost();
    }

    private void CheckIfLost()
    {
        bool aliveAgents = false;
        Transform parent = gameObject.transform.parent;

        for (int i = 0; i < parent.childCount; i++)
        {
            GameObject agent = parent.GetChild(i).gameObject;
            if (agent.activeSelf)
            {
                aliveAgents = true;
            }
        }

        if (!aliveAgents) // if all agents from team died
        {
            var rewardValues = gameObject.GetComponent<RewardValuesScript>();
            rewardValues.getRewardValues();
            if (gameObject.GetComponent<AgentComponentsScript>().color == "blue")
            {
                AddRewardTeam(rewardValues.rewards["gameLost"], "blue");
                AddRewardTeam(rewardValues.rewards["gameWon"], "red");
            }
            else
            {
                AddRewardTeam(rewardValues.rewards["gameLost"], "red");
                AddRewardTeam(rewardValues.rewards["gameWon"], "blue");
            }
        }
    }
    private void GetTeams()
    {
        Transform agents = gameObject.transform.parent.transform.parent;
        Transform redAgents = agents.GetChild(0);
        Transform blueAgents = agents.GetChild(1);
        for (int i = 0; i < gameObject.transform.parent.childCount; i++)
        {
            teamRed.Add(redAgents.GetChild(i).gameObject);
            teamBlue.Add(blueAgents.GetChild(i).gameObject);
        }
    }
}

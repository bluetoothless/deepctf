using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;
using Unity.MLAgents.Sensors;

public class AgentMovementWSAD : Agent
{
    public float rotateSpeed = 180f;
    public float forwardSpeed = 600f;
    public float backSpeed = 450f;
    private float speedModifier = 1f;
   

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
        //jak juz zaczniemy uzywac to odkomentowac:
       // float[,] arrRays = raysPerception(); //40 floatow
       //
    }


    private float[,] raysPerception()
    {
        int layerMask = 1 << 6;

        float RayDistance = 200.0f;
        int numberOfRays = 10;
        float startDegree = -90.0f;//zawsze musi byc ujemne bo jebnie!
        float stepDegree = -2 * startDegree / (float)numberOfRays;
        

        float[,] outputArray = new float[10,4]; //10 promieni, po 4 zmienne, i,0-distance, i,1 - rodzaj, i,2 kolor, i,3 czy z flaga?
        RaycastHit hit;
        Ray ray;
        for (int i =0;i<numberOfRays;i++)
        {
           ray  = new Ray(transform.position, transform.TransformDirection(Quaternion.Euler(0, startDegree+stepDegree*i, 0) * Vector3.forward));

            if (Physics.Raycast(ray, out hit, RayDistance, layerMask))
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
                Debug.Log(this.name + "Ray"+i+ "Did Hit: " + hit.collider.gameObject+" in distance: " + +hit.distance);
                outputArray[i, 0] = hit.distance;
                //hit.collider.gameObject.GetComponent(typeof(RayResponder));
              //  Debug.Log(RayResponder.message());

            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * RayDistance, Color.green);
                //Debug.Log(this.name + "Ray"+i+ "Did not Hit");
            }

        }
        return outputArray;
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

        //dla widzenia promieni
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

    public void Kill()
    {
        AddRewardAgent(-1000f);
        gameObject.SetActive(false);
    }
}

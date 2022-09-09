using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevel2 : TestLevel
{
    public float rotateSpeed = 180f;
    public float forwardSpeed = 600f;
    public float backSpeed = 450f;

    public override void Run(Transform t)
    {
        bool W = true ? Random.Range(0, 10) > 1 : false;
        bool S = true ? Random.Range(0, 2) == 1 : false;
        bool A = true ? Random.Range(0, 10) > 2 : false;
        bool D = true ? Random.Range(0, 20) > 1 : false;

        Rigidbody rb = t.gameObject.GetComponent<Rigidbody>();
        // Faster forward than back
        if (W)
            rb.AddForce(t.rotation * Vector3.forward * forwardSpeed * Time.deltaTime * 1, ForceMode.VelocityChange);
        else if (S)
            rb.AddForce(t.rotation * Vector3.back * backSpeed * Time.deltaTime * 1, ForceMode.VelocityChange);
        else
            rb.velocity = Vector3.zero;


        if (A)
            t.Rotate(0, -rotateSpeed * Time.deltaTime * 1, 0, Space.World);
        if (D)
            t.Rotate(0, rotateSpeed * Time.deltaTime * 1, 0, Space.World);
    }

    public override void Spawn()
    {
        foreach (var agent in GameManager.BlueAgents)
        {
            agent.transform.position = new Vector3(agent.transform.position.x, 20, agent.transform.position.z);
        }

        var blueBaseScript = GameObject.Find("Blue Base(Clone)").GetComponent<BlueBaseScript>();

        for (int i = 0; i < Random.Range(10, 30); i++)
        {
            int index;
            do
            {
                index = Random.Range(0, 8899);
                if (blueBaseScript.CheckIfCanSpawnAt(index))
                    break;
            } while (true);

            blueBaseScript.SpawnAgentAt(index);
        }
    }
}

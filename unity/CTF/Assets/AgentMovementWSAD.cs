using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovementWSAD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(transform.rotation * Vector3.left * 500, ForceMode.Force);
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(transform.rotation * Vector3.right * 500, ForceMode.Force);
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(transform.rotation * Vector3.forward * 500, ForceMode.Force);
        if (Input.GetKey(KeyCode.S))
            rb.AddForce(transform.rotation * Vector3.back * 500, ForceMode.Force);
        //else
            //rb.inertiaTensor = Vector3.zero;
            //rb.velocity = Vector3.zero;
            //rb.angularVelocity = Vector3.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovementWSAD : MonoBehaviour
{
    public float rotateSpeed = 1f;
    public float forwardSpeed = 1f;
    public float backSpeed = 1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Faster forward than back

        Rigidbody rb = GetComponent<Rigidbody>();
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(transform.rotation * Vector3.forward * forwardSpeed * Time.deltaTime, ForceMode.VelocityChange);
        else if (Input.GetKey(KeyCode.S))
            rb.AddForce(transform.rotation * Vector3.back * backSpeed * Time.deltaTime, ForceMode.VelocityChange);
        else
            rb.velocity = Vector3.zero;


        if (Input.GetKey(KeyCode.A))
            transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0, Space.World);
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.World);


        //else
        //rb.inertiaTensor = Vector3.zero;
        //rb.velocity = Vector3.zero;
        //rb.angularVelocity = Vector3.zero;
    }
}

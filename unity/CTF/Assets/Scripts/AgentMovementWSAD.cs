using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovementWSAD : MonoBehaviour
{
    public float rotateSpeed = 180f;
    public float forwardSpeed = 600f;
    public float backSpeed = 450f;
    private float speedModifier = 1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*        WSAD MOVEMENT*/
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

        //else
        //rb.inertiaTensor = Vector3.zero;
        //rb.velocity = Vector3.zero;
        //rb.angularVelocity = Vector3.zero;
    }

    public void changeSpeedModifier(float newModified)
    {
        speedModifier = newModified;
    }
}

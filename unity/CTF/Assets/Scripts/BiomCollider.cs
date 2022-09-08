using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomCollider : MonoBehaviour
{
    // Start is called before the first frame update
    private int BiomTag = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "lake")
        {
            BiomTag = 3;
        }
        else if (other.tag == "desert")
        {
            BiomTag = 2;
        }
        else if (other.tag == "accelerate")
        {
            BiomTag = 1;
        }
        else
        {
            BiomTag = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        OnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        BiomTag = 0;
    }

    public int getBiomTag()
    {
        return BiomTag;
    }
}

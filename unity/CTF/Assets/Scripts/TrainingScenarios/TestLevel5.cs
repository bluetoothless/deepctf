using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevel5 : TestLevel
{
    public override void Run(Transform t)
    {
        t.position = new Vector3(t.position.x, 20, t.position.z);
    }
}
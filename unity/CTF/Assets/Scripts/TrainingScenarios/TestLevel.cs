using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TestLevel
{
    public virtual void Run(Transform t) {}
    public virtual void Spawn(List<GameObject> team) {}
}

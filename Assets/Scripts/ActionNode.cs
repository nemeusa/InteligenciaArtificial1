using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : Node
{
    public System.Action<Boid> action;

    public ActionNode(System.Action<Boid> action)
    {
        this.action = action;
    }

    public override void Execute(Boid boid)
    {
        action?.Invoke(boid);
    }
}

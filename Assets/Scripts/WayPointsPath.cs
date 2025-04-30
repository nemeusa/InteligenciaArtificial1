using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointsPath : MonoBehaviour
{
    public Transform[] nodes;

    public Transform GetNode(int index)
    {
        return nodes[index % nodes.Length];
    }

    public int Length => nodes.Length;
}

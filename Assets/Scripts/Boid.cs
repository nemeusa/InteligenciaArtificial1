using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    [SerializeField] Hunter _hunter;
    [SerializeField] float maxVelocity;

    [Range(0f, 1f)]
    [SerializeField] float velocity;

    void Update()
    {
        
    }

    //public Vector3 Evade(Hunter hunter)
    //{
    //   var dir = hunter.transform.position + hunter.Velocity;

    //  return dir;
    //}
}

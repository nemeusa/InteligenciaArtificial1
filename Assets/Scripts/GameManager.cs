using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header ("Flocking")]
    public List<Boid> boids = new List<Boid>();
    [Range(0f, 1f)] public float weightSeparation;
    [Range(0f, 1f)] public float weightAligment;
    [Range(0f, 1f)] public float weightCohesion;

    [Header ("Decisions")]
    public Transform hunter;
    public List<Transform> fruits = new List<Transform>();

    private void Awake()
    {
        instance = this;
    }
}

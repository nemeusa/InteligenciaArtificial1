using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    public List<Boid> boids = new List<Boid>();

    [Range(0f, 1f)] public float weightSeparation;
    [Range(0f, 1f)] public float weightAligment;
    [Range(0f, 1f)] public float weightCohesion;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
}

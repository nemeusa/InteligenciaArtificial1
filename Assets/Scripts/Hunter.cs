using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Hunter : MonoBehaviour
{
    FSM<TypeFSM> _fsm;

    [Header ("Energy")]
    public float maxEnergy;
    public float energyRecovery; 
    public float energyDrain;
     public float currentEnergy;
    public bool isTired => currentEnergy <= 0f;

    [Header ("Move")]
    public float _maxVelocity;
    [Range(0f, 1f)] public float _maxForce;
    public Boid _boid;
    [HideInInspector] public Vector3 _velocity;
    public WayPointsPath path;
    [HideInInspector] public int currentNodeIndex = 0;
    public float nodeArrivalThreshold = 0.5f;

    [Header ("Pursuit")]
    [HideInInspector] public List<Boid> visibleBoids = new();
    [SerializeField] float viewRadius;
    [SerializeField] float viewAngle;
    public bool isPursuit;

    void Awake()
    {
        _fsm = new FSM<TypeFSM>();

        _fsm.AddState(TypeFSM.Idle, new IdleState(_fsm, this));
        _fsm.AddState(TypeFSM.Move, new MoveState(_fsm, this));
        _fsm.AddState(TypeFSM.Pursuit, new PursuitState(_fsm, this));

        _fsm.ChangeState(TypeFSM.Move);
        currentEnergy = maxEnergy;
    }

    void Update()
    {
        _fsm.Execute();
        UpdateVision();
        CheckBoidCollision();
    }

    public List<Boid> GetVisibleBoids()
    {
        List<Boid> visibleBoids = new();

        foreach (Boid boid in GameManager.instance.boids)
        {
            Vector3 dirBoid = boid.transform.position - transform.position;
            float dist = dirBoid.magnitude;

            if (dist <= viewRadius)
            {
                float angle = Vector3.Angle(transform.forward, dirBoid);

                if (angle <= viewAngle / 2f)
                {
                    visibleBoids.Add(boid);
                }
            }
        }

        return visibleBoids;
    }

    public void UpdateVision()
    {
        visibleBoids.Clear();

        foreach (Boid boid in GameManager.instance.boids)
        {
            Vector3 dirToBoid = boid.transform.position - transform.position;
            float distance = dirToBoid.magnitude;

            if (distance <= viewRadius)
            {
                float angle = Vector3.Angle(transform.forward, dirToBoid);
                if (angle <= viewAngle / 2f)
                {
                    visibleBoids.Add(boid);
                }
            }
        }
    }

    private void CheckBoidCollision()
    {
        if (_fsm.CurrentStateKey == TypeFSM.Pursuit)
        {
            var boids = GameManager.instance.boids.ToList();

            foreach (var boid in boids)
            {
                if (boid == null) continue;

                if (Vector3.Distance(transform.position, boid.transform.position) < 1.2f)
                {
                    GameManager.instance.boids.Remove(boid);
                    boid.Kill();
                    Debug.Log("eliminadoo");
                    break;
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, leftBoundary * viewRadius);
        Gizmos.DrawRay(transform.position, rightBoundary * viewRadius);
    }
}

public enum TypeFSM
{
    Idle,
    Move,
    Pursuit
}

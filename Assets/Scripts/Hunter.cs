using System.Collections.Generic;
using UnityEngine;
public class Hunter : MonoBehaviour
{
    FSM<TypeFSM> _fsm;

    [Header ("Energy")]
    public float maxEnergy;
    public float energyRecovery; 
    public float energyDrain;
    [HideInInspector] public float currentEnergy;
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
    [SerializeField] float viewRadius;
    [SerializeField] float viewAngle;

    void Awake()
    {
        _fsm = new FSM<TypeFSM>();

        _fsm.AddState(TypeFSM.Idle, new IdleState(_fsm, this));
        _fsm.AddState(TypeFSM.Move, new MoveState(_fsm, this));
        _fsm.AddState(TypeFSM.Pursuit, new PursuitState(_fsm, this));

        _fsm.ChangeState(TypeFSM.Move);
    }

    void Update()
    {
        _fsm.Execute();
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

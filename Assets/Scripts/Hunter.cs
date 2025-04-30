using UnityEngine;
using UnityEngine.TextCore.Text;

public class Hunter : MonoBehaviour
{
    FSM<TypeFSM> _fsm;

    [Header ("Energy")]
    public float maxEnergy;
    public float energyRecoveryRate; 
    public float energyDrainRate;
    [HideInInspector]
    public float currentEnergy;
    public bool isTired => currentEnergy <= 0f;

    [Header ("Move")]
    public float _maxVelocity;
    [Range(0f, 1f)]
    public float _maxForce;
    public Boid _boid;
    [HideInInspector]
    public Vector3 _velocity;


    void Awake()
    {
        _fsm = new FSM<TypeFSM>();

        _fsm.AddState(TypeFSM.Idle, new IdleState(_fsm, this));
        _fsm.AddState(TypeFSM.Move, new MoveState(_fsm, this));
        _fsm.AddState(TypeFSM.Pursuit, new PursuitState(_fsm, this));

        _fsm.ChangeState(TypeFSM.Idle);
    }

    void Update()
    {
        _fsm.Execute();
    }
}

public enum TypeFSM
{
    Idle,
    Move,
    Pursuit
}

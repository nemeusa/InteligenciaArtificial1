using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    FSM<TypeFSM> _fsm;

    Color _color;

    void Awake()
    {
        _fsm = new FSM<TypeFSM>();

        _fsm.AddState(TypeFSM.Idle, new IdleState(_fsm));
        _fsm.AddState(TypeFSM.Move, new MoveState());

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
    Move
}

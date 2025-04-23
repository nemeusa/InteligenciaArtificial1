using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    FSM<TypeFSM> _fsm;
    TypeFSM _typeFSM;

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
        //if (_typeFSM == TypeFSM.Idle)
        if (_fsm.CurrentStateKey == TypeFSM.Idle)
        {
            Debug.Log("toy azul xd");
            _color = Color.blue;
            OnColor();
        }

        if (_fsm.CurrentStateKey == TypeFSM.Move)
        {
            Debug.Log("toy rojo xd");
            _color = Color.red;
            OnColor();
        }

    }
    public void OnColor()
    {
        GetComponent<Renderer>().material.color = _color;
    }
}

public enum TypeFSM
{
    Idle,
    Move
}

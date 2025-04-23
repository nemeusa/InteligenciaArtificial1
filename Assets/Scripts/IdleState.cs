using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : Istate
{
    FSM<TypeFSM> _fsm;
    Hunter _hunter;

    
    public IdleState(FSM<TypeFSM> fsm)
    {
        _fsm = fsm;
    }

    public void OnEnter()
    {
        Debug.Log("Enter to Idle");
    }
    public void OnUpdate()
    {
        Debug.Log("Update to Idle");
        if (Input.GetKeyDown(KeyCode.Space)) _fsm.ChangeState(TypeFSM.Move);
    }

    public void OnExit()
    {
        Debug.Log("Exit to Idle");
    }
}

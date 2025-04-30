using UnityEngine;

public class IdleState : Istate
{
    FSM<TypeFSM> _fsm;
    Hunter _hunter;
    
    public IdleState(FSM<TypeFSM> fsm, Hunter hunter)
    {
        _fsm = fsm;
        _hunter = hunter;
    }

    public void OnEnter()
    { 
        _hunter.GetComponent<Renderer>().material.color = Color.blue;
    }

    public void OnUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) _fsm.ChangeState(TypeFSM.Move);
        if (_hunter.currentEnergy == _hunter.maxEnergy)
        {
            _fsm.ChangeState(TypeFSM.Move);
        }
        _hunter.currentEnergy += _hunter.energyRecoveryRate * Time.deltaTime;
        _hunter.currentEnergy = Mathf.Clamp(_hunter.currentEnergy, 0f, _hunter.maxEnergy);
    }

    public void OnExit()
    { }
}

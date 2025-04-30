using UnityEngine;

public class PursuitState : Istate
{
    FSM<TypeFSM> _fsm;
    Hunter _hunter;

    public PursuitState(FSM<TypeFSM> fsm, Hunter hunter)
    {
        _fsm = fsm;
        _hunter = hunter;
    }
   
    public void OnEnter()
    {
        Debug.Log("Enter to Pursuit");
        _hunter.GetComponent<Renderer>().material.color = Color.red;
    }
    public void OnUpdate()
    {
        Debug.Log("Update to Pursuit");
    }

    public void OnExit()
    {
        Debug.Log("Exit to Pursuit");
    }
}

using UnityEngine;

public class MoveState : Istate
{
    public void OnEnter()
    {
        Debug.Log("Enter to Move");
    }
    public void OnUpdate()
    {
        Debug.Log("Update to Move");
    }

    public void OnExit()
    {
        Debug.Log("Exit to Move");
    }
}

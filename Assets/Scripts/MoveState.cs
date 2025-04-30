using System.Collections.Generic;
using UnityEngine;
public class MoveState : Istate
{
    FSM<TypeFSM> _fsm;
    Hunter _hunter;

    public MoveState(FSM<TypeFSM> fsm, Hunter hunter)
    {
        _fsm = fsm;
        _hunter = hunter;
    }

    public void OnEnter()
    {
        _hunter.GetComponent<Renderer>().material.color = Color.yellow;
        _hunter.currentEnergy = _hunter.maxEnergy;
    }
    public void OnUpdate()
    {
        //MOVEMENT
        List<Boid> visibleBoids = _hunter.GetVisibleBoids();

        if (visibleBoids.Count == 0)
        {
            Vector3 nodePos = _hunter.path.GetNode(_hunter.currentNodeIndex).position;

            AddForce(Seek(nodePos));
            _hunter.transform.position += _hunter._velocity * Time.deltaTime;

            float dist = Vector3.Distance(_hunter.transform.position, nodePos);
            if (dist <= _hunter.nodeArrivalThreshold)
            {
                _hunter.currentNodeIndex++;
                if (_hunter.currentNodeIndex >= _hunter.path.Length)
                {
                    _hunter.currentNodeIndex = 0;
                }
            }

            return;
        }

        //ENERGY
        if (_hunter.isTired)
        {
            _fsm.ChangeState(TypeFSM.Idle);
            return;
        }
        _hunter.currentEnergy -= _hunter.energyDrain * Time.deltaTime;
        _hunter.currentEnergy = Mathf.Clamp(_hunter.currentEnergy, 0f, _hunter.maxEnergy);
    }

    public void AddForce(Vector3 dir)
    {
        _hunter._velocity = Vector3.ClampMagnitude(_hunter._velocity + dir, _hunter._maxVelocity);
        _hunter.transform.forward = _hunter._velocity;
    }

    public Vector3 Seek(Vector3 boid)
    {
        var dir = boid - _hunter.transform.position;
        dir.Normalize();
        dir *= _hunter._maxVelocity;

        var steering = dir - _hunter._velocity;
        steering = Vector3.ClampMagnitude(steering, _hunter._maxForce);

        return steering;
    }


    public void OnExit()
    { }
}

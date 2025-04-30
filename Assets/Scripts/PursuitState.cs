using System.Collections.Generic;
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
        _hunter.currentEnergy = _hunter.maxEnergy;
    }
    public void OnUpdate()
    {
        //PURSUIT
        List<Boid> visibleBoids = _hunter.GetVisibleBoids();
        Boid closest = null;
        float minDist = float.MaxValue;

        foreach (Boid boid in visibleBoids)
        {
            float dist = Vector3.Distance(_hunter.transform.position, boid.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = boid;
            }
        }

        if (visibleBoids.Count == 0) _fsm.ChangeState(TypeFSM.Move);



        if (closest != null)
        {
            AddForce(Seek(closest.transform.position));

            _hunter.transform.position += _hunter._velocity * Time.deltaTime;
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

    public void OnExit()
    { }

    public Vector3 Seek(Vector3 boid)
    {
        var dir = boid - _hunter.transform.position;
        dir.Normalize();
        dir *= _hunter._maxVelocity;

        var steering = dir - _hunter._velocity;
        steering = Vector3.ClampMagnitude(steering, _hunter._maxForce);

        return steering;
    }

    public Vector3 Pursuit(Boid boid)
    {
        var dir = boid.transform.position + boid._velocity;
        return Seek(dir);
    }

    public void AddForce(Vector3 dir)
    {
        _hunter._velocity = Vector3.ClampMagnitude(_hunter._velocity + dir, _hunter._maxVelocity);
        _hunter.transform.forward = _hunter._velocity;
    }
}
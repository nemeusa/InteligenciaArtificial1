using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] Transform objeto;
    [SerializeField] float _maxVelocity;
    [SerializeField] float _maxForce;

    Vector3 _velocity;
    void Update()
    {
        AddForce(Seek(transform.position));

        transform.forward = _velocity;
        transform.position += _velocity * Time.deltaTime;
    }

    public Vector3 Seek(Vector3 target)
    {
        var desired = target - transform.position;
        desired.Normalize();
        desired *= _maxVelocity;

        var steering = desired - _velocity;
            steering = Vector3.ClampMagnitude(desired,_maxForce);
        return steering;
    }

    public void AddForce(Vector3 dir)
    {
        var vectorcitouwu = Vector3.ClampMagnitude(_velocity + dir, _maxVelocity);

        transform.forward = _velocity;
    }
}

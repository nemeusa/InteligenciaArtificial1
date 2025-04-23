using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTest : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _maxVelocity;
    [Range(0f, 1f)]
    [SerializeField] float _maxForce;

    Vector3 _velocity;
    public Vector3 Velocity { get { return _velocity; } }
    private void Update()
    {
        AddForce(transform.forward);

        transform.position += _velocity * Time.deltaTime;
    }

    public void AddForce(Vector3 dir)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + dir, _maxVelocity);

        transform.forward = _velocity;
    }
}

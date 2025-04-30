using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Boid : MonoBehaviour
{
    [Header ("Flocking")]
    [SerializeField] float _radiusSeparation;
    [SerializeField] float _radiusDetection;

    [Header ("movement")]
    [SerializeField] float _maxVelocity;
    [SerializeField] Hunter _hunter;
    [Range(0f, 1f)]
    [SerializeField] float _maxForce;

    [HideInInspector]
    public Vector3 _velocity;
    public Vector3 Velocity { get { return _velocity; } }

    private void Start()
    {
        GameManager.instance.boids.Add(this);
        AddForce(new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * _maxVelocity);
    }

    private void Update()
    {
        Flocking();

        //Movement
        AddForce(Evade(_hunter));
        transform.position += _velocity * Time.deltaTime;
    }

    #region flocking
    void Flocking()
    {
        AddForce(Separation(GameManager.instance.boids, _radiusSeparation) * GameManager.instance.weightSeparation);
        AddForce(Aligment(GameManager.instance.boids, _radiusDetection) * GameManager.instance.weightAligment);
        AddForce(Cohesion(GameManager.instance.boids, _radiusDetection) * GameManager.instance.weightCohesion);
    }

    Vector3 Separation(List<Boid> boids, float radius)
    {
        Vector3 desired = Vector3.zero;

        foreach (var item in boids)
        {
            var dir = item.transform.position - transform.position;

            if (dir.magnitude > radius || item == this) continue;

            desired -= dir;
        }

        if (desired == Vector3.zero)
            return desired;

        desired.Normalize();
        desired *= _maxVelocity;

        var steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce);
        return steering;
    }

    Vector3 Aligment(List<Boid> boids, float radius)
    {
        Vector3 desired = Vector3.zero;

        foreach (var item in boids)
        {
            var dir = item.transform.position - transform.position;

            if (dir.magnitude > radius || item == this) continue;

            desired += item.Velocity;
        }

        if (desired == Vector3.zero)
            return desired;

        desired.Normalize();
        desired *= _maxVelocity;

        var steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce);
        return steering;
    }

    Vector3 Cohesion(List<Boid> boids, float radius)
    {
        Vector3 desired = Vector3.zero;

        foreach (var item in boids)
        {
            var dir = item.transform.position - transform.position;

            if (dir.magnitude > radius || item == this) continue;

            desired += item.transform.position;
        }

        if (desired == Vector3.zero)
            return desired;

        desired -= transform.position;

        desired.Normalize();
        desired *= _maxVelocity;

        var steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce);
        return steering;
    }
    #endregion

    #region Directions
    public Vector3 Evade(Hunter hunter)
    {
        var dir = hunter.transform.position + hunter._velocity;
        return Flee(dir);
    }
    public void AddForce(Vector3 dir)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + dir, _maxVelocity);
        transform.forward = _velocity;
    }


    public Vector3 Flee(Vector3 hunter)
    {
        return -Seek(hunter);
    }
    public Vector3 Seek(Vector3 hunter)
    {
        var dir = hunter - transform.position;
        dir.Normalize();
        dir *= _maxVelocity;

        var steering = dir - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce);

        return steering;
    }
    #endregion


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radiusSeparation);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radiusDetection);
    }
}

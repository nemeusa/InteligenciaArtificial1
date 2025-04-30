using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boid : MonoBehaviour
{
    [Header ("Flocking")]
    [SerializeField] float _radiusSeparation;
    [SerializeField] float _radiusDetection;

    [Header ("movement")]
    [SerializeField] float _radiusArrive;
    [SerializeField] float _maxVelocity;
    [SerializeField] Hunter _hunter;
    [Range(0f, 1f)] [SerializeField] float _maxForce;
    Vector3 _randomTarget;
    float _timer;

    [Header ("Decision tree")]
    public BoidState currentState;
    [SerializeField] float _stateChangeRate = 2f;
    float _stateTimer;

    bool _isDead = false;

    [HideInInspector] public Vector3 _velocity;
    public Vector3 Velocity { get { return _velocity; } }

    private void Start()
    {
        //ADDS
        GameManager.instance.boids.Add(this);
        AddForce(new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * _maxVelocity);
    }

    private void Update()
    {
        if (_isDead) return;

        StateController();
        HandleState();
        CheckFruitCollision();
        transform.position += _velocity * Time.deltaTime;
    }

    void StateController()
    {
        {
            float hunterDist = Vector3.Distance(transform.position, _hunter.transform.position);
            Transform closestFruit = GetClosestFruit(10f);

            if (hunterDist < 5f)
            {
                currentState = BoidState.Scape;
                Debug.Log("scape");

                return;
            }

            if (closestFruit != null)
            {
                currentState = BoidState.Gather;
                Debug.Log("frutitas :D");
                return;
            }

            foreach (var boid in GameManager.instance.boids)
            {
                if (boid == this) continue;
                if (Vector3.Distance(transform.position, boid.transform.position) < _radiusDetection)
                {
                    currentState = BoidState.Flocking;
                    return;
                }
            }

            currentState = BoidState.Walk;
        }
    }

    void HandleState()
    {
        switch (currentState)
        {
            case BoidState.Walk:
                UpdateIdle();
                break;

            case BoidState.Flocking:
                Flocking();
                break;

            case BoidState.Gather:
                Gather();
                break;

            case BoidState.Scape:
                AddForce(Evade(_hunter));
                break;
        }

    }

    public void Kill()
    {
        _isDead = true;
        GetComponent<Renderer>().material.color = Color.white;
        Destroy(gameObject, 1);
    }

    void Gather()
    {
        Transform closestFruit = GetClosestFruit(10f);
        if (closestFruit != null)
        {
            AddForce(Arrive(closestFruit.position));
        }
    }

    Transform GetClosestFruit(float maxDistance)
    {
        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (var fruit in GameManager.instance.fruits)
        {
            float dist = Vector3.Distance(transform.position, fruit.position);

            if (dist < maxDistance && dist < minDist)
            {

                minDist = dist;
                closest = fruit;
            }
        }

        return closest;
    }

    private void CheckFruitCollision()
    {
        if (currentState == BoidState.Gather)
        {
            foreach (var fruit in GameManager.instance.fruits.ToList())
            {
                if (Vector3.Distance(transform.position, fruit.position) < 1f)
                {
                    GameManager.instance.fruits.Remove(fruit);
                    Destroy(fruit.gameObject);
                    break;
                }
            }
        }
    }


    void UpdateIdle()
    {
        _stateTimer += Time.deltaTime;

        if (_stateTimer >= _stateChangeRate || Vector3.Distance(transform.position, _randomTarget) < 1f)
        {
            _randomTarget = transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            _stateTimer = 0f;
        }

        Debug.Log("walk");

        AddForce(Arrive(_randomTarget));
    }

    #region flocking

    void Flocking()
    {
        AddForce(Separation(GameManager.instance.boids, _radiusSeparation) * GameManager.instance.weightSeparation);
        AddForce(Aligment(GameManager.instance.boids, _radiusDetection) * GameManager.instance.weightAligment);
        AddForce(Cohesion(GameManager.instance.boids, _radiusDetection) * GameManager.instance.weightCohesion);
        Debug.Log("Flockinggg");

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

        if (desired == Vector3.zero) return desired;

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




    public Vector3 Arrive(Vector3 target)
    {
        float dist = Vector3.Distance(transform.position, target);

        if (dist > _radiusArrive) return Seek(target);

        var desired = target - transform.position;
        desired.Normalize();
        desired *= _maxVelocity * (dist / _radiusArrive);

        var steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce);

        return steering;
    }
    public void AddForce(Vector3 dir)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + dir, _maxVelocity);
        transform.forward = _velocity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radiusSeparation);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radiusDetection);
    }
}

public enum BoidState
{
    Walk,
    Flocking,
    Gather,
    Scape
}

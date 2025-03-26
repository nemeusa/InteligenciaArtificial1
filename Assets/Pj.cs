using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pj : MonoBehaviour
{
    public Enemy enemy;

    private void Start()
    {
        enemy = FindObjectOfType<Enemy>();
    }
    void Update()
    {
        var dir = enemy.transform.position - transform.position;
        var dist = Vector3.Distance(enemy.transform.position, transform.position);

        transform.forward = dir;
        transform.position += transform.forward * 6 * Time.deltaTime;
    }
}

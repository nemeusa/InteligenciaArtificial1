using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pj : MonoBehaviour
{
    public Enemy enemy;
    public Transform objeto;
    public Node firstNode;

    private void Start()
    {
        enemy = FindObjectOfType<Enemy>();
    }
    void Update()
    {
        var dir = enemy.transform.position - transform.position;
        var dirObj = objeto.transform.position - transform.position;
        var dist = Vector3.Distance(enemy.transform.position, transform.position);
        var distObj = Vector3.Distance(objeto.transform.position, transform.position);

        if (dist > 1 && distObj < 5)
        transform.forward = dir;
        else if (dist < 1 && distObj > 5)
        transform.forward = dirObj;
        transform.position += transform.forward * 6 * Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class ColorHunter : MonoBehaviour
{
    FSM<TypeFSM> _typeFSM;
    TypeFSM _type;
    Color _color;

    void Start()
    {
        
    }

    void Update()
    {
        if(_type == TypeFSM.Idle)
        {
            Debug.Log("toy azul xd");
            _color = Color.blue;
            OnColor();
        }
        eif(_type == TypeFSM.Move)
        {
            Debug.Log("toy rojo xd");
            _color = Color.red;
            OnColor();
        }
    }


    public void OnColor()
    {
        GetComponent<Renderer>().material.color = _color;
    }
}

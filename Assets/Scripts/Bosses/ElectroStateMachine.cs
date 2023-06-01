using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroStateMachine : StateMachine
{
    [field:SerializeField] public Health Player { get; private set; }
    
    [field : SerializeField ] public float DetectionRange { get; private set; }

    [field: SerializeField] public List<Transform> InnerCubes { get; private set; }

    private void Start()
    {
       // Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        SwitchState(new ElectroIdleState(this));
    }
}

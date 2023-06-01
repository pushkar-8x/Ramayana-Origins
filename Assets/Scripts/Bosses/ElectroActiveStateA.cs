using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElectroActiveStateA : ElectroBaseState
{
    
    public ElectroActiveStateA(ElectroStateMachine stateMachine) : base(stateMachine)
    {
       
    }

    public override void Enter()
    {
        
    }

    public override void Tick(float deltaTime)
    {
        foreach (Transform t in stateMachine.InnerCubes)
        {
            t.Rotate(Vector3.up * 100f * Time.deltaTime);
        }

        if(!IsInChaseRange())
            stateMachine.SwitchState(new ElectroIdleState(stateMachine));
    }

    public override void Exit()
    {
        
    }

   

   
}

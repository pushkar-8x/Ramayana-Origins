using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroIdleState : ElectroBaseState
{
    public ElectroIdleState(ElectroStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
       
    }

    
    public override void Tick(float deltaTime)
    {
       if(IsInChaseRange())
        {
            stateMachine.SwitchState(new ElectroActiveStateA(stateMachine));
        }
    }
    public override void Exit()
    {
       
    }

}

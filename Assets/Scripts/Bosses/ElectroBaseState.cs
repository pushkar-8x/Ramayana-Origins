using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElectroBaseState : State
{
    protected ElectroStateMachine stateMachine;
    public ElectroBaseState(ElectroStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected bool IsInChaseRange()
    {
        if (stateMachine.Player.IsDead) { return false; }

        float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.DetectionRange * stateMachine.DetectionRange;
    }
}

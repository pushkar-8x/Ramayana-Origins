using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    private readonly int DashHash = Animator.StringToHash("Dash");
    public PlayerDashState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    private const float CrossFadeDuration = 0.1f;

    private float remainingDashTime;

    public override void Enter()
    {
        Debug.Log("InDash");
        remainingDashTime = stateMachine.DashDuration;
        stateMachine.Animator.CrossFadeInFixedTime(DashHash, CrossFadeDuration);
    }


    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();
        movement += stateMachine.transform.forward * (stateMachine.DashLength / stateMachine.DashDuration);

        Move(movement, deltaTime);

        remainingDashTime -= deltaTime;

        if(remainingDashTime <=0f)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
        Debug.Log("Dashing..");
    }


    public override void Exit()
    {
        
    }

    
}

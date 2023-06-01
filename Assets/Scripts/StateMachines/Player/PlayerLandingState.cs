using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingState : PlayerBaseState
{
    private readonly int LandHash = Animator.StringToHash("Landing");
    private const float CrossFadeDuration = 0.1f;
    public PlayerLandingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private float remainingLandTime;
    public override void Enter()
    {
        remainingLandTime = stateMachine.LandDuration;
        stateMachine.Animator.CrossFadeInFixedTime(LandHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        remainingLandTime -= deltaTime;
        if(remainingLandTime<=0f)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {
       
    }

    
}

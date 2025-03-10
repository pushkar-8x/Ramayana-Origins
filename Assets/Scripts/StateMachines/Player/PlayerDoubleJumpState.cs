using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJumpState : PlayerBaseState
{

    private readonly int DoubleJumpHash = Animator.StringToHash("DoubleJump");

    private Vector3 momentum;

    private const float CrossFadeDuration = 0.1f;
    public PlayerDoubleJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    

    public override void Enter()
    {
        stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);

        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;

        stateMachine.Animator.CrossFadeInFixedTime(DoubleJumpHash, CrossFadeDuration);

        
    }


    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);

        if (stateMachine.Controller.velocity.y <= 0)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }

        FaceTarget();
    }
    public override void Exit()
    {
       
    }

    
}

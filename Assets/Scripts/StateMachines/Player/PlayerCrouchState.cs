using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
    private readonly int CrouchBlendTreeHash = Animator.StringToHash("CrouchBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    private Vector3 OriginalControllerCentre;
    private float OriginalControllerHeight;

    public PlayerCrouchState(PlayerStateMachine stateMachine , Vector3 centre , float height) : base(stateMachine)
    {
        OriginalControllerCentre = centre;
        OriginalControllerHeight = height;
    }
   

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(CrouchBlendTreeHash, CrossFadeDuration);
        AdjustColliders();

    }

    

    public override void Tick(float deltaTime)
    {
        if(!stateMachine.InputReader.IsCrouching)
        {
            ReturnToLocomotion();
            return;
        }

        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.CrouchMovementSpeed, deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);

        FaceMovementDirection(movement, deltaTime);


        
    }


    public override void Exit()
    {
        stateMachine.Controller.center = OriginalControllerCentre;
        stateMachine.Controller.height = OriginalControllerHeight;
    }


    private void AdjustColliders()
    {
        stateMachine.Controller.height /= 2f;
        stateMachine.Controller.center = new Vector3(OriginalControllerCentre.x, OriginalControllerCentre.y/2f, OriginalControllerCentre.z);
    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y +
            right * stateMachine.InputReader.MovementValue.x;
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * stateMachine.RotationDamping);
    }
}

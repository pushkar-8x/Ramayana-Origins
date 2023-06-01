using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState : PlayerBaseState
{

   
    private readonly int ShootHash = Animator.StringToHash("Shoot");
    private readonly int AimBlendTreeHash = Animator.StringToHash("AimBlendTree");
    private readonly int AimForwardHash = Animator.StringToHash("AimForward");
    private readonly int AimRightHash = Animator.StringToHash("AimRight");

    private const float CrossFadeDuration = 0.1f;



    public PlayerAimState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.WeaponHandler.ToggleSword(false); stateMachine.WeaponHandler.ToggleBow(true);
        stateMachine.InputReader.ShootEvent += Shoot;
        stateMachine.CrossHair.gameObject.SetActive(true);
        stateMachine.Animator.CrossFadeInFixedTime(AimBlendTreeHash, CrossFadeDuration);
    }

   

    public override void Tick(float deltaTime)
    {
        if(!stateMachine.InputReader.IsAiming)
        {
            ReturnToLocomotion();
        }

       

        Vector3 movement = CalculateMovement(deltaTime);

        Move(movement * stateMachine.AimMovementSpeed, deltaTime);

        UpdateAnimator(deltaTime);



        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Shoot");
        if(normalizedTime > 0.8f)
        {
            ReturnToLocomotion();
        }

        RotateTowardsAimPoint();

    }


    public override void Exit()
    {
        stateMachine.InputReader.ShootEvent -= Shoot;
        stateMachine.CrossHair.gameObject.SetActive(false);
        stateMachine.WeaponHandler.ToggleSword(true); stateMachine.WeaponHandler.ToggleBow(false);
    }

    private void Shoot()
    {
        Debug.Log("Shot");
        stateMachine.Animator.CrossFadeInFixedTime(ShootHash, CrossFadeDuration);
    }


    private Vector3 CalculateMovement(float deltaTime)
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

        return movement;
    }

    private void UpdateAnimator(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.y == 0)
        {
            stateMachine.Animator.SetFloat(AimForwardHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(AimForwardHash, value, 0.1f, deltaTime);
        }

        if (stateMachine.InputReader.MovementValue.x == 0)
        {
            stateMachine.Animator.SetFloat(AimRightHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(AimRightHash, value, 0.1f, deltaTime);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwingingState : PlayerBaseState
{
    private readonly int SwingHash = Animator.StringToHash("Swing");
    private const float CrossFadeDuration = 0.1f;

    private float maxSwingDistance = 25f;
    
    private Vector3 swingPoint;
    public PlayerSwingingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(SwingHash, CrossFadeDuration);
        StartSwing();

        Debug.Log("Entered swing");
    }

    

    public override void Tick(float deltaTime)
    {
        //Move(Vector3.zero);
        DrawRope();
        if (!stateMachine.InputReader.IsSwinging)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        }
        stateMachine.ForceReceiver.SetGravity(0);

    }

    public override void Exit()
    {
        StopSwing();
        Debug.Log("Exited swing");
        stateMachine.ForceReceiver.SetGravity(Physics.gravity.y);
    }
    

    private void StartSwing()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(stateMachine.MainCameraTransform.position, stateMachine.MainCameraTransform.forward,
                            out raycastHit, maxSwingDistance, stateMachine.whatIsGrappleable))
        {
            swingPoint = raycastHit.point;
            stateMachine.Joint = stateMachine.gameObject.AddComponent<SpringJoint>();
            stateMachine.Joint.autoConfigureConnectedAnchor = false;
            stateMachine.Joint.connectedAnchor = swingPoint;

            float distanceFromPoint = Vector3.Distance(stateMachine.transform.position, swingPoint);

            // the distance grapple will try to keep from grapple point. 
            stateMachine.Joint.maxDistance = distanceFromPoint * 0.8f;
            stateMachine.Joint.minDistance = distanceFromPoint * 0.25f;

            // customize values as you like
            stateMachine.Joint.spring = 4.5f;
            stateMachine.Joint.damper = 7f;
            stateMachine.Joint.massScale = 4.5f;

            stateMachine.RopeLineRenderer.positionCount = 2;
            currentGrapplePosition = stateMachine.RopeStartPoint.position;
        }


        
    }

    public void StopSwing()
    {
        

        stateMachine.RopeLineRenderer.positionCount = 0;
        stateMachine.ResetJoint();
    }

    private Vector3 currentGrapplePosition;

    private void DrawRope()
    {
        // if not grappling, don't draw rope
        if (!stateMachine.Joint) return;

        currentGrapplePosition =
            Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 8f);

        stateMachine.RopeLineRenderer.SetPosition(0, stateMachine.RopeStartPoint.position);
        stateMachine.RopeLineRenderer.SetPosition(1, currentGrapplePosition);
    }

}

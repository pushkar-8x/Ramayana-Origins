using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public bool IsAttacking { get; private set; }
    public bool IsBlocking { get; private set; }

    public bool IsSwinging { get; private set; }
    public bool IsSprinting { get; set; }

    public bool IsCrouching { get; private set; }
    public bool IsAiming { get; private set; }
    public Vector2 MovementValue { get; private set; }

    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action TargetEvent;
    public event Action DashEvent;
    public event Action ShootEvent;

    private Controls controls;

    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);

        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        DodgeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {

    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        TargetEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsAttacking = true;
            ShootEvent?.Invoke();
        }
        else if (context.canceled)
        {
            IsAttacking = false;
        }
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsBlocking = true;
            IsAiming = true;
        }
        else if (context.canceled)
        {
            IsBlocking = false;
            IsAiming = false;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
       // if (!context.performed) { return; }

        if (context.performed)
        {
            IsSprinting = true;
            

        }
        else if (context.canceled)
        {
            IsSprinting = false;
            

        }

        // DashEvent?.Invoke();

    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsCrouching = true;
            
        }
        else if (context.canceled)
        {
            IsCrouching = false;
            
        }
    }

    public void OnSwing(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsSwinging = true;
            Debug.Log("Swing pressed");


        }
        else if (context.canceled)
        {
            IsSwinging = false;
            Debug.Log("Swing cancelled");

        }

    }
}

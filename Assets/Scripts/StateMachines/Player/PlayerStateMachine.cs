using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
 
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public WeaponDamage Weapon { get; private set; }

    [field: SerializeField] public WeaponHandler WeaponHandler { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField] public LedgeDetector LedgeDetector { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }

    [field: SerializeField] public float SprintMovementSpeed { get; private set; }

    [field:SerializeField] public float MaxStamina { get; private set; }
    
    [field: SerializeField] public float TargetingMovementSpeed { get; private set; }

    [field: SerializeField] public float CrouchMovementSpeed { get; private set; }
    [field: SerializeField] public float AimMovementSpeed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public float DodgeDuration { get; private set; }

    [field: SerializeField] public float LandDuration { get; private set; }
    [field: SerializeField] public float DodgeLength { get; private set; }
    [field: SerializeField] public float DashDuration { get; private set; }
    [field: SerializeField] public float DashLength { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }


    [field: SerializeField] public float DoubleJumpForce { get; private set; }

    [field: SerializeField] public Transform CrossHair { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }

    [field: SerializeField] public Attack[] AirAttacks { get; private set; }


    [field: SerializeField] public float maxSwingDistance { get; private set; }

    [field: SerializeField] public LayerMask whatIsGrappleable { get; private set; }

    [field: SerializeField] public LineRenderer RopeLineRenderer { get; private set; }

    [field: SerializeField] public Transform RopeStartPoint { get; private set; }

    public SpringJoint Joint { get; set; }

    public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity;
    public Transform MainCameraTransform { get; private set; }

    public float CurrentStamina { get; set; }


    



    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CrossHair.gameObject.SetActive(false);

        MainCameraTransform = Camera.main.transform;

        CurrentStamina = MaxStamina;

        SwitchState(new PlayerFreeLookState(this));

        UIHandler.instance.FillStaminaUI(CurrentStamina, MaxStamina);
    }


    


    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
    }

    private void HandleTakeDamage()
    {
        SwitchState(new PlayerImpactState(this));
    }

    private void HandleDie()
    {
        SwitchState(new PlayerDeadState(this));
    }

    
    public void RegenerateStamina()
    {
        if(IsGeneratingStamina())
        CurrentStamina += Time.deltaTime;
        CurrentStamina = Mathf.Clamp(CurrentStamina,0,MaxStamina);
        FillStaminaUI();
        
    }

    public bool IsGeneratingStamina()
    {
        if (CurrentStamina == MaxStamina) return false;
        else return true;
    }

    public void FillStaminaUI()
    {
        UIHandler.instance.FillStaminaUI(CurrentStamina, MaxStamina);
    }

    public void ResetJoint()
    {
        Destroy(Joint);
    }
    
}

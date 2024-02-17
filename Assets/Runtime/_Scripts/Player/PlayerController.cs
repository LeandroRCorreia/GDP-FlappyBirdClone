using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{   
    [Header("Player Components")]
    
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerAudio playerAudio;
    [SerializeField] private PlayerAnimationController playerAnimation;
 
    public MovementParams CurrentMovementParams 
    {
        get {return movementParams;}

        set
        {
            if(!IsTouchedGround)
            {
                movementParams = value;
            }
        }
         
    }

    private MovementParams movementParams;

    public event Action playerDeathEvent;

    private Vector3 velocity;
    private float zRot;

    public bool IsDied {get; private set;} = false;
    public bool IsFlapped {get; private set;} = false;
    
    [Header("External Dependencies")]
    [SerializeField] private GameMode gameMode;

    public bool IsTouchedGround {get; private set;}

    void Update()
    {
        if (!IsDied)
        {
            ProcessInput();
        }
        ModifyVelocity();
        RotateDown();

        Vector3 targetPosition = transform.position + velocity * Time.deltaTime;
        Quaternion targetRotation = Quaternion.Euler(0, 0, zRot);
        transform.SetPositionAndRotation(targetPosition, targetRotation);
    }

    private void ProcessInput()
    {
        if (playerInput.Tap())
        {
            ProcessFlap();
        }
    }

    private void ModifyVelocity()
    {
        velocity.x = CurrentMovementParams.ForwardSpeed;
        velocity.y -= CurrentMovementParams.Gravity * Time.deltaTime;
    }

    private void ProcessFlap()
    {
        velocity.y = CurrentMovementParams.UpSpeed;
        zRot = CurrentMovementParams.FlapAngleDegress;
        IsFlapped = true;
        playerAudio.PlayFlapAudio();
    }

    private void RotateDown()
    {
        if (velocity.y < 0)
        {
            zRot -= CurrentMovementParams.RotateDownSpeed * Time.deltaTime;
            zRot = Mathf.Max(-90, zRot);
        }
    }

    public void StopImediatelly()
    {
        velocity = Vector2.zero;

    }

    public void Die()
    {
        if(IsDied) return;
        IsDied = true;
        StopImediatelly();
        playerDeathEvent?.Invoke();

    }

    public void OnHitGround()
    {
        IsTouchedGround = true;
        StopImediatelly();

    }

}

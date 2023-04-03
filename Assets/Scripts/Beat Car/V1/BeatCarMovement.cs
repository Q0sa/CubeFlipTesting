using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

//=====
// OBS THIS CODE IS NON FUNCTIONAL OR HAS BEEN ABANDONED
//=====

public class BeatCarMovement : MonoBehaviour
{

    //OBS this is currently designed for Keyboard input only
    
    [SerializeField]private float turnSpeed = 25.0f;
    [SerializeField] private float accelSpeed = 10.0f;


    private Controls playerInput;
    private InputAction movementInput;
    private InputAction accelerationInput;
 
    private enum TurnDirection
    {

        None,
        Left,
        Right,

    }

    private enum AccelDirection
    {

        Forward,
        Backward

    }

    private TurnDirection turnDirection = TurnDirection.None;
    private AccelDirection accelDirection = AccelDirection.Forward;

    private Rigidbody rb;


    private void Awake()
    {
        playerInput = new Controls();
        rb = GetComponent<Rigidbody>();

    }

    private void OnEnable()
    {
        movementInput = playerInput.BeatCar.Movement;
        accelerationInput = playerInput.BeatCar.BeatHit;

        movementInput.Enable();
        movementInput.performed += OnMovementInput;
        movementInput.canceled += OnMovementCancel;

        accelerationInput.Enable();
        accelerationInput.performed += OnAccelerationInput; 

    }

    private void OnDisable()
    {
        movementInput.Disable();
        movementInput.performed -= OnMovementInput;
        movementInput.canceled -= OnMovementCancel;

        accelerationInput.Disable();
        accelerationInput.performed -= OnAccelerationInput;
    }

    private void OnAccelerationInput(InputAction.CallbackContext context)
    {


        rb.AddForce(transform.forward * accelSpeed, ForceMode.Impulse);

    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {

        Vector2 inputValue = context.ReadValue<Vector2>();

        turnDirection = inputValue.x == 0 ? TurnDirection.None : 
                        inputValue.x > 0  ? TurnDirection.Right : TurnDirection.Left; //Double Ternary check to make sure that 0 doesnt go always to left

        accelDirection = inputValue.y >= 0 ? AccelDirection.Forward : AccelDirection.Backward; //Will always be Forward unless input y goes negative

    }

    private void OnMovementCancel(InputAction.CallbackContext context)
    {
        
        //Resets to default
        turnDirection = TurnDirection.None;
        accelDirection = AccelDirection.Forward; 

    }

    void FixedUpdate()
    {

        if(rb.velocity.normalized.magnitude > 0) 
            TurnMovement();

    }

    private void TurnMovement()
    {

        switch (turnDirection)
        {
            case TurnDirection.None:

                break;

            case TurnDirection.Left:

                rb.AddTorque(new Vector3(rb.angularVelocity.x, -turnSpeed, rb.angularVelocity.z), ForceMode.Force);
                break;

            case TurnDirection.Right:

                rb.AddTorque(new Vector3(rb.angularVelocity.x, turnSpeed, rb.angularVelocity.z), ForceMode.Force);
                break;

            default:
                break;
        }

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BeatCarMovement : MonoBehaviour
{

    //OBS this is currently designed for Keyboard input only

    private Controls playerInput;
    private InputAction movementInput;

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

    private void Awake()
    {
        playerInput = new Controls();
    }

    private void OnEnable()
    {
        movementInput = playerInput.BeatCar.Movement;

        movementInput.Enable();
        movementInput.performed += OnMovementInput;
        movementInput.canceled += OnMovementCancel;

    }

    private void OnDisable()
    {
        movementInput.Disable();
        movementInput.performed -= OnMovementInput;
        movementInput.canceled -= OnMovementCancel;
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

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
        Debug.Log(turnDirection.ToString() + "\n" + accelDirection.ToString() );

    }
}

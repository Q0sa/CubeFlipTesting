using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BeatCarSteeringV2 : MonoBehaviour
{

    [SerializeField] private int maxAngleFromZero = 75;

    private float steering = 0.0f;
    private float steerTime = 0.0f;
    private Controls playerInput;
    private InputAction steeringAction;

    void Awake()
    {
        
        playerInput = new Controls();

    }

    private void OnEnable()
    {

        steeringAction = playerInput.BeatCar.Movement;
        steeringAction.Enable();

        steeringAction.performed += Steer;
        steeringAction.canceled += StopSteer;
        

    }

    private void OnDisable()
    {

        steeringAction.Disable();
        steeringAction.performed -= Steer;
        steeringAction.canceled -= StopSteer;

    }

    private void Steer(InputAction.CallbackContext context) {
        //needs to be translated over time so that there is actual "grip" being applied
        steering = context.ReadValue<float>();
        //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + (steering * maxAngleFromZero), 0) ;
    
    }

    private void StopSteer(InputAction.CallbackContext context){

        steering = context.ReadValue<float>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //needs to be translated over time so that there is actual "grip" being applied
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(transform.eulerAngles.x, 
                                                                                transform.eulerAngles.y + (steering * maxAngleFromZero), 
                                                                                transform.eulerAngles.z), 
                                                                                steerTime);

        steerTime += Time.fixedDeltaTime * 10f;

    }

    private void Update()
    {
        


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BeatCarAcceleration : MonoBehaviour
{

    [SerializeField] float accelerationAmount = 10f;

    private Controls playerInput;
    private InputAction accelerationInput;

    private Rigidbody rb;


    private void Awake()
    {
        
        playerInput = new Controls(); 

        rb = GetComponent<Rigidbody>();

    }
    // Start is called before the first frame update
    void OnEnable()
    {

        accelerationInput = playerInput.BeatCar.BeatHit;
        accelerationInput.Enable();
        accelerationInput.performed += OnAccelerationInput;

    }

    private void OnDisable()
    {
        accelerationInput.Disable();
        accelerationInput.performed -= OnAccelerationInput;
    }

    void OnAccelerationInput (InputAction.CallbackContext context)
    {

        rb.AddForce(transform.GetChild(0).forward * accelerationAmount, ForceMode.Acceleration);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnDrawGizmosSelected()
    //{
        
    //    Gizmos.color = Color.green;

    //    Gizmos.DrawWireSphere(transform.position, .5f);

    //}
}

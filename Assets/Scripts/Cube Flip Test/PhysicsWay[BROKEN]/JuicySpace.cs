using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//=====
// OBS THIS CODE IS NON FUNCTIONAL OR HAS BEEN ABANDONED
//=====

public class JuicySpace : MonoBehaviour
{



    [SerializeField] private float UpRotationForce = 1f;
    //[SerializeField] private float DownRotationForce = 1f;
    //[SerializeField] private float LeftRotationForce = 1f;
    //[SerializeField] private float RightRotationForce = 1f;

    Rigidbody rb;

    Controls playerInput;
    private InputAction spaceHit;

    private void Awake()
    {
        playerInput = new Controls();
    }
    
    private void Start()
    {
      
        rb = GetComponent<Rigidbody>();

    }

    private void OnEnable()
    {

        spaceHit = playerInput.CubeFlip.Flip;
        spaceHit.Enable();
        spaceHit.performed += SlapCube;

    }

    private void OnDisable()
    {
        spaceHit.Disable();
    }

    private void SlapCube(InputAction.CallbackContext context)
    {

        rb.AddRelativeTorque(UpRotationForce * Vector3.right);

    }

}

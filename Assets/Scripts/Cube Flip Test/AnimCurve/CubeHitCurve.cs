using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static System.TimeZoneInfo;

public class CubeHitCurve : MonoBehaviour
{
    [SerializeField] private RotationStages[] rotationStages;

    private Quaternion startRotation;
    private Quaternion targetRotation;

    private int currentStage = 0;
    private float step = 0f;

    private bool impulseAdded = true;

    private Rigidbody rb;

    private Controls playerInput;
    private InputAction spaceHit;

    [System.Serializable]
    public struct RotationStages
    {

        [SerializeField] public float speed;
        [SerializeField] public Vector3 rotationTarget;
        [Space]
        [SerializeField] public float impulseForce;
        [SerializeField] public Vector3 impulseDirection;

    }  


    private void Awake(){

        playerInput = new Controls();
       
    }

    void Start(){
        rb = GetComponent<Rigidbody>();

        targetRotation = transform.rotation;
        startRotation = transform.rotation;

    }

    private void OnEnable()
    {

        spaceHit = playerInput.CubeFlip.Flip;
        spaceHit.Enable();
        spaceHit.performed += NextStage;

    }

    private void OnDisable()
    {
        spaceHit.Disable();
    }

    private void Update()
    {

        CubeRotation();

    }

    private void CubeRotation()
    {
        
        if (transform.rotation != targetRotation) {

            transform.rotation = Quaternion.RotateTowards(startRotation, targetRotation, step);
            UpdateRotateStep();

        } else if (!impulseAdded) {

            ApplyRotationStageTorque(rotationStages[currentStage].impulseDirection.normalized, rotationStages[currentStage].impulseForce);
            impulseAdded = true;

        }


    }

    private void UpdateRotateStep()
    {
        step += Time.fixedDeltaTime * rotationStages[currentStage].speed;
    }

    private void ApplyRotationStageTorque(Vector3 direction, float torqueForce)
    {
        rb.AddTorque(direction * torqueForce, ForceMode.Impulse);
        step = 0f;
    }

    private void NextStage(InputAction.CallbackContext context)
    {

        startRotation = targetRotation;
        targetRotation = Quaternion.Euler(rotationStages[currentStage].rotationTarget);
       
        currentStage = (currentStage + 1) % rotationStages.Length;

        step = 0f;
        impulseAdded = false;

    }
}

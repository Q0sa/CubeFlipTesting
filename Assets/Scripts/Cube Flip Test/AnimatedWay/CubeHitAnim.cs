using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeHitAnim : MonoBehaviour
{

    private Controls playerInput;
    private InputAction spaceHit;
    private Animator animator;

    private void Awake()
    {
        playerInput = new Controls();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        spaceHit = playerInput.CubeFlip.Flip;
        spaceHit.Enable();
        spaceHit.performed += TriggerNextAnim;
    }

    private void OnDisable()
    {
        spaceHit.Disable();
        spaceHit.performed -= TriggerNextAnim;

    }

    private void Update()
    {
        if(shouldStartAnim())
            animator.SetBool("isSpaceHit", false);
    }

    private bool shouldStartAnim() 
    {
        return animator.IsInTransition(0) && animator.GetBool("isSpaceHit") == true;
    }

    private void TriggerNextAnim(InputAction.CallbackContext context) 
    {
        animator.SetBool("isSpaceHit", true);
    }

}

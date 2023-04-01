using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeLookSettings : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineFreeLook activeFreeLook;

    [Space]
    [Header("Camera Options")]
    [Range(50f, 100f)]
    [SerializeField] private float FOV = 90f;
    [Range(2f, 10f)]
    [SerializeField] private float CameraDistance = 5f;



    [Space(4f)]
    [Header("Look Options")]
    [Range(0.0f, 5.0f)]
    [SerializeField] private float XSensitivity = 1f;
    [SerializeField] private bool InverseLookDirectionX = false;
    [Space(2.5f)]
    [Range(0.0f, 5.0f)]
    [SerializeField] private float YSensitivity = 1f;
    [SerializeField] private bool InverseLookDirectionY = false;

 

    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
     
        activeFreeLook.m_Lens.FieldOfView = 90f;
        activeFreeLook.m_Orbits[0].m_Radius = CameraDistance;
        activeFreeLook.m_Orbits[1].m_Radius = CameraDistance;
        activeFreeLook.m_Orbits[2].m_Radius = CameraDistance;

        activeFreeLook.m_XAxis.m_MaxSpeed *= XSensitivity;
        activeFreeLook.m_YAxis.m_MaxSpeed *= YSensitivity;
        
        activeFreeLook.m_XAxis.m_InvertInput = InverseLookDirectionX;
        activeFreeLook.m_YAxis.m_InvertInput = InverseLookDirectionY;
        
        
    }

}

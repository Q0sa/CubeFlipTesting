using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCube : MonoBehaviour
{


    [SerializeField] float stabalisationSpeed = 1.0f;

    private Transform[] cubeFaceTransf;
    private Transform camTransf;

    private float[] axisAngles;

    private Rigidbody rb;

    private void Start()
    {

        cubeFaceTransf = new Transform[6];

        cubeFaceTransf[0] = transform.GetChild(0);
        cubeFaceTransf[1] = transform.GetChild(1);
        cubeFaceTransf[2] = transform.GetChild(2);
        cubeFaceTransf[3] = transform.GetChild(3);
        cubeFaceTransf[4] = transform.GetChild(4);
        cubeFaceTransf[5] = transform.GetChild(5);

        axisAngles = new float[5];

        axisAngles[0] = 0f;
        axisAngles[1] = 90f;
        axisAngles[2] = 180f;
        axisAngles[3] = 270f;
        axisAngles[4] = 360f;

        rb = GetComponent<Rigidbody>();
        camTransf = GameObject.FindGameObjectWithTag("MainCamera").transform;

    }

    private void Update()
    {
        if (rb.angularVelocity.magnitude < 0.005f && rb.angularVelocity.magnitude > -0.005f) 
            StabaliseToAxis();

    }
    private void FixedUpdate()
    {

        Debug.Log("Nearest Face:" + GetNearestFace().name);
   
    }

    private void StabaliseToAxis()
    {

        //float step = stabalisationSpeed * Time.fixedDeltaTime;    
        
        transform.LookAt(camTransf.position);

    }

    //private float GetNearestAngle(float rotation)
    //{
    //    rotation -= 180f;

    //    if (rotation != 0.0f)
    //    {
    //        float smallestDifference = 9999999999999f;
    //        float closestAngle = 0;

    //        float modRotation = rotation >= 0 ? rotation : rotation + 180f;

    //        foreach (float angle in axisAngles)
    //        {

    //            float diff = Mathf.Abs(modRotation - angle);

    //            if (diff < smallestDifference)
    //            {

    //                smallestDifference = diff;
    //                closestAngle = angle;

    //            }

    //        }

    //        return closestAngle;
    //    }

    //    return rotation;

    //}

    private Transform GetNearestFace()
    {
        Transform closestFace = transform.GetChild(5);

        float currDist = 0f;
        float closestDist = 9999999999999f; 

        foreach (Transform cubeFace in cubeFaceTransf){

            currDist = Vector3.Distance(camTransf.position, cubeFace.position);

            if (currDist < closestDist){

                closestFace = cubeFace;
                closestDist = currDist;

            }
                
        }

        return closestFace;

    }
   
    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, transform.GetChild(0).position);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.GetChild(1).position);
        Gizmos.DrawLine(transform.position, transform.GetChild(2).position);
        Gizmos.DrawLine(transform.position, transform.GetChild(3).position);
        Gizmos.DrawLine(transform.position, transform.GetChild(4).position);
        Gizmos.DrawLine(transform.position, transform.GetChild(5).position);


    }
}

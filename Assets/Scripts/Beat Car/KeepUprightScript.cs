using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepUprightScript : MonoBehaviour
{
    
    private RaycastHit groundPlane;
    
    void Update()
    {

        Physics.Raycast(transform.position, Vector3.down, out groundPlane, 2.0f);


        transform.up = groundPlane.normal;
        transform.rotation = transform.parent.rotation;

    }

}

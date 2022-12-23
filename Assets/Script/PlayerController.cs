using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody pr;
    public Transform tr;
    public float propulsionForce = 10.0f;
    public float walkForce = 5.0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pr.AddForce(tr.forward * -propulsionForce, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.Z))
        {
            pr.AddForce(tr.rotation * Vector3.forward * walkForce, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.S))
        {
            pr.AddForce(tr.rotation * Vector3.back * walkForce, ForceMode.Acceleration);
        }
    }
}

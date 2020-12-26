using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    Rigidbody rb;
    public Rigidbody Rb { get { return (rb == null) ? GetComponent<Rigidbody>() : rb; } }
    void Start()
    {
        Rb.AddForce(Physics.gravity * 1.5f);
        Rb.AddForce(Vector3.up * 7.5f, ForceMode.Impulse);
    }

   
}

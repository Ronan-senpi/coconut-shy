using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public Rigidbody Rb { get; set; }
    public SphereCollider Col { get; set; }
    public Vector3 Pos { get { return transform.position; } }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        Col = GetComponent<SphereCollider>();
    }

    public void Push(Vector3 force)
    {
        Rb.AddForce(force, ForceMode.Impulse);
    }
    public void ActivateRb()
    {
        Rb.isKinematic = false;
    }

    public void DesactivateRb()
    {
        Rb.velocity = Vector3.zero;
        Rb.angularVelocity = Vector3.zero;
        Rb.isKinematic = true;

    }
}

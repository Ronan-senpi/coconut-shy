using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public Rigidbody Rb { get; set; }
    public BoxCollider Col { get; set; }
    public Vector3 Pos { get { return transform.position; } }
    protected MeshRenderer mr;
    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        mr.enabled = false;
        Rb = GetComponent<Rigidbody>();
        Col = GetComponent<BoxCollider>();
    }
    private void Start()
    {
        StartCoroutine(ActiveMeshRenderer());
    }
    IEnumerator ActiveMeshRenderer()
    {
        yield return new WaitForSeconds(1.6f);
        mr.enabled = true;
    }
    public void Push(Vector3 force)
    {
        ActivateRb();
        transform.parent = null;
        Rb.AddForce(force, ForceMode.Impulse);
    }
    public void ActivateRb()
    {
        Rb.isKinematic = false;
    }

    public void DesactivateRb()
    {
        //Rb.velocity = Vector3.zero;
        //Rb.angularVelocity = Vector3.zero;
        Rb.isKinematic = true;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movimiento2 : MonoBehaviour
{
    public float rapidezMaxima;
    public float aceleracion;
    public float rapidezRotacion;
    private Vector3 direccion {
        get {
            float direccionX = Input.GetAxisRaw("Horizontal");
            float direccionZ = Input.GetAxisRaw("Vertical");

            return new Vector3(direccionX, 0f, direccionZ);
        }
    }
    public Vector3 velocidadFinal;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (direccion.sqrMagnitude > 1)
        {
            velocidadFinal = Vector3.MoveTowards(velocidadFinal, direccion.normalized, aceleracion * Time.deltaTime);
        } else
        {
            velocidadFinal = Vector3.MoveTowards(velocidadFinal, direccion, aceleracion * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = velocidadFinal * rapidezMaxima;

        if (direccion != Vector3.zero)
        {
            rb.rotation = Quaternion.RotateTowards(rb.rotation, Quaternion.LookRotation(direccion), rapidezRotacion * Time.deltaTime);
        }
    }
}

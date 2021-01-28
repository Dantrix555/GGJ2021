using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovimientoPersonaje : MonoBehaviour
{

    public float rapidezMaxima = 5f;
    public float rapidezActual
    {
        get
        {
            //Genera el efecto de aceleración y desaceleración (Se modifica cambiando los ajustes de los ejes)
            return Mathf.Max(Mathf.Abs(direccion.x), Mathf.Abs(direccion.z)) * rapidezMaxima;
        }
    }

    public Vector3 direccion
    {
        get
        {
            //Lo de siempre
            float direccionX = Input.GetAxis("Horizontal");
            float direccionZ = Input.GetAxis("Vertical");

            return new Vector3(direccionX, 0f, direccionZ);
        }
    }

    public Vector3 velocidad
    {
        get
        {
            //Tambien lo de siempre
            return direccion.normalized * rapidezActual;
        }
    }

    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        rb.velocity = velocidad;
        if (direccion != Vector3.zero)
        {
            rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(direccion), 0.1f);
        }
    }
}

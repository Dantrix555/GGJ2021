using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    public Transform objetivo;
    public Transform camara;
    public float suavizado;
    Vector3 posicionFinal;
    Vector3 velocidad = Vector3.zero;

    private void Awake()
    {
        camara = transform.GetChild(0);
    }
    private void Update()
    {
        posicionFinal = Vector3.SmoothDamp(transform.position, objetivo.position, ref velocidad, suavizado);
    }
    private void LateUpdate()
    {
        transform.position = posicionFinal;
        camara.LookAt(objetivo);
    }
}

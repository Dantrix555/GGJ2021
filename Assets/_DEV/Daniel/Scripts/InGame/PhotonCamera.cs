using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonCamera : MonoBehaviour
{
    private Transform objetivo;
    private bool isFollowingPlayer = false;

    public Transform camara;
    public float suavizado;
    Vector3 posicionFinal;
    Vector3 velocidad = Vector3.zero;

    public void SetPlayerTransform(Transform newPlayerTransform)
    {
        objetivo = newPlayerTransform;
        IsFollowingPlayer(true);
    }

    public void IsFollowingPlayer(bool follow)
    {
        isFollowingPlayer = follow;
    }

    private void Awake()
    {
        camara = transform.GetChild(0);
    }

    private void Update()
    {
        if(isFollowingPlayer)
            posicionFinal = Vector3.SmoothDamp(transform.position, objetivo.position, ref velocidad, suavizado);
    }

    private void LateUpdate()
    {
        if(isFollowingPlayer)
        {
            transform.position = posicionFinal;
            camara.LookAt(objetivo);
        }
    }
}

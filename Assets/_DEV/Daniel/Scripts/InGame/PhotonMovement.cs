using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody))]
public class PhotonMovement : MonoBehaviourPun
{
    public float rapidezMaxima;
    public float aceleracion;
    public float rapidezRotacion;
    private Vector3 direccion
    {
        get
        {
            float direccionX = Input.GetAxisRaw("Horizontal");
            float direccionZ = Input.GetAxisRaw("Vertical");

            return new Vector3(direccionX, 0f, direccionZ);
        }
    }
    public Vector3 velocidadFinal;
    private Rigidbody rb;
    private PhotonCamera cameraReference;

    private bool _canMove = true;
    public bool CanMove { get => _canMove; set => _canMove = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if(photonView.IsMine)
        {
            InGameSingleton.SetCachedPlayerController(this);
            cameraReference = GameObject.Find("Objetivo Camara").GetComponent<PhotonCamera>();
            cameraReference.SetPlayerTransform(transform);
        }
    }
    private void Update()
    {
        if(photonView.IsMine && CanMove)
        {
            if (direccion.sqrMagnitude > 1)
            {
                velocidadFinal = Vector3.MoveTowards(velocidadFinal, direccion.normalized, aceleracion * Time.deltaTime);
            }
            else
            {
                velocidadFinal = Vector3.MoveTowards(velocidadFinal, direccion, aceleracion * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate()
    {
        if(photonView.IsMine && CanMove)
        {
            rb.velocity = new Vector3(velocidadFinal.x * rapidezMaxima, rb.velocity.y, velocidadFinal.z * rapidezMaxima);

            if (direccion != Vector3.zero)
            {
                rb.rotation = Quaternion.RotateTowards(rb.rotation, Quaternion.LookRotation(direccion), rapidezRotacion * Time.deltaTime);
            }
        }
    }

    public void BlockMove()
    {
        photonView.RPC("BlockMovement", RpcTarget.All);
    }

    private void BlockMovement()
    {
        StartCoroutine(DisableMoveTemporally());
    }

    [PunRPC]
    private IEnumerator DisableMoveTemporally()
    {
        CanMove = false;
        yield return new WaitForSeconds(3f);
        CanMove = true;
    }

    public void SetStartPosition()
    {
        photonView.RPC("SetPosition", RpcTarget.All);
    }

    [PunRPC]
    public void SetPosition()
    {
        List<string> playerList = PhotonSingleton.GetPlayersInRoomNames();

        for (int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i] == PhotonNetwork.NickName)
            {
                transform.position = InGameSingleton.NewPlayerPosition(i);
                break;
            }
        }
    }

    public void BlockPlayer()
    {
        cameraReference.IsFollowingPlayer(false);
        CanMove = false;
    }

    public void LeaveGame()
    {
        PhotonSingleton.LeaveRoom();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(Rigidbody))]
public class PhotonMovement : MonoBehaviourPun
{
    public float rapidezMaxima;
    public float aceleracion;
    public float rapidezRotacion;
    private Vector3 _direccion
    {
        get
        {
            float direccionX = Input.GetAxisRaw("Horizontal");
            float direccionZ = Input.GetAxisRaw("Vertical");

            return new Vector3(direccionX, 0f, direccionZ);
        }
    }
    public Vector3 velocidadFinal;
    [Space(5)]
    [Header("Character Mesh Reference")]
    [SerializeField] private SkinnedMeshRenderer _characterRenderer;

    private Rigidbody _rb;
    private PhotonCamera _cameraReference;
    private string _cachedMaterialPath;

    private bool _canMove = true;
    public bool CanMove { get => _canMove; set => _canMove = value; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        if(photonView.IsMine)
        {
            InGameSingleton.SetCachedPlayerController(this);
            _cameraReference = GameObject.Find("Objetivo Camara").GetComponent<PhotonCamera>();
            _cameraReference.SetPlayerTransform(transform);
        }
    }
    private void Update()
    {
        if(photonView.IsMine && CanMove)
        {
            if (_direccion.sqrMagnitude > 1)
            {
                velocidadFinal = Vector3.MoveTowards(velocidadFinal, _direccion.normalized, aceleracion * Time.deltaTime);
            }
            else
            {
                velocidadFinal = Vector3.MoveTowards(velocidadFinal, _direccion, aceleracion * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate()
    {
        if(photonView.IsMine && CanMove)
        {
            _rb.velocity = new Vector3(velocidadFinal.x * rapidezMaxima, _rb.velocity.y, velocidadFinal.z * rapidezMaxima);

            if (_direccion != Vector3.zero)
            {
                _rb.rotation = Quaternion.RotateTowards(_rb.rotation, Quaternion.LookRotation(_direccion), rapidezRotacion * Time.deltaTime);
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
                if(photonView.IsMine)
                    _cachedMaterialPath = "SalesmanMaterials/Character_Vendedor" + i;

                transform.position = InGameSingleton.NewPlayerPosition(i);
                break;
            }
        }

        InGameSingleton.TimeInSeconds = 10;
    }

    public void SetNewMaterial()
    {
        photonView.RPC("SetMaterial", RpcTarget.All, _cachedMaterialPath);
    }

    [PunRPC]
    public void SetMaterial(string materialPath)
    {
        _characterRenderer.material = Resources.Load(materialPath) as Material;
    }

    public void BlockPlayer()
    {
        _cameraReference.IsFollowingPlayer(false);
        CanMove = false;
        _rb.velocity = Vector3.zero;
    }

    public void LeaveGame()
    {
        PhotonSingleton.LeaveRoom();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonCollectable : PhotonInteractable
{
    [SerializeField] private PhotonView _photonView;
    public PhotonView PhotonView => _photonView;

    protected override void EventManager_OnInteraction(PhotonInteractor sender, PhotonInteractable interactable)
    {
        if (interactable == this)
        {
            DestroyInteractable();
        }
    }

    public void DestroyInteractable()
    {
        _photonView.RPC("DestroyGameObject", RpcTarget.All);
    }

    [PunRPC]
    public void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }

}

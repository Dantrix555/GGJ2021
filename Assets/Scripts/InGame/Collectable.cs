using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Collectable : Interactable
{
    [SerializeField] private PhotonView photonView;
    public enum Description 
    {
        Claro,
        Oscuro,
        Duro,
        Suave
    }

    public string collectableName;
    public List<Description> descriptions;

    protected override void OnInteraction(Interactor sender, Interactable interactable)
    {
        Player player = (Player) sender;
        if (player.currentCollectable != null)
        {
            //player.DropCollectable();
        }

        player.currentCollectable = this;
        photonView.RPC("RemoveCollectable", RpcTarget.All);
    }

    public void RemoveCollectable() {
        gameObject.SetActive(false);
    }
}

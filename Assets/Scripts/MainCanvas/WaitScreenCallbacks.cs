using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

[RequireComponent(typeof(PlayersWaitScreen))]
public class WaitScreenCallbacks : MonoBehaviourPunCallbacks
{
    private Text[] _playerNames = default;
    public Text[] PlayerNames { get => _playerNames; set => _playerNames = value; }

    private Button _startGameButton = default;
    public Button StartGameButton { get => _startGameButton; set => _startGameButton = value; }

    [SerializeField] private PhotonView _photonView = default;

    // Update is called once per frame
    void Update()
    {
        _photonView.RPC("UpdateRoomInfo", RpcTarget.All);
    }

    [PunRPC]
    public void UpdateRoomInfo()
    {
        List<string> playersNames = PhotonSingleton.GetPlayersInRoomNames();

        //Add new player names
        for (int i = 0; i < playersNames.Count; i++)
        {
            PlayerNames[i].gameObject.SetActive(true);
            PlayerNames[i].text = playersNames[i];
        }

        //Remove names of the players who lefts the room
        for (int i = playersNames.Count; i < PlayerNames.Length; i++)
        {
            PlayerNames[i].text = "";
            PlayerNames[i].gameObject.SetActive(false);
        }

        if (PhotonNetwork.IsMasterClient)
        {
            _startGameButton.gameObject.SetActive(true);
            _startGameButton.interactable = PhotonSingleton.CanGameStart();
        }

    }

    public void CleanRPCBuffer()
    {
        PhotonNetwork.OpCleanRpcBuffer(_photonView);
    }
}

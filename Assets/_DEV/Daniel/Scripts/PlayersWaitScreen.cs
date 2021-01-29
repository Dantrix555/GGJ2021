using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersWaitScreen : PanelBase
{
    //Specific room callbacks references
    //[SerializeField] private RoomInfoPanelCallbacks _roomInfoPanelCallbacks = default;
    [SerializeField] private Text[] _playerNames = default;
    [SerializeField] private Button _goBackButton = default;
    [SerializeField] private Button _startGameButton = default;

    public override void OnPanelActive()
    {
        //Clear the players name text
        for (int i = 0; i < _playerNames.Length; i++)
        {
            _playerNames[i].text = "";
        }

        _startGameButton.interactable = false;
        _startGameButton.gameObject.SetActive(false);

        //_roomInfoPanelCallbacks.gameObject.SetActive(false);

        _goBackButton.onClick.AddListener(GoToMainMenu);
        _startGameButton.onClick.AddListener(StartGame);
    }

    public void GoToMainMenu()
    {
        //Clear RPC buffer and leaves the room
        //_roomInfoPanelCallbacks.CleanRPCBuffer();
        //PhotonSingleton.LeaveRoom();
        //MainCanvasReference.SetActiveNewPanel(MainCanvasReference.MainMenuScreen);
    }

    public void StartGame()
    {
        //_roomInfoPanelCallbacks.CleanRPCBuffer();
        //PhotonSingleton.SetRoomVisible(false);
        //PhotonSingleton.LoadLevel(PhotonSingleton.Level.InGame);
    }

    public override void OnPanelStart()
    {
        //_roomInfoPanelCallbacks.PlayerNames = _playerNames;
        //_roomInfoPanelCallbacks.StartGameButton = _startGameButton;
    }
}

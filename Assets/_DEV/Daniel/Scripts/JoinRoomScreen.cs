using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinRoomScreen : PanelBase
{
    [SerializeField] private InputField _roomCodeInputField = default;
    [SerializeField] private Button _joinRoomButton = default;
    [SerializeField] private Button _goBackButton = default;

    public override void OnPanelActive()
    {
        _roomCodeInputField.onValueChanged.AddListener(SetJoinButtonActive);
        _joinRoomButton.onClick.AddListener(JoinRoom);
        _goBackButton.onClick.AddListener(GoToMainMenu);
    }

    public void SetJoinButtonActive(string roomName)
    {
        _joinRoomButton.interactable = roomName.Length >= 3;
    }

    public void JoinRoom()
    {
        string roomName = _roomCodeInputField.text;

        if (roomName.Length < 3) { Debug.LogError("The room names have at least 3 characters"); return; }

        //Check if room exists
        if (PhotonSingleton.RoomExists(roomName))
        {
            PhotonSingleton.JoinRoom(roomName);
            MainCanvasReference.SetActiveNewPanel(MainCanvasReference.PlayerWaitScreen);
        }
        else
        {
            Debug.LogError("Sorry the room doesn't exists");
        }
    }

    public void GoToMainMenu()
    {
        MainCanvasReference.SetActiveNewPanel(MainCanvasReference.MainMenuScreen);
    }


    public override void OnPanelStart()
    {
        _roomCodeInputField.text = "";
        _joinRoomButton.interactable = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateRoomScreen : PanelBase
{
    [SerializeField] private InputField _roomNameInputField = default;
    [SerializeField] private Button _createRoomButton = default;
    [SerializeField] private Button _goBackButton = default;

    public override void OnPanelActive()
    {
        _roomNameInputField.onValueChanged.AddListener(SetCreateButtonActive);
        _createRoomButton.onClick.AddListener(CreateRoom);
        _goBackButton.onClick.AddListener(GoToMainMenu);
    }

    public void SetCreateButtonActive(string roomName)
    {
        _createRoomButton.interactable = roomName.Length >= 3;
    }

    public void CreateRoom()
    {
        string roomName = _roomNameInputField.text;

        if (roomName.Length < 3) { Debug.LogError("Trying to create room with a name with length less than 3 characters"); return; }

        //Check if room exists
        if (!PhotonSingleton.RoomExists(roomName))
        {
            PhotonSingleton.CreateRoom(roomName);
            MainCanvasReference.SetActiveNewPanel(MainCanvasReference.PlayerWaitScreen);
        }
        else
        {
            Debug.LogError("A room with that name is already created");
        }
    }

    public void GoToMainMenu()
    {
        MainCanvasReference.SetActiveNewPanel(MainCanvasReference.MainMenuScreen);
    }

    public override void OnPanelStart()
    {
        _roomNameInputField.text = "";
        _createRoomButton.interactable = false;
    }
}

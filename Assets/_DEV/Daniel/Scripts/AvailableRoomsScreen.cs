using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvailableRoomsScreen : PanelBase
{
    [SerializeField] private Button _goBackButton = default;
    [SerializeField] private GameObject _roomButtoninfo = default;
    [SerializeField] private Transform _contentTransform = default;

    //Load button prefabs
    //private List<RoomInfoButton> _roomInfoList;
    //private List<RoomInfoButton> RoomInfoList => _roomInfoList != null ? _roomInfoList : InitializeRoomInfoList();
    //private List<RoomInfoButton> InitializeRoomInfoList() { _roomInfoList = new List<RoomInfoButton>(); return _roomInfoList; }

    private int roomNumber = default;

    private string roomName = default;

    public override void OnPanelActive()
    {
        _goBackButton.onClick.AddListener(GoToMainMenu);
    }

    public void GoToMainMenu()
    {
        MainCanvasReference.SetActiveNewPanel(MainCanvasReference.MainMenuScreen);
    }

    public override void OnPanelStart()
    {
        LoadAvailableRooms();
    }

    public void LoadAvailableRooms()
    {
        CleanItems();

        //Get list of rooms
        //List<string> roomNames = PhotonSingleton.GetRoomNames();

        //Set room names from list and set each one in a button
        //for (int i = 0; i < roomNames.Count; i++)
        //{
        //    roomNumber = i;
        //    roomName = roomNames[i];
        //    GameObject roomButtonObject = Instantiate(_roomButtoninfo, _contentTransform.position, Quaternion.identity, _contentTransform);
        //    RoomInfoButton roomButtonInfo = roomButtonObject.GetComponent<RoomInfoButton>();

        //    roomButtonInfo.RoomNameText.text = roomNames[i];
        //    roomButtonInfo.PlayersInRoomText.text = PhotonSingleton.GetPlayersInRoom(i).ToString() + " / 5";
        //    roomButtonInfo.RoomButton.onClick.AddListener(SetRoomButtonLoad);

        //    RoomInfoList.Add(roomButtonInfo);
        //}
    }

    private void CleanItems()
    {
        //Clear old instances of the items
        //if (RoomInfoList.Count == 0) { return; }
        //foreach (RoomInfoButton roomInfo in RoomInfoList) { Destroy(roomInfo.gameObject); }
        //RoomInfoList.Clear();
    }

    public void SetRoomButtonLoad()
    {
        //When clicked button load room with specific data
        //PhotonSingleton.JoinRoom(roomName);
        //MainCanvasReference.SetActiveNewPanel(MainCanvasReference.PlayerWaitScreen);
    }
}

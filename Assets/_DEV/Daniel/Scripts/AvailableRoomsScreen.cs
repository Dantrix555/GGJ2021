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
    private List<Button> _roomButtonsList = new List<Button>();

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

        List<string> roomNames = PhotonSingleton.GetRoomNames();

        for (int i = 0; i < roomNames.Count; i++)
        {
            roomName = roomNames[i];
            GameObject roomButtonObject = Instantiate(_roomButtoninfo, _contentTransform.position, Quaternion.identity, _contentTransform);
            Button roomButtonInfo = roomButtonObject.GetComponentInChildren<Button>();

            roomButtonInfo.GetComponentInChildren<Text>().text = roomNames[i];
            roomButtonInfo.onClick.AddListener(SetRoomButtonLoad);

            _roomButtonsList.Add(roomButtonInfo);
        }
    }

    private void CleanItems()
    {
        //Clear old instances of the items
        if (_roomButtonsList.Count == 0) { return; }
        foreach (Button roomButtonInfo in _roomButtonsList) { Destroy(roomButtonInfo.gameObject); }
        _roomButtonsList.Clear();
    }

    public void SetRoomButtonLoad()
    {
        //When clicked button load room with specific data
        PhotonSingleton.JoinRoom(roomName);
        MainCanvasReference.SetActiveNewPanel(MainCanvasReference.PlayerWaitScreen);
    }
}

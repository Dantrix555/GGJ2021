using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : PanelBase
{
    [SerializeField] private Button _createRoomButton = default;
    [SerializeField] private Button _joinRoomButton = default;
    [SerializeField] private Button _availableRoomsButton = default;
    [SerializeField] private Button _titleScreenButton = default;

    public override void OnPanelActive()
    {
        _createRoomButton.onClick.AddListener(LoadCreateRoomScreen);
        _joinRoomButton.onClick.AddListener(LoadJoinRoomScreen);
        _availableRoomsButton.onClick.AddListener(LoadAvailableRoomScreen);
        _titleScreenButton.onClick.AddListener(LoadTitleScreen);
    }

    public void LoadCreateRoomScreen()
    {
        MainCanvasReference.SetActiveNewPanel(MainCanvasReference.CreateRoomScreen);
    }

    public void LoadJoinRoomScreen()
    {
        MainCanvasReference.SetActiveNewPanel(MainCanvasReference.JoinRoomScreen);
    }

    public void LoadAvailableRoomScreen()
    {
        MainCanvasReference.SetActiveNewPanel(MainCanvasReference.AvailableRoomsScreen);
    }

    public void LoadTitleScreen()
    {
        MainCanvasReference.SetActiveNewPanel(MainCanvasReference.TitleScreen);
    }

    public override void OnPanelStart()
    {
        PhotonSingleton.SetSyncScene(true);
    }
}

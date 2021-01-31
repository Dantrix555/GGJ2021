using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MainCanvasController : MonoBehaviour
{
    [Header("Canvas Panels")]
    [SerializeField] private LoadingScreen _loadingScreen = default;
    [SerializeField] private TitleScreen _titleScreen = default;
    [SerializeField] private MainMenuScreen _mainMenuScreen = default;
    [SerializeField] private CreateRoomScreen _createRoomScreen = default;
    [SerializeField] private JoinRoomScreen _joinRoomScreen = default;
    [SerializeField] private AvailableRoomsScreen _availableRoomsScreen = default;
    [SerializeField] private PlayersWaitScreen _playersWaitScreen = default;

    public LoadingScreen LoadingScreen => _loadingScreen;
    public TitleScreen TitleScreen => _titleScreen;
    public MainMenuScreen MainMenuScreen => _mainMenuScreen;
    public CreateRoomScreen CreateRoomScreen => _createRoomScreen;
    public JoinRoomScreen JoinRoomScreen => _joinRoomScreen;
    public AvailableRoomsScreen AvailableRoomsScreen => _availableRoomsScreen;
    public PlayersWaitScreen PlayerWaitScreen => _playersWaitScreen;

    private PanelBase activePanel;
    private PanelBase lastActivePanel;

    public void Start()
    {
        _loadingScreen.SetMainCanvasReference(this);
        _titleScreen.SetMainCanvasReference(this);
        _mainMenuScreen.SetMainCanvasReference(this);
        _createRoomScreen.SetMainCanvasReference(this);
        _joinRoomScreen.SetMainCanvasReference(this);
        _availableRoomsScreen.SetMainCanvasReference(this);
        _playersWaitScreen.SetMainCanvasReference(this);

        _loadingScreen.ForcePanelClose();
        _titleScreen.ForcePanelClose();
        _mainMenuScreen.ForcePanelClose();
        _createRoomScreen.ForcePanelClose();
        _joinRoomScreen.ForcePanelClose();
        _availableRoomsScreen.ForcePanelClose();
        _playersWaitScreen.ForcePanelClose();

        SetActiveNewPanel(_loadingScreen);
    }


    public void TriggerStartPanel(bool haveSavedName)
    {
        if (haveSavedName)
        {
            PhotonSingleton.SetPlayerNickname(PlayerPrefs.GetString(PhotonSingleton.PlayerNamePrefsKey));
            SetActiveNewPanel(MainMenuScreen);
        }
        else
        {
            SetActiveNewPanel(TitleScreen);
        }
    }

    public void SetActiveNewPanel(PanelBase newPanel)
    {

        if (activePanel != null) { lastActivePanel = activePanel; activePanel.SetPanelActiveStatus(false); };
        newPanel.SetPanelActiveStatus(true);
        activePanel = newPanel;

        activePanel.OnPanelStart();
    }
}

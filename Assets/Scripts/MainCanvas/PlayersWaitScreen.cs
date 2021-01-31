using UnityEngine;
using UnityEngine.UI;

public class PlayersWaitScreen : PanelBase
{
    //Specific room callbacks references
    [SerializeField] private WaitScreenCallbacks _waitScreenCallbacks = default;
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

        _waitScreenCallbacks.gameObject.SetActive(false);

        _goBackButton.onClick.AddListener(GoToMainMenu);
        _startGameButton.onClick.AddListener(StartGame);
    }

    public void GoToMainMenu()
    {
        //Clear RPC buffer and leaves the room
        _waitScreenCallbacks.CleanRPCBuffer();
        PhotonSingleton.LeaveRoom();
        MainCanvasReference.SetActiveNewPanel(MainCanvasReference.MainMenuScreen);
    }

    public void StartGame()
    {
        _waitScreenCallbacks.CleanRPCBuffer();
        PhotonSingleton.SetRoomVisible(false);
        PhotonSingleton.LoadScene(1);
    }

    public override void OnPanelStart()
    {
        _waitScreenCallbacks.PlayerNames = _playerNames;
        _waitScreenCallbacks.StartGameButton = _startGameButton;
    }
}

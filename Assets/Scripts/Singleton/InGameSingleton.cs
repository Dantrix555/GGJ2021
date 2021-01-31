using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InGameSingleton : BASESingleton<InGameSingleton>
{
    protected InGameSingleton() { }

    [SerializeField] private PhotonView _photonView;

    private PhotonMovement _cachedPlayer = default;
    
    private Vector3[] _playersPosition = default;

    private List<PhotonMovement> _playersInGame = new List<PhotonMovement>();
    public static List<PhotonMovement> PlayersInGame => Instance._playersInGame;

    private int _timeInSeconds = 0;
    public static int TimeInSeconds { get => Instance._timeInSeconds; set => Instance._timeInSeconds = value; }
    
    private int _actualRound = 1;
    public static int ActualRound { get => Instance._actualRound; set => Instance._actualRound = value; }

    private void Awake()
    {
        //Set manually players possible position
        _playersPosition = new Vector3[5];
        _playersPosition[0] = new Vector3(0f, 0.5f, 1f);
        _playersPosition[1] = new Vector3(-3f, 0.5f, 1f);
        _playersPosition[2] = new Vector3(-6f, 0.5f, 1f);
        _playersPosition[3] = new Vector3(3f, 0.5f, 1f);
        _playersPosition[4] = new Vector3(6f, 0.5f, 1f);
    }

    private void Update()
    {
        Debug.LogError(_playersInGame.Count);
    }

    public static void SetCachedPlayerController(PhotonMovement playerController)
    {
        Instance._cachedPlayer = playerController;
    }

    public static Vector3 NewPlayerPosition(int _positionIndex)
    {
        Vector3 newPosition = Instance._playersPosition[_positionIndex];
        return newPosition;
    }

    public static void SetGameFinished()
    {
        Instance._photonView.RPC("StopAllPlayers", RpcTarget.All);
    }

    [PunRPC]
    private void StopAllPlayers()
    {
        foreach (PhotonMovement player in _playersInGame)
            player.BlockPlayer();
    }
}

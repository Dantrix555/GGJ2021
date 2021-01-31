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

    public static void SetCachedPlayerController(PhotonMovement playerController)
    {
        Instance._cachedPlayer = playerController;
    }

    public static Vector3 NewPlayerPosition(int _positionIndex)
    {
        Vector3 newPosition = Instance._playersPosition[_positionIndex];
        return newPosition;
    }

    public static void SetRoundFinished()
    {
        Instance._photonView.RPC("StopRound", RpcTarget.All);
    }

    [PunRPC]
    private void StopRound()
    {
        StartCoroutine(StopGame());
    }

    private IEnumerator StopGame()
    {
        Debug.LogError("Game Stopped for a while");

        _cachedPlayer.BlockPlayer();

        yield return new WaitForSeconds(1f);
        

        if (_actualRound <= 5 * PhotonSingleton.GetPlayersInRoom())
        {
            //Do fade in effect

            _actualRound++;

            //yield return new WaitForSeconds(1f);

            _cachedPlayer.SetStartPosition();

            //Do fade out effect
            
            _cachedPlayer.UnBlockPlayer();

            _timeInSeconds = 10;
        }
        else
        {
        //Set the winner

        //yield return new WaitForSeconds(2f);

        //Do fade in effect

            _cachedPlayer.CameraReference.IsFollowingPlayer(false);

            yield return new WaitForSeconds(2f);

            PhotonSingleton.LeaveRoom();
        }
    }
}

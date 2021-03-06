using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonOverrides : MonoBehaviourPunCallbacks
{
    public override void OnConnectedToMaster()
    {
        Debug.LogWarning("Player connected to master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.LogWarning("Player joined successfuly to lobby");
    }

    public override void OnJoinedRoom()
    {
        Debug.LogWarning("Player joined in a new room");
    }

    public override void OnLeftRoom()
    {
        if(InGameSingleton.Instance != null)
            PhotonSingleton.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount >= PhotonSingleton.MaxPlayersPerRoom)
        {
            PhotonSingleton.SetRoomVisible(false);
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        if (InGameSingleton.Instance == null)
            PhotonSingleton.SetRoomVisible(true);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {

        //Gets the list of new rooms have been created
        foreach (RoomInfo room in roomList)
        {
            //Check if the room isn't joinable
            if (room.PlayerCount <= 0 || !room.IsVisible || !room.IsOpen || room.RemovedFromList)
            {
                //If the room isn't joinable and exist in the cached room list, destroy the reference of that room
                if (PhotonSingleton.CachedRoomList.Contains(room))
                {
                    PhotonSingleton.CachedRoomList.Remove(room);
                }

                continue;
            }

            //If the new room has an old reference in cached room list destroy the old one
            if (PhotonSingleton.CachedRoomList.Contains(room))
            {
                PhotonSingleton.CachedRoomList.Remove(room);
            }

            //Set the reference of the new room
            PhotonSingleton.CachedRoomList.Add(room);
        }
    }
}

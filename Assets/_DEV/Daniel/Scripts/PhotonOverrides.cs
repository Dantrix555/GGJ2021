using System.Collections;
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
        //PhotonSingleton.LoadScene(PhotonSingleton.Scene.Menu);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount >= PhotonSingleton.MaxPlayersPerRoom)
        {
            PhotonSingleton.SetRoomVisible(false);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //if (RPCSingleton.Instance == null && PhotonSingleton.PlayerIsWaiting)
        //    PhotonSingleton.SetRoomVisible(true);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {

        //Gets the list of new rooms have been created
        foreach (RoomInfo room in roomList)
        {

            Debug.LogError(room.Name);

            //Check if the room isn't joinable
            if (room.PlayerCount <= 0 || !room.IsVisible || !room.IsOpen || room.RemovedFromList)
            {
                //If the room isn't joinable and exist in the cached room list, destroy the reference of that room
                if (PhotonSingleton.CachedRoomList.Contains(room))
                {
                    Debug.LogError("Room " + room.Name + " has been removed");
                    PhotonSingleton.CachedRoomList.Remove(room);
                }

                continue;
            }

            //If the new room has an old reference in cached room list destroy the old one
            if (PhotonSingleton.CachedRoomList.Contains(room))
            {
                Debug.LogError("Old room reference: " + room.Name + " has been removed");
                PhotonSingleton.CachedRoomList.Remove(room);
            }

            //Set the reference of the new room
            PhotonSingleton.CachedRoomList.Add(room);
        }
    }
}

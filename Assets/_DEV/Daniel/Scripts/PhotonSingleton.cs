using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonSingleton : BASESingleton<PhotonSingleton>
{
    protected PhotonSingleton() {}

    public const string PlayerNamePrefsKey = "PlayerNickName";
    public const int MaxPlayersPerRoom = 5;
    public const int MinPlayersRequired = 2;

    private List<RoomInfo> _cachedRoomList = new List<RoomInfo>();
    public static List<RoomInfo> CachedRoomList { get => Instance._cachedRoomList; set => Instance._cachedRoomList = value; }

    public enum Scene
    {
        Menu,
        InGame
    }

    public static void SetSyncScene(bool state)
    {
        PhotonNetwork.AutomaticallySyncScene = state;
    }

    public static void ConnectToPhoton()
    {
        PhotonNetwork.GameVersion = "0.1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public static bool PhotonIsActive()
    {
        return PhotonNetwork.IsConnectedAndReady;
    }

    public static void CreateRoom(string roomName)
    {
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = MaxPlayersPerRoom,};
        PhotonNetwork.CreateRoom(roomName, roomOptions, null);
    }

    public static void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public static void SetRoomVisible(bool state)
    {
        PhotonNetwork.CurrentRoom.IsOpen = state;
        PhotonNetwork.CurrentRoom.IsVisible = state;
    }

    public static bool RoomExists(string roomName)
    {
        foreach (RoomInfo room in Instance._cachedRoomList) { if (room.Name == roomName) { return true; } }
        return false;
    }

    public static void LoadScene(Scene sceneToLoad)
    {
        //PhotonNetwork.LoadLevel(sceneToLoad.ToString());
        Debug.LogError("Here we change the scene");
    }

    public static void SetPlayerNickname(string newNickname)
    {
        if (newNickname.Length < 3) { Debug.LogError("Were trying to set a nickname with length less than 3 characters"); return; }
        PhotonNetwork.NickName = newNickname;
        PlayerPrefs.SetString(PlayerNamePrefsKey, newNickname);
    }

    public static string GetPlayerNickname()
    {
        return PhotonNetwork.NickName;
    }

    public static List<string> GetRoomNames()
    {
        List<string> roomNames = new List<string>();
        for (int i = 0; i < Instance._cachedRoomList.Count; i++) { roomNames.Add(Instance._cachedRoomList[i].Name); }
        return roomNames;
    }

    public static int GetPlayerIndex(string playerNickname)
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.PlayerList[i].NickName == playerNickname)
                return i;
        }
        return -1;
    }

    public static List<string> GetPlayersInRoomNames()
    {
        List<string> playersNames = new List<string>();
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList) { playersNames.Add(player.NickName); }
        return playersNames;
    }

    public static Photon.Realtime.Player GetPlayerInRoom(int index)
    {
        return PhotonNetwork.PlayerList[index];
    }

    public static int GetPlayersInRoom()
    {
        return PhotonNetwork.CurrentRoom.PlayerCount;
    }

    public static bool PlayerIsOwner()
    {
        return PhotonNetwork.IsMasterClient;
    }

    public static bool CanGameStart()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount >= MinPlayersRequired) { return true; }
        return false;
    }

    public static void LeaveRoom()
    {
        //The Master client leaves the room, and there's another players in room
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1 && PhotonNetwork.IsMasterClient)
        {
            //If the owner leaves the room, the new master client is the next player in the list (MasterClient always has index 0)
            PhotonNetwork.SetMasterClient(PhotonNetwork.CurrentRoom.GetPlayer(1));
        }

        PhotonNetwork.LeaveRoom();
    }

    public static void LeaveLobby()
    {
        PhotonNetwork.LeaveLobby();
    }
}

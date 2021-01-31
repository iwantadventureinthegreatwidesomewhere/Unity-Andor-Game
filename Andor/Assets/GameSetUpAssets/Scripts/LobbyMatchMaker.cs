using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyMatchMaker : MonoBehaviourPunCallbacks, ILobbyCallbacks
{   

    public static LobbyMatchMaker lobby;

    public string roomName;
    public int roomSize;
    
    public GameObject roomListingPrefab;
    public Transform roomsPanel;


    private void Awake(){
        lobby = this; //this is to create a singleton
    }


    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //Connects to Photon Master server
    }

    public override void OnConnectedToMaster(){
        
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
        PlayerPrefs.SetString("MyUsername", PhotonNetwork.NickName);
        Debug.Log("Player has connected to the photonserver!");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList){
        base.OnRoomListUpdate(roomList);
        RemoveRoomListings();
        foreach(RoomInfo room in roomList){
            ListRoom(room);
        }
    }

    void RemoveRoomListings(){
        while(roomsPanel.childCount != 0){
            Destroy(roomsPanel.GetChild(0).gameObject);
        }
    }

    void ListRoom(RoomInfo room){
        if(room.IsOpen && room.IsVisible){
            GameObject tempListing = Instantiate(roomListingPrefab, roomsPanel);
            RoomBtnList tempButton = tempListing.GetComponent<RoomBtnList>();
            tempButton.roomName = room.Name;
            tempButton.roomSize = room.MaxPlayers;
            tempButton.SetRoom();
        }
    }

    

    
    public override void OnCreateRoomFailed(short returnCode, string message){
        Debug.Log("Failed to create room");
        
    }

    public void OnRoomNameChanged(string nameIn){
        roomName = nameIn;
    }

    public void OnRoomSizeChanged(string sizeIn){
        roomSize = int.Parse(sizeIn);
    }


    public void CreateRoom(){
        
        RoomOptions roomOps = new RoomOptions(){ IsVisible = true, IsOpen = true, MaxPlayers = (byte) roomSize };
        PhotonNetwork.CreateRoom(roomName, roomOps);
    }

    public void JoinLobbyOnClick(){
        if(!PhotonNetwork.InLobby){
            PhotonNetwork.JoinLobby();
            
        }
    }
    
}
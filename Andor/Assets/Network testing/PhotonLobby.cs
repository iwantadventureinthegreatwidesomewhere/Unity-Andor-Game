using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonLobby : MonoBehaviourPunCallbacks
{   

    public static PhotonLobby lobby;

    public GameObject battleButton;

    public GameObject cancelButton;

    private void Awake(){
        lobby = this; //this is to create a singleton
    }


    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //Connects to Photon Master server
    }

    public override void OnConnectedToMaster(){
        Debug.Log("Player has connected to the photonserver!");
        PhotonNetwork.AutomaticallySyncScene = true;
        battleButton.SetActive(true);
    }

    public void OnBattleButtonClicked(){
        Debug.Log("Looking for game");
        battleButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message){

        Debug.Log("Failure to join, create room");
        createRoom();
        

    }
    public override void OnCreateRoomFailed(short returnCode, string message){
        Debug.Log("Failed to create room");
        createRoom();
    }


    void createRoom(){
        int randomRoomName  = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions(){ IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
    }

    public void onCancelButtonClick(){
        Debug.Log("Not looking anymore");
        cancelButton.SetActive(false);
        battleButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

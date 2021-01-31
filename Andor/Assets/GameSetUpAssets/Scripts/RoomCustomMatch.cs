using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomCustomMatch : MonoBehaviourPunCallbacks, IInRoomCallbacks{

    //room info
    public static RoomCustomMatch room;
    private PhotonView PV;

    public bool isGameLoaded;
    public int currentScene;

    Player[] photonPlayers;
    public int playersInRoom;
    public int myNumberInRoom;

    public int playersInGame;

    public int multiPlayerScene;

    public GameObject lobbyGO;

    public GameObject roomGO;

    public Transform playersPanel;

    public GameObject playerListingPrefab;
    public GameObject startButton;

    private void Awake(){


        //set up Singleton
        if(RoomCustomMatch.room == null){
            RoomCustomMatch.room = this;
        } else {
            if(Room.room != this){
                Destroy(RoomCustomMatch.room.gameObject);
                RoomCustomMatch.room = this;

            }
        }
        DontDestroyOnLoad(this.gameObject);
        
    }

    void Start(){
        PV = GetComponent<PhotonView>();
    }

    

    public override void OnEnable(){
        //subscribe to functions
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable(){
        //subscribe to functions
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer){
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("A new Player has joined");
        ClearPlayerListing();
        ListPlayers();
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom++;

    }

    public override void OnPlayerLeftRoom(Player otherPlayer){
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log(otherPlayer.NickName + " has left the game");
        playersInRoom--;
        ClearPlayerListing();
        ListPlayers();
    }

    public override void OnJoinedRoom(){
        
        base.OnJoinedRoom();
        Debug.Log("Entered a room");

        lobbyGO.SetActive(false);
        roomGO.SetActive(true);
        startButton.SetActive(false);
        if(PhotonNetwork.IsMasterClient){
            startButton.SetActive(true);
        }

        ClearPlayerListing();
        ListPlayers();
        
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        myNumberInRoom = playersInRoom;
        
        


        
    }

    void ClearPlayerListing(){
        for(int i = playersPanel.childCount - 1; i>=0 ; i--){
            Destroy(playersPanel.GetChild(i).gameObject);
        }
    }

    void ListPlayers(){
        if(PhotonNetwork.InRoom){
            foreach(Player player in PhotonNetwork.PlayerList){
                GameObject tempListing =  Instantiate(playerListingPrefab, playersPanel);
                Text tempText = tempListing.transform.GetChild(0).GetComponent<Text>();
                tempText.text = player.NickName;
            }
        }

    }

    public void StartGame(){
        /*
        if(PhotonNetwork.IsMasterClient){
            return;
        }*/
        PhotonNetwork.LoadLevel(multiPlayerScene);
        
    }

    void OnSceneFinishedLoading(Scene scene,  LoadSceneMode mode){
        currentScene = scene.buildIndex;
        if(currentScene == multiPlayerScene){
            CreatePlayer();
        }

    }

    private void CreatePlayer(){

        
        PhotonNetwork.Instantiate(Path.Combine("Players", "Player"), transform.position, Quaternion.identity, 0);
    }


}
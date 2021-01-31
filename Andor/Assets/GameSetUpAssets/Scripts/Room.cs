using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class Room : MonoBehaviourPunCallbacks, IInRoomCallbacks{

    //room info
    public static Room room;
    private PhotonView PV;

    public bool isGameLoaded;
    public int currentScene;
/*
    Player[] photonPlayers;
    public int playersInRoom;
    public int myNumberInRoom;

    public int playersInGame;
*/
    public int multiPlayerScene;


    private void Awake(){


        //set up Singleton
        if(Room.room == null){
            Room.room = this;
        } else {
            if(Room.room != this){
                Destroy(Room.room.gameObject);
                Room.room = this;

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

    public override void OnJoinedRoom(){
        
        base.OnJoinedRoom();
        Debug.Log("Entered a room");
        /*
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        myNumberInRoom = playersInRoom;
        PhotonNetwork.NickName = myNumberInRoom.ToString();
        */


        StartGame();
    }

    void StartGame(){
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

        
        //PhotonNetwork.Instantiate(Path.Combine("Heroes", "Player"), transform.position, Quaternion.identity, 0);
    }


}
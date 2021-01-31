﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RoomBtnList : MonoBehaviour
{
    public Text nameText;
    public Text sizeText;

    public string roomName;
    public int roomSize;

    public void SetRoom(){

        nameText.text = roomName;
        sizeText.text = roomSize.ToString();
    }

    public void JoinRoomOnClick(){
        PhotonNetwork.JoinRoom(roomName);
    }
}

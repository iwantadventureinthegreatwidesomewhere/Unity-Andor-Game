using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Scripts{
    public class ChatSyncUpdater : MonoBehaviour
    {
        void Awake(){
            GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
            Instantiator i = GameObject.Find("God").GetComponent<Instantiator>();
            GM.sendMessageToChat(i.messageFromPlayer, Message.MessageType.playerMessage);

            Destroy(this);
            
        }
    }
}
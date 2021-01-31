using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts{    
    public class Instantiator : MonoBehaviour
    {
        //formove update
        public GameObject moveUpdater;

        public HeroController hero;
        public int newpos;
        public void updateMove(HeroController h, int p){
            hero = h;
            newpos = p; 

            Instantiate(moveUpdater, new Vector3(0, 0, 0), Quaternion.identity);
        }

        //for changes on the wells
        public GameObject wellUpdater;
        public int heroDrinkerID;
        public void updateWell(int id){
            heroDrinkerID = id;
            Instantiate(wellUpdater, new Vector3(0, 0, 0), Quaternion.identity);
        }

        //for Changes regarding farmers
        //pick up
        public GameObject farmerPickUpdater;
        public int heroPickUp;
        public void updateFarmerPickUp(int id){
            heroPickUp = id;
            Instantiate(farmerPickUpdater, new Vector3(0, 0, 0), Quaternion.identity);
        }

        //drop
        public GameObject farmerDropUpdater;
        public int heroDrop;
        public void updateFarmerDrop(int id){
            heroDrop = id;
            Instantiate(farmerDropUpdater, new Vector3(0, 0, 0), Quaternion.identity);
        }

        //for when players end their day
        public GameObject playerDayEnderUpdater;
        public int heroEndid;

        public void  updatePlayerEnding(int id){
            heroEndid = id;
            Instantiate(playerDayEnderUpdater, new Vector3(0, 0, 0), Quaternion.identity);

        }

        //for newday
        public GameObject newDayUpdater;
        public void newday(){

            Instantiate(newDayUpdater, new Vector3(0, 0, 0), Quaternion.identity);
        }

        //for Combat updates
        public GameObject combatUpdater;
        
        public void updateCombat(){

            Instantiate(combatUpdater, new Vector3(0, 0, 0), Quaternion.identity);
        }

        //for retreat recover
        public GameObject retreatUpdater;

        public void updateRecover(){
            Instantiate(retreatUpdater, new Vector3(0, 0, 0), Quaternion.identity);
        }

        //for chat sync
        public GameObject ChatSyncUpdater;

        public string messageFromPlayer;

        public void sendChatMess(string mess){
            messageFromPlayer = mess;
            Instantiate(ChatSyncUpdater, new Vector3(0, 0, 0), Quaternion.identity);
        }

    }
}

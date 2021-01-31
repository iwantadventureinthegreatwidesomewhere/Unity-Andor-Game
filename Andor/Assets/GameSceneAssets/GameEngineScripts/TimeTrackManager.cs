using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts{    
    public class TimeTrackManager : MonoBehaviour
    {
        [SerializeField]
        GameObject[] timeTracker;

        public bool firstToEnd = true;
        public GameObject getTimeTrack(int time){
            
            return timeTracker[time+1];


        }

        public GameObject getRoosterBox(){
            return timeTracker[1];
        }

        public GameObject getSunriseBox(){
            return timeTracker[0];
        }

        public bool isFirstToEnd(){
            if(firstToEnd == true){
                firstToEnd = false;
                return true;
            }
            return firstToEnd;
        }
    }
}

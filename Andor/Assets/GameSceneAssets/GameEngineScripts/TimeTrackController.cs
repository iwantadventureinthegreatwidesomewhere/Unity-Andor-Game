using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts{
    public class TimeTrackController : MonoBehaviour
    {
        
        public GameObject timeTrack;

        [SerializeField]
        TimeTrackManager ttm;

        int time = 0; //from 0 to 10
        public int maxTime=10;
        public void setTTM(TimeTrackManager attm){
            ttm = attm;
        }

        public void tick(){
            time++;
            if(time > maxTime){
                if(ttm.isFirstToEnd()){
                    timeTrack = ttm.getRoosterBox();
                    setTTposition();
                    time = 0;
                    return;
                }
                timeTrack = ttm.getSunriseBox();
                setTTposition();
                time = 0;
            }
            
            setTT(time);

        }
        public int getTime(){
            return time;
        }

        public int getTimeLeft(){
            return maxTime - time;
        }
        public void setTT(int i){
            timeTrack = ttm.getTimeTrack(i);
            setTTposition();
        }


        public void setTTposition(){
            this.transform.position = timeTrack.transform.position;
        }

        public void ResetTurn(){
            if(ttm.isFirstToEnd()){
                timeTrack = ttm.getRoosterBox();
                setTTposition();
                time = 0;
                return;
            }
            timeTrack = ttm.getSunriseBox();
            setTTposition();
            time = 0;
        }

    }
}

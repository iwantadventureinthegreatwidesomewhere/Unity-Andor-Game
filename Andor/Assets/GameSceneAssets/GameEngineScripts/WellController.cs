using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts{
    public class WellController : MonoBehaviour
    {
        [SerializeField]
        GameObject currentPosition;

        Well well;
        public void setPosition(){
            this.transform.position = currentPosition.transform.position;

        }

        public GameObject getCurrentPosition(){
            return currentPosition;
        }

        public void setNode(GameObject pos){
            currentPosition = pos;
            setPosition(); 
        } 

        public void setWell(Well pWell){
            well = pWell;
        }

        public Well getWell(){
            return well;
        }

        public void remove()
        {
            Destroy(this);
        }
    }
}
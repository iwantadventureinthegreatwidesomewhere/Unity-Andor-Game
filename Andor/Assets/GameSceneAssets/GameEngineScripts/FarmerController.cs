using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts{
    public class FarmerController : MonoBehaviour
    {
        [SerializeField]
        public GameObject currentPosition;

        Farmer farmer;
        public void setPosition(){
            Vector3 pos = currentPosition.transform.position;
            pos.x -= 0.25f;
            pos.z -= 0.25f;
            this.transform.position = pos;

        }

        public void setNode(GameObject pos){
            currentPosition = pos;
            setPosition(); 
        } 

        public GameObject getCurrentPosition(){
            return currentPosition;
        }

        public void setFarmer(Farmer pfarmer){
            farmer = pfarmer;
        }

        public Farmer getFarmer()
        {
            return farmer;
        }
   
    }
}

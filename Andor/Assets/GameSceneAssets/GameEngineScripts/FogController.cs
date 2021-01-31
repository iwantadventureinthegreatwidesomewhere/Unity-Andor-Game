using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts{
    public class FogController : MonoBehaviour
    {
        [SerializeField]
        GameObject currentPosition;

        Fog aFog;

        public void setPosition(){
            this.transform.position = currentPosition.transform.position;

        }

        public void setNode(GameObject pos){
            currentPosition = pos;
            setPosition(); 
        } 

        public void setFog(Fog pfog){
            aFog = pfog;
        }

        public GameObject getCurrentPosition()
        {
            return currentPosition;
        }

        public Fog getFog()
        {
            return aFog;
        }
    }
}

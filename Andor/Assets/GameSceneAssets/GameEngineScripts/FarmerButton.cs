using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class FarmerButton : MonoBehaviour
    {

        public void OnPickUpButtonPressed()
        {
            
            GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
            //HeroSelection.HS.mySelectedCharacter;
            GM.farmerPickedUpBy(HeroSelection.HS.mySelectedCharacter);
            
        }

        public void OnDropButtonPressed()
        {
            
            GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
            //HeroSelection.HS.mySelectedCharacter;
            GM.farmerDroppedBy(HeroSelection.HS.mySelectedCharacter);
            
        }
    }
}
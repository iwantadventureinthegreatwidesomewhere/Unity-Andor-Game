using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class EndDayBtn: MonoBehaviour
    {
        public bool Pressed { get; set; }

        public void OnButtonPressed()
        {
            Pressed = true;
            GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
            GM.endDay(HeroSelection.HS.mySelectedCharacter);
            GM.endTurn();
            
        }
    }
}
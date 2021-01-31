using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class MoveButton : MonoBehaviour
    {
        public bool Pressed { get; set; }

        public void OnButtonPressed()
        {
            Pressed = true;
            GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
            GameObject hero = GameObject.Find(GM.GetHeroByPID(HeroSelection.HS.mySelectedCharacter));
            hero.GetComponent<HeroController>().show();
        }
    }
}


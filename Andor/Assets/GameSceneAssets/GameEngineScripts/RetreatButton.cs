using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class RetreatButton : MonoBehaviour
    {
        public bool Pressed { get; set; }

        public void OnButtonPressed()
        {
            Pressed = true;
            CombatManager.instance.setRetreatHeroID(HeroSelection.HS.mySelectedCharacter);
        }
    }
}

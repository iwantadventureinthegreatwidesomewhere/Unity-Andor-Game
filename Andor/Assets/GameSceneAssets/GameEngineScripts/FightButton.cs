using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class FightButton : MonoBehaviour
    {
        public bool Pressed { get; set; }

        public void OnButtonPressed()
        {
            Pressed = true;
            //TODO: Mark monsters in your attack range
            CombatManager.instance.setBattleHeroId(HeroSelection.HS.mySelectedCharacter);
        }
    }
}



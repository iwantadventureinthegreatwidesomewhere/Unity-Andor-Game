using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts{
    public class FarmerPickUpUpdater : MonoBehaviour
    {
        void Awake() {
            Instantiator i = GameObject.Find("God").GetComponent<Instantiator>();
            GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
            GameObject hero = GameObject.Find(GM.GetHeroByPID(i.heroPickUp)) ;
            hero.GetComponent<HeroController>().grabFarmer();
            i.heroDrop = -1;
            Destroy(this);
        }
    }
}


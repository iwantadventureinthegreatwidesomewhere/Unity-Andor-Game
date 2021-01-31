using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts{
    public class WellUpdate : MonoBehaviour
    {
        void Awake() {
            Instantiator i = GameObject.Find("God").GetComponent<Instantiator>();
            GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
            GameObject hero = GameObject.Find(GM.GetHeroByPID(i.heroDrinkerID)) ;
            hero.GetComponent<HeroController>().useWell();
            i.heroDrinkerID = -1;
            Destroy(this);
        }
    }
}

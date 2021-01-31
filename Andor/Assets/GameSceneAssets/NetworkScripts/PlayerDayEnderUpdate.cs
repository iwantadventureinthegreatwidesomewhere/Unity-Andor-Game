using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts{
    public class PlayerDayEnderUpdate : MonoBehaviour
    {
    
    void Awake() {
        Instantiator i = GameObject.Find("God").GetComponent<Instantiator>();
        GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameObject hero = GameObject.Find(GM.GetHeroByPID(i.heroEndid));
        hero.GetComponent<HeroController>().hasEndedDay = true;
        GM.nextTurnOrder.Add(i.heroEndid);
        i.heroEndid = -1;
        Destroy(this);
    }
    
}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts{
    public class MoveUpdate : MonoBehaviour
    {
    
    void Awake() {
        Instantiator i = GameObject.Find("God").GetComponent<Instantiator>();
        i.hero.GetComponent<HeroController>().heroMove(GameManager.Board.getNodeWithRank(i.newpos));
        i.hero = null;
        i.newpos = 0;
        Destroy(this);
    }

    
}
}

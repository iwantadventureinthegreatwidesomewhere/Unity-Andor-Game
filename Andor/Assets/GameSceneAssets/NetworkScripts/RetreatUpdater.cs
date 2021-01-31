using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Scripts{
    public class RetreatUpdater : MonoBehaviour
    {
        void Awake(){
            GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
            CombatManager CM = CombatManager.instance;
            MonsterController monster = null ;
            HeroController hero = GameObject.Find(GM.GetHeroByPID(CM.battleHeroID)).GetComponent<HeroController>();
            if(hero.GetHeroID() != HeroSelection.HS.mySelectedCharacter){
                foreach(GameObject m in GM.getMonsterList()){
                    if(m.GetComponent<MonsterController>().getCurrentPosition().GetComponent<Node>().getRank() == CM.MonsterFighterID){
                        monster = m.GetComponent<MonsterController>();
                        break;
                    }
                }

                monster.GetMonster().Recover();
            }


            
            CM.battleHeroID = 0;
            CM.MonsterFighterID = 0;
            CM.heroRetreaterID = 0;

            Destroy(this);
            
        }
    }
}
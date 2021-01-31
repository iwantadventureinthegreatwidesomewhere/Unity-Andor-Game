using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Scripts{
    public class CombatUpdater : MonoBehaviour
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

                int result = CM.heroSyncBV - CM.monsterSyncBV;

                if(result > 0){
                    monster.GetMonster().TakeDamage(result);
                } else {
                    hero.getHero().TakeDamage(result);

                }
                hero.TimeTrackTick();
            }


            CM.monsterSyncBV = 0;
            CM.heroSyncBV = 0;
            

            Destroy(this);
            
        }
    }
}
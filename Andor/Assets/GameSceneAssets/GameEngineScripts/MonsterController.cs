using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class MonsterController : MonoBehaviour
    {
        public MonsterKind monsterKind;
        private Monster monster;

        [SerializeField]
        GameObject currentPosition;

        [SerializeField]
        GameObject monsterNameUI;

        [SerializeField]
        GameObject monsterWPUI;

        [SerializeField]
        GameObject monsterSPUI;

        
        public void setUIElements(GameObject name, GameObject wp, GameObject sp){
            monsterNameUI = name;
            monsterWPUI = wp;
            monsterSPUI = sp;
        }
        

        public void setMonster(Monster m){
            monster = m;
        }

        public Monster GetMonster()
        {
            return monster;
        }

        private void OnMouseDown()
        {
            if(!CombatManager.instance.FightButtonPressed())
            {
                return;
            }

            bool inRange = false;
            //TODO: Get the current hero
            GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
            Hero hero = GameObject.Find(GM.GetHeroByPID(HeroSelection.HS.mySelectedCharacter)).GetComponent<HeroController>().getHero();
            //Don't start combat if no time left
            if(!hero.HasTimeLeft())
            {
                return;
            }
            //If on same tile, you are in range
            if (hero.getRank() == currentPosition.GetComponent<Node>().getRank())
            {
                inRange = true;
            }
            else if (hero.getHeroKind() == HeroKind.Archer || hero.IsCarrying(new Bow(ItemWeight.Heavy)))
            {
                //If you are archer/have bow and are on a neighbouring tile, you are in range
                foreach(GameObject neighbour in currentPosition.GetComponent<Node>().getNeighbours())
                {
                    Node node = neighbour.GetComponent<Node>();
                    if(hero.getRank() == node.getRank())
                    {
                        inRange = true;
                    }
                }
            }
            if(!inRange)
            {
                return;
            }
            bool princePresent = false;
            ThoraldController thorald = FindObjectOfType<ThoraldController>();
            if(thorald != null && thorald.currentPosition.GetComponent<Node>().getRank() == currentPosition.GetComponent<Node>().getRank())
            {
                princePresent = true;
            }
            //Search for heroes to join the fight
            GameManager gm = FindObjectOfType<GameManager>();
            List<Hero> heroes = new List<Hero>();
            foreach(GameObject g in gm.HeroList)
            {
                Hero h = g.GetComponent<HeroController>().getHero();
                if(h.Equals(hero))
                {
                    break;
                }
                if(h.getRank() == currentPosition.GetComponent<Node>().getRank())
                {
                    heroes.Add(h);
                }
                else if(h.getHeroKind() == HeroKind.Archer || hero.IsCarrying(new Bow(ItemWeight.Heavy)))
                {
                    foreach (GameObject neighbour in currentPosition.GetComponent<Node>().getNeighbours())
                    {
                        Node node = neighbour.GetComponent<Node>();
                        if (h.getRank() == node.getRank())
                        {
                            heroes.Add(h);
                            break;
                        }
                    }
                }
            }
            CombatManager.instance.setMonsterFighterID(currentPosition.GetComponent<Node>().getRank());
            CombatParty party = new CombatParty(hero);
            StartCoroutine(CombatManager.instance.CallForHelp(party, heroes, monster, princePresent));
            //Combat combat = new Combat(party, monster, princePresent);
            //CombatManager.instance.StartCombatRound(combat);
        }

        public void setPosition(){
            Vector3 pos = currentPosition.transform.position;
            pos.x += 0.25f;
            pos.z += 0.25f;
            this.transform.position = pos;

        }

        public void setNode(GameObject pos){
            currentPosition = pos;
            setPosition(); 
        } 

        public void monsterMove(){
            GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
            GameObject nextSpace = currentPosition.GetComponent<Node>().getLowestRankNeighbour();
        
            this.currentPosition = nextSpace;
            setPosition();
            if(currentPosition.GetComponent<Node>().getRank() == 0){
                
                GM.attackCastle();
                this.gameObject.SetActive(false);
            }

            foreach(GameObject farmer in GM.getFarmerList()){
                if(farmer.GetComponent<FarmerController>().getCurrentPosition().GetComponent<Node>().getRank() == currentPosition.GetComponent<Node>().getRank()){
                    farmer.SetActive(false);
                }
            
            }
        }


        void OnMouseOver()
        {
            monsterNameUI.GetComponent<UnityEngine.UI.Text>().text = monster.getMonsterName();

        
            monsterWPUI.GetComponent<UnityEngine.UI.Text>().text = monster.getWP().ToString();

        
            monsterSPUI.GetComponent<UnityEngine.UI.Text>().text = monster.getSP().ToString();

            
        }

        void OnMouseExit()
        {
            monsterNameUI.GetComponent<UnityEngine.UI.Text>().text = "";

        
            monsterWPUI.GetComponent<UnityEngine.UI.Text>().text = "";

        
            monsterSPUI.GetComponent<UnityEngine.UI.Text>().text = "";

            
        }

        public GameObject getCurrentPosition()
        {
            return currentPosition;
        }
    }
}


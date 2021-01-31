using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;
using System.Runtime.InteropServices;

namespace Scripts
{
    public class HeroController : MonoBehaviour
    {
        public PlayerSetUp psu;
        public PhotonView PV;
        public HeroKind heroKind;
        Hero hero;

        [SerializeField]
        public GameObject currentPosition;

        [SerializeField]
        BoardManager Board;

        [SerializeField]
        TimeTrackController timeTrack;

        [SerializeField]
        public GameObject nameUI;

        [SerializeField]
        public GameObject wpUI;

        [SerializeField]
        public GameObject spUI;

        [SerializeField]
        public GameObject goldUI;

        [SerializeField]
        public GameObject farmerUI;

        public bool hasEndedDay =  false;


        public List<ItemIcon> items = new List<ItemIcon>();
        List<GameObject> farmersCarried = new List<GameObject>();

        //Is here for testing and state machine will create all hero and monster related stuff

        void Start()
        {
            PV = GetComponent<PhotonView>();
        }

        void Update(){

            if(PV.IsMine){
                nameUI.GetComponent<Text>().text = GetName();
                wpUI.GetComponent<Text>().text = hero.getWillpowerPoints().ToString();
                spUI.GetComponent<Text>().text = hero.getStrengthPoints().ToString();
                goldUI.GetComponent<Text>().text = hero.getGold().ToString();
                farmerUI.GetComponent<Text>().text = farmersCarried.Count.ToString();
            }
        }

        public string GetName(){
            if(heroKind == HeroKind.Archer){
                return "Archer";

            } else if ( heroKind == HeroKind.Dwarf){
                return "Dwarf";

            }else if (heroKind == HeroKind.Warrior){
                return "Warrior";
            } else {
                return "Wizard";
            }
        }

        public void CallForHelp(MonsterController monster)
        {


        }

        public void show() {
            if (timeTrack.getTimeLeft() > 0) {
                foreach (GameObject x in currentPosition.GetComponent<Node>().getNeighbours()) {
                    x.GetComponent<MeshRenderer>().enabled = true;
                    x.GetComponent<BoxCollider>().enabled = true;
                }
            }
        }
        public void hide() {
            foreach (GameObject x in currentPosition.GetComponent<Node>().getNeighbours()) {
                x.GetComponent<MeshRenderer>().enabled = false;
                x.GetComponent<BoxCollider>().enabled = false;
            }
        }

        public void heroMove(GameObject newPos) {
            hide();
            if (hero.FreeMoveSpaces > 0)
            {
                hero.FreeMoveSpaces--;
            }
            else
            {
                timeTrack.tick();
                if(timeTrack.getTime() > 7){
                    if(GameObject.Find("GameManager").GetComponent<GameManager>().getEventsApplied().Contains(EventKind.Event19)&& timeTrack.getTime() > 8)
                    {
                        hero.setWillpowerPoints(hero.getWillpowerPoints() - 3);
                    }else if (GameObject.Find("GameManager").GetComponent<GameManager>().getEventsApplied().Contains(EventKind.Event26) && timeTrack.getTime() == 8)
                    {
                        // do nothing
                    }
                    else
                    {
                        hero.setWillpowerPoints(hero.getWillpowerPoints() - 2);
                    }
                }
            }
            setCurrentPosition(newPos);

            hero.setRank(newPos.GetComponent<Node>().getRank());
           
            if(PV.IsMine){
                PV.RPC("updateRank", RpcTarget.AllBuffered, hero.getRank());
            }

            foreach (GameObject farmer in farmersCarried) {
                farmer.GetComponent<FarmerController>().setNode(newPos);
            }

            // pick up items on the ground on specific region
            if (newPos.GetComponent<Node>().GetRegion().getRank() == 72 && newPos.GetComponent<Node>().GetRegion().hasItem())
            {
                List<Item> itemOnGround = newPos.GetComponent<Node>().GetRegion().getItems();
                foreach(Item i in itemOnGround.ToArray())
                {
                    if (i.GetType() == typeof(Wineskin))
                    {
                        hero.pickUpItemOnGround(newPos.GetComponent<Node>().GetRegion(),new Wineskin(ItemWeight.Light));
                        GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(EventKind.Event30, false, new Vector3(0,0,0), null, null);
                    }
                }
            }
            if (newPos.GetComponent<Node>().GetRegion().getRank() == 57 && newPos.GetComponent<Node>().GetRegion().hasItem())
            {
                List<Item> itemOnGround = newPos.GetComponent<Node>().GetRegion().getItems();
                foreach (Item i in itemOnGround.ToArray())
                {
                    if (i.GetType() == typeof(Shield))
                    {
                        hero.pickUpItemOnGround(newPos.GetComponent<Node>().GetRegion(), new Shield(ItemWeight.Heavy));
                        GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(EventKind.Event29, false, new Vector3(0, 0, 0), null, null);
                    }
                }
            }
            // track some events
            if (newPos.GetComponent<Node>().GetRegion().getRank() >= 22 && newPos.GetComponent<Node>().GetRegion().getRank() <= 24)
            {
                if (GameObject.Find("R (24)").GetComponent<Node>().GetRegion().getEvents().Contains(EventKind.Event21))
                {
                    hero.setWillpowerPoints(hero.getWillpowerPoints() - 4);
                    GameObject.Find("R (24)").GetComponent<Node>().GetRegion().removeEvent(EventKind.Event21);
                    GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(EventKind.Event21, false, new Vector3(0, 0, 0), "", "");
                }
            }
            if (newPos.GetComponent<Node>().GetRegion().getRank() == 57 && newPos.GetComponent<Node>().GetRegion().getEvents()!=null)
            {
                List<EventKind> list = newPos.GetComponent<Node>().GetRegion().getEvents();
                foreach (EventKind ek in list.ToArray())
                {
                    if (ek == EventKind.Event8)
                    {
                        GameObject EventUI = GameObject.Find("EventParent").transform.GetChild(0).gameObject;
                        GameObject DUI = null;
                        GameObject EUI = null;
                        GameObject YesButton = null;
                        GameObject NoButton = null;
                        for (int i = 0; i < EventUI.transform.childCount; i++)
                        {
                            if (EventUI.transform.GetChild(i).transform.name == "Description")
                            {
                                DUI = EventUI.transform.GetChild(i).gameObject;
                            }
                            if (EventUI.transform.GetChild(i).transform.name == "Effect")
                            {
                                EUI = EventUI.transform.GetChild(i).gameObject;
                            }
                            if (EventUI.transform.GetChild(i).transform.name == "EventYes")
                            {
                                YesButton = EventUI.transform.GetChild(i).gameObject;
                            }
                            if (EventUI.transform.GetChild(i).transform.name == "EventNo")
                            {
                                NoButton = EventUI.transform.GetChild(i).gameObject;
                            }
                        }
                        DUI.GetComponent<Text>().text = "";
                        EUI.GetComponent<Text>().text = "Want to buy 2 wp with 2 golds?";
                        YesButton.SetActive(true);
                        NoButton.SetActive(true);
                        EventUI.SetActive(true);
                    }
                    if(ek == EventKind.Event3)
                    {
                        hero.setStrengthPoints(hero.getStrengthPoints() + 1);
                        GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(EventKind.Event3, false,new Vector3(0,0,0), "", "");
                        newPos.GetComponent<Node>().GetRegion().removeEvent(EventKind.Event3);
                    }
                }
            }
            if (newPos.GetComponent<Node>().GetRegion().getHerb()&& newPos.GetComponent<Node>().GetRegion().getRank()!=0)
            {
                newPos.GetComponent<Node>().GetRegion().setHerb(false);
                GameObject FogInfo = GameObject.Find("FogInfo").transform.GetChild(0).gameObject;
                GameObject DUI = null;
                string toDisplay = "Herb picked"; 
                for (int i = 0; i < FogInfo.transform.childCount - 1; i++)
                {
                    if (FogInfo.transform.GetChild(i).transform.name == "Description")
                    {
                        DUI = FogInfo.transform.GetChild(i).gameObject;
                    }
                }
                DUI.GetComponent<Text>().text = toDisplay;
                FogInfo.SetActive(true);
                hero.setHerb(true);
                GameObject.Find("GameManager").GetComponent<GameManager>().updateHerb(false, new Vector3(0, 0, 0));
            }
            if (newPos.GetComponent<Node>().GetRegion().getRank() ==0&& hero.getHerb())
            {
                hero.setHerb(false);
                newPos.GetComponent<Node>().GetRegion().setHerb(true);
                GameObject FogInfo = GameObject.Find("FogInfo").transform.GetChild(0).gameObject;
                GameObject DUI = null;
                string toDisplay = "Herb dropped";
                for (int i = 0; i < FogInfo.transform.childCount - 1; i++)
                {
                    if (FogInfo.transform.GetChild(i).transform.name == "Description")
                    {
                        DUI = FogInfo.transform.GetChild(i).gameObject;
                    }
                }
                DUI.GetComponent<Text>().text = toDisplay;
                FogInfo.SetActive(true);
                hero.setHerb(false);
                GameObject.FindObjectOfType<GameManager>().HerbInCastle = true;
            }
            if (newPos.GetComponent<Node>().GetRegion().hasItem())
            {
                List<Item> items = newPos.GetComponent<Node>().GetRegion().getItems();
                Item toPick = null;
                foreach(Item i in items)
                {
                    if (i.GetType() == typeof(Runestone))
                    {
                        toPick = i;
                    }
                }
                if (toPick != null)
                {
                    hero.pickUpItemOnGround(newPos.GetComponent<Node>().GetRegion(), toPick);
                    GameObject RunestoneUI = GameObject.Find("RunestoneUI").transform.GetChild(0).gameObject;
                    GameObject DUI = null;
                    string toDisplay = "One rune stone found and picked up!";
                    for (int i = 0; i < RunestoneUI.transform.childCount - 1; i++)
                    {
                        if (RunestoneUI.transform.GetChild(i).transform.name == "Description")
                        {
                            DUI = RunestoneUI.transform.GetChild(i).gameObject;
                        }
                    }
                    DUI.GetComponent<Text>().text = toDisplay;
                    RunestoneUI.SetActive(true);
                }
            }

            // when step on a fog, reveal and activate it.
            GameObject gameManager = GameObject.Find("GameManager");
            
            foreach (TileUnit u in newPos.GetComponent<Node>().GetRegion().getTileUnits().ToArray())
            {
                if (u.GetType() == typeof(Fog))
                {
                    ((Fog)u).reveal();
                    // update() in gameManager calls every frame, I don't know if that means each method, so here I call manually

                    //gameManager.GetComponent<GameManager>().updateFog(); 
                    ((Fog)u).activate(this.hero);
                    //gameManager.GetComponent<GameManager>().updateFog();
                }

                
            }

            foreach(GameObject m in gameManager.GetComponent<GameManager>().getMonsterList()){
                if(m.GetComponent<MonsterController>().getCurrentPosition().GetComponent<Node>().getRank() == currentPosition.GetComponent<Node>().getRank()){
                    foreach (GameObject farmer in farmersCarried) {
                        farmer.SetActive(false);
                    }
                    farmersCarried = new List<GameObject>();
                }
            }

            if (newPos.GetComponent<Node>().getRank() == 0) {
                for (int i = 0; i < farmersCarried.Count; i++) {
                    gameManager.GetComponent<GameManager>().getCastle().addGoldenShield();
                    farmersCarried[i].SetActive(false);
                }
                gameManager.GetComponent<GameManager>().getCastleHealth().GetComponent<UnityEngine.UI.Text>().text = gameManager.GetComponent<GameManager>().getCastle().getNumGoldenShields().ToString();
                farmersCarried = new List<GameObject>();
            }
        }

        public void RevealAdjacentFog()
        {
            foreach(GameObject node in currentPosition.GetComponent<Node>().getNeighbours())
            {
                foreach (TileUnit u in node.GetComponent<Node>().GetRegion().getTileUnits().ToArray())
                {
                    if (u.GetType() == typeof(Fog))
                    {
                        ((Fog)u).reveal();
                        //((Fog)u).activate(this.hero);
                    }
                }
            }
        }

        [PunRPC]
        public void updateRank(int newPos){
            psu.changeMove = true;
            psu.changePos = newPos;

        }

        public void setBoard(BoardManager board) {
            Board = board;
        }

        public void setCurrentPosition(GameObject curr) {
            currentPosition = curr;
            //hero.setRank()
            setHeroPosition();
        }

        public void setTimeTrack(TimeTrackController ttc) {
            timeTrack = ttc;
        }

        public void TimeTrackTick()
        {
            timeTrack.tick();
        }

        public bool HasTimeLeft()
        {
            return timeTrack.getTimeLeft() > 0;
        }

        void setHeroPosition() {
            this.transform.position = currentPosition.transform.position;
        }

        public GameObject getCurrentPosition()
        {
            return currentPosition; 
        }
        
        int grabbedAtRank = -1;
        public void grabFarmer()
        {
            if(grabbedAtRank == hero.getRank()){
                return;
            }
            GameObject gameManager = GameObject.Find("GameManager");
            //find well
            GameObject farmerFound = null;
            foreach (GameObject farmer in gameManager.GetComponent<GameManager>().getFarmerList())
            {
                if (farmer.GetComponent<FarmerController>().getCurrentPosition().GetComponent<Node>().getRank() == hero.getRank())
                {
                    farmersCarried.Add(farmer);
                    farmer.GetComponent<FarmerController>().getFarmer().setGuider(hero);
                    grabbedAtRank = hero.getRank();
                }
            }
        }

        public void dropFarmer()
        {
            if(farmersCarried.Count > 0)
            {
                grabbedAtRank = -1;
            }
            foreach (GameObject farmer in farmersCarried)
            {
                farmer.GetComponent<FarmerController>().currentPosition = currentPosition;
                farmer.GetComponent<FarmerController>().setPosition();
            }
            farmersCarried = new List<GameObject>();
            Debug.Log("Dropped");
        }

        public void UpdateItemIcons(Item item, int index)
        {
            items[index].SetItem(item);
            //Debug.Log(index);
        }


        public void useWell(){
            GameObject gameManager= GameObject.Find("GameManager");
            //find well
            
            GameObject wellFound = null;
            foreach(GameObject well in gameManager.GetComponent<GameManager>().getWellList()){
                if(well.GetComponent<WellController>().getCurrentPosition().GetComponent<Node>().getRank() == hero.getRank() && !(well.GetComponent<WellController>().getWell().getUsedToday())){
                    wellFound = well;
                    break;
                }
                
            }
            Debug.Log(hero.getWillpowerPoints());
            if(wellFound != null){
                hero.setWillpowerPoints(hero.getWillpowerPoints() + 3);
                wellFound.GetComponent<WellController>().getWell().setUsedToday(true);
                wellFound.active = false;

            }
            Debug.Log(hero.getWillpowerPoints());

        }

        public Hero getHero(){
            return hero;
        }

        public void setHero(Hero pHero){
            hero = pHero;
        }

        public int GetHeroID(){
            if(heroKind == HeroKind.Archer){
                return 0;
            } else if ( heroKind == HeroKind.Dwarf){
                return 1;
            }else if (heroKind == HeroKind.Warrior){
                return 2;
            } else {
                return 3;
            }
        }


        
    }
}


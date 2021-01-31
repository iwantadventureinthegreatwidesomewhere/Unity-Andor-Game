using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
namespace Scripts{
public class PlayerSetUp : MonoBehaviour
{

    private PhotonView PV;
    public GameObject hero;

    public Instantiator instantiator;
    public GameObject ttHero;
    public Hero hero1;
    private GameManager GM;

    public int current_rank;

    public int current_tt;

    //changed pos
    public bool changeMove = false;
    public int changePos;


    //ui for non free actions
    GameObject EndTurnBtn;
    GameObject MoveBtn;
    GameObject FightBtn;

    GameObject WellDrinkBtn;

    GameObject pickUpFarmerBtn;
    GameObject dropFarmerBtn;
    GameObject endDayBtn;

    public int id;
    void Start() {
        PV =  GetComponent<PhotonView>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        instantiator = GameObject.Find("God").GetComponent<Instantiator>();

        
        
        if(PV.IsMine){
            int pid = HeroSelection.HS.mySelectedCharacter;
            PV.RPC("SetUpPlayer", RpcTarget.All, pid);

            if(id == 0){
                hero = PhotonNetwork.Instantiate(Path.Combine("Heroes", "Archer"), new Vector3(0, 0, 0), Quaternion.identity);
                ttHero = PhotonNetwork.Instantiate(Path.Combine("TimeTrack", "ArcherTimetrack"), new Vector3(0, 0, 0), Quaternion.identity);
            } else if(id == 1){
                hero = PhotonNetwork.Instantiate(Path.Combine("Heroes", "Dwarf"), new Vector3(0, 0, 0), Quaternion.identity);
                ttHero = PhotonNetwork.Instantiate(Path.Combine("TimeTrack", "DwarfTimetrack"), new Vector3(0, 0, 0), Quaternion.identity);
            } else if(id == 2){
                hero = PhotonNetwork.Instantiate(Path.Combine("Heroes", "Warrior"), new Vector3(0, 0, 0), Quaternion.identity);
                ttHero = PhotonNetwork.Instantiate(Path.Combine("TimeTrack", "WarriorTimetrack"), new Vector3(0, 0, 0), Quaternion.identity);
            } else {
                hero = PhotonNetwork.Instantiate(Path.Combine("Heroes", "Wizard"), new Vector3(0, 0, 0), Quaternion.identity);
                ttHero = PhotonNetwork.Instantiate(Path.Combine("TimeTrack", "WizardTimetrack"), new Vector3(0, 0, 0), Quaternion.identity);
            }

            

            if(id == 0){
               hero1 = new Hero(GameManager.game, HeroKind.Archer, hero.GetComponent<HeroController>());
            } else if(id == 1){
                hero1 = new Hero(GameManager.game, HeroKind.Dwarf, hero.GetComponent<HeroController>());
            } else if(id == 2){
                hero1 = new Hero(GameManager.game, HeroKind.Warrior, hero.GetComponent<HeroController>());
            } else {
                hero1 = new Hero(GameManager.game, HeroKind.Wizard, hero.GetComponent<HeroController>());
            }

            hero1.setupHero();
            hero.GetComponent<HeroController>().setCurrentPosition(GameManager.Board.getNodeWithRank(hero1.getRank()));
            hero.GetComponent<HeroController>().setBoard(GameManager.Board);
            hero.GetComponent<HeroController>().setHero(hero1);
            
            hero.GetComponent<HeroController>().setTimeTrack(ttHero.GetComponent<TimeTrackController>());

            hero.GetComponent<HeroController>().nameUI = GameObject.Find("HeroNameText");
            hero.GetComponent<HeroController>().wpUI = GameObject.Find("WillpowerValueText");
            hero.GetComponent<HeroController>().spUI = GameObject.Find("StrengthPointValueText");
            hero.GetComponent<HeroController>().goldUI = GameObject.Find("GoldValueText");
            hero.GetComponent<HeroController>().farmerUI = GameObject.Find("FarmerValueText");
            hero.GetComponent<HeroController>().psu = this.GetComponent<PlayerSetUp>();
            ttHero.GetComponent<TimeTrackController>().setTTM(GM.gameObject.GetComponent<TimeTrackManager>());

            hero.GetComponent<HeroController>().items.Add(GameObject.Find("ItemIcon").GetComponent<ItemIcon>());
            hero.GetComponent<HeroController>().items.Add(GameObject.Find("ItemIcon (1)").GetComponent<ItemIcon>());
            hero.GetComponent<HeroController>().items.Add(GameObject.Find("ItemIcon (2)").GetComponent<ItemIcon>());
            hero.GetComponent<HeroController>().items.Add(GameObject.Find("ItemIcon (3)").GetComponent<ItemIcon>());
            hero.GetComponent<HeroController>().items.Add(GameObject.Find("ItemIcon (4)").GetComponent<ItemIcon>());
            //ttHero.GetComponent<TimeTrackController>().ResetTurn(); 
            MoveBtn =GameObject.Find("MoveBtn");
            FightBtn =GameObject.Find("FightButton");
            EndTurnBtn = GameObject.Find("EndTurnbtn");
            pickUpFarmerBtn = GameObject.Find("PickUpFarmer");
            dropFarmerBtn = GameObject.Find("DropFarmer");
            endDayBtn = GameObject.Find("EndDayBtn");



            WellDrinkBtn = GameObject.Find("EndTurnbtn");

            MoveBtn.SetActive(false);
            FightBtn.SetActive(false);
            EndTurnBtn.SetActive(false);
            pickUpFarmerBtn.SetActive(false);
            dropFarmerBtn.SetActive(false);
            endDayBtn.SetActive(false);

            GM.HeroList.Add(hero);
            GM.HeroTTList.Add(ttHero);
            
            
        } else {
            hero = GameObject.Find(GM.GetHeroByPID(id));
            ttHero = GameObject.Find(GM.GetHeroTTByPID(id));

            if(id == 0){
               hero1 = new Hero(GameManager.game, HeroKind.Archer, hero.GetComponent<HeroController>());
            } else if(id == 1){
                hero1 = new Hero(GameManager.game, HeroKind.Dwarf, hero.GetComponent<HeroController>());
            } else if(id == 2){
                hero1 = new Hero(GameManager.game, HeroKind.Warrior, hero.GetComponent<HeroController>());
            } else {
                hero1 = new Hero(GameManager.game, HeroKind.Wizard, hero.GetComponent<HeroController>());
            }

            hero1.setupHero();
            hero.GetComponent<HeroController>().setCurrentPosition(GameManager.Board.getNodeWithRank(hero1.getRank()));
            hero.GetComponent<HeroController>().setBoard(GameManager.Board);
            hero.GetComponent<HeroController>().setHero(hero1);

            hero.GetComponent<HeroController>().psu = this.GetComponent<PlayerSetUp>();
            
            hero.GetComponent<HeroController>().setTimeTrack(ttHero.GetComponent<TimeTrackController>());
            ttHero.GetComponent<TimeTrackController>().setTTM(GM.gameObject.GetComponent<TimeTrackManager>());
            //ttHero.GetComponent<TimeTrackController>().ResetTurn(); 

            GM.HeroList.Add(hero);
        }  

        if(GM.HeroList.Count == RoomCustomMatch.room.playersInRoom){
            GM.SetTurnOrder();
        }
        
    }
    
    
    bool setTurnUp = false;
    void FixedUpdate(){
            if(!GM.pause){
                if(changeMove){
                Debug.Log(changePos);
                if(PV.IsMine){
                    changeMove = false;
                }else {
                    instantiator.updateMove(hero.GetComponent<HeroController>(), changePos);
                    changeMove = false;
                
                }
                
            }

            if(PV.IsMine){ 
                if(hero.GetComponent<HeroController>().hasEndedDay){
                    MoveBtn.SetActive(false);
                    FightBtn.SetActive(false);
                    EndTurnBtn.SetActive(false);
                    pickUpFarmerBtn.SetActive(false);
                    dropFarmerBtn.SetActive(false);
                    endDayBtn.SetActive(false);
                    GM.nextTurn();
                    setTurnUp = false;
                } else {
                    if(id == GM.turnOrder[GM.turnTick] && !setTurnUp){
                        MoveBtn.SetActive(true);
                        FightBtn.SetActive(true);
                        EndTurnBtn.SetActive(true);
                        pickUpFarmerBtn.SetActive(true);
                        dropFarmerBtn.SetActive(true);
                        endDayBtn.SetActive(true);
                        setTurnUp = true;
                    } else if(id != GM.turnOrder[GM.turnTick] && setTurnUp){
                        MoveBtn.SetActive(false);
                        FightBtn.SetActive(false);
                        EndTurnBtn.SetActive(false);
                        pickUpFarmerBtn.SetActive(false);
                        dropFarmerBtn.SetActive(false);
                        endDayBtn.SetActive(false);
                        setTurnUp = false;
                    }
                }
            }
        }
    }
    


    [PunRPC]
    void SetUpPlayer( int pid){
        id = pid;
    }
}
}

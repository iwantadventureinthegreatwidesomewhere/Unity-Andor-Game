using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace Scripts
{
    public class CombatManager : MonoBehaviour
    {
        public static CombatManager instance;
        //For archer/bow
        public bool DoneRolling { get; set; }

        //TODO:
        //Each player in combat should have their own instance of the following:
        //  diceButton, doneRollingButton, continueButton, retreatButton, okButton, goldButton, WPButton

        //These should be displayed to every player in combat:
        //  combatStatusText, heroBV, monsterBV, panel

        public GameObject panel;
        public GameObject diePrefab;
        public GameObject blackDiePrefab;

        public FightButton fightButton;
        public DiceButton diceButton;
        public DoneRollingButton doneRollingButton;
        public ContinueButton continueButton;
        public RetreatButton retreatButton;
        public OKButton okButton;
        public GoldButton goldButton;
        public WPButton WPButton;
        public Text combatStatusText;
        public Text heroBV;
        public Text monsterBV;

        //Store value of rolls so they can be modified later (eg. by wizard ability)
        public List<DieValue> heroRolls = new List<DieValue>();

        PhotonView PV;

        Instantiator God;

        //setUp for online sync
        public int monsterSyncBV;
        public int heroSyncBV;

        public int battleHeroID;

        public int MonsterFighterID;

        public bool battleUpdate = false;

        public void setBVSync(int pmonsterSyncBV, int pheroSyncBV){
            PV.RPC("BVsync", RpcTarget.AllBuffered, pmonsterSyncBV, pheroSyncBV);
        }
        [PunRPC]
        void BVsync(int BVM, int BVH){
            heroSyncBV = BVH;
            monsterSyncBV = BVM;
            battleUpdate  = true;
        }

        public void setMonsterFighterID(int id){
            PV.RPC("monsterIDSync", RpcTarget.AllBuffered, id);
        }

        [PunRPC]
        void monsterIDSync(int id){
            MonsterFighterID = id;
        }

        public void setBattleHeroId(int id){
            PV.RPC("heroIDSync", RpcTarget.AllBuffered, id);
        }

        [PunRPC]
        void heroIDSync(int id){
            battleHeroID = id;
        }

        //in case of recover

        public int heroRetreaterID;

        bool recover = false;

        public void setRetreatHeroID(int hrID){
            PV.RPC("recSyncHero", RpcTarget.AllBuffered, hrID);
        }

        [PunRPC]
        public void recSyncHero( int hrID){
            heroRetreaterID = hrID;
            recover = true;
        }
        void FixedUpdate(){
            if(battleUpdate){
                God.updateCombat();
                battleUpdate = false;
            }

            if(recover){
                God.updateRecover();
                recover = false;
            }
        }



        void Start()
        {
            PV = GetComponent<PhotonView>();
            God = GameObject.Find("God").GetComponent<Instantiator>();
            instance = this;
        }
        
        public void StartCombatRound(Combat combat)
        {
            //TODO: These UI updates should happen to every player in combat
            ClearPanel();
            panel.SetActive(true);
            combatStatusText.text = "In Battle";
            fightButton.gameObject.SetActive(false);
            continueButton.gameObject.SetActive(false);
            retreatButton.gameObject.SetActive(false);
            goldButton.gameObject.SetActive(false);
            WPButton.gameObject.SetActive(false);

            StartCoroutine(combat.StartCombatRound());
        }
        //Call at the start of each hero's attack
        public void StartHeroAttack()
        {
            heroRolls = new List<DieValue>();
            //TODO: display this only for the current player
            diceButton.gameObject.SetActive(true);
        }

        public void EndCombatRound()
        {
            //TODO: display for each player in combat
            continueButton.gameObject.SetActive(true);
            retreatButton.gameObject.SetActive(true);
        }

        public void EndCombat()
        {
            //TODO: These UI updates should happen to every player in combat
            fightButton.gameObject.SetActive(true);
            diceButton.gameObject.SetActive(false);
            doneRollingButton.gameObject.SetActive(false);
            continueButton.gameObject.SetActive(false);
            retreatButton.gameObject.SetActive(false);
            goldButton.gameObject.SetActive(false);
            WPButton.gameObject.SetActive(false);
            ClearPanel();
            combatStatusText.text = "";
        }

        public void DisplayDoneRollingButton(bool toggle)
        {
            //TODO: display to current player
            doneRollingButton.gameObject.SetActive(toggle);
        }

        public void DisplayRetreatButton()
        {
            //TODO: display to all players
            retreatButton.gameObject.SetActive(true);
        }

        public void DisplayRewardButtons()
        {
            //TODO: display to party leader
            goldButton.gameObject.SetActive(true);
            WPButton.gameObject.SetActive(true);
        }

        public void DisplayOKButton()
        {
            //TODO: display to all players
            okButton.gameObject.SetActive(true);
        }

        public void SetCombatStatusText(string text)
        {
            //TODO: display to all players
            combatStatusText.text = text;
        }

        public void SetHeroBV(string text)
        {
            //TODO: display to all players
            heroBV.text = text;
        }

        public void SetMonsterBV(string text)
        {
            //TODO: display to all players
            monsterBV.text = text;
        }

        private void ClearPanel()
        {
            foreach (Transform child in panel.transform)
            {
                if(child.GetComponent<Die>() != null)
                {
                    Destroy(child.gameObject);
                }
            }
            //TODO: update for all players
            heroBV.text = "";
            monsterBV.text = "";
            panel.SetActive(false);
        }

        #region Buttons

        public bool DiceButtonPressed()
        {
            bool pressed = diceButton.Pressed;
            diceButton.Pressed = false;
            return pressed;
        }

        public bool DoneRollingButtonPressed()
        {
            bool pressed = doneRollingButton.Pressed;
            doneRollingButton.Pressed = false;
            return pressed;
        }

        public bool ContinueButtonPressed()
        {
            bool pressed = continueButton.Pressed;
            continueButton.Pressed = false;
            return pressed;
        }

        public bool FightButtonPressed()
        {
            bool pressed = fightButton.Pressed;
            fightButton.Pressed = false;
            return pressed;
        }

        public bool RetreatButtonPressed()
        {
            bool pressed = retreatButton.Pressed;
            retreatButton.Pressed = false;
            return pressed;
        }

        public bool OKButtonPressed()
        {
            bool pressed = okButton.Pressed;
            okButton.Pressed = false;
            return pressed;
        }

        public bool GoldButtonPressed()
        {
            bool pressed = goldButton.Pressed;
            goldButton.Pressed = false;
            return pressed;
        }

        public bool WPButtonPressed()
        {
            bool pressed = WPButton.Pressed;
            WPButton.Pressed = false;
            return pressed;
        }

        #endregion

        public List<int> HeroRoll(int numRegularDice, int numBlackDice)
        {
            List<int> rolls = new List<int>();
            for(int i = 0; i < numRegularDice + numBlackDice; i++)
            {
                GameObject die;
                if (i < numRegularDice)
                {
                    die = Instantiate(diePrefab);
                }
                else
                {
                    die = Instantiate(blackDiePrefab);
                }
                die.GetComponent<RectTransform>().SetParent(panel.transform);
                die.GetComponent<RectTransform>().anchoredPosition = new Vector3(-50 + 30 * i, 35);
                rolls.Add(die.GetComponent<Die>().Roll());
                //Store roll for later
                heroRolls.Add(die.GetComponent<Die>().GetValue());
            }
            return rolls;
        }

        public List<int> MonsterRoll(int numRegularDice, int numBlackDice)
        {
            List<int> rolls = new List<int>();
            for (int i = 0; i < numRegularDice + numBlackDice; i++)
            {
                GameObject die;
                if (i < numRegularDice)
                {
                    die = Instantiate(diePrefab);
                }
                else
                {
                    die = Instantiate(blackDiePrefab);
                }
                die.GetComponent<RectTransform>().SetParent(panel.transform);
                die.GetComponent<RectTransform>().anchoredPosition = new Vector3(-50 + 30 * i, -40);
                rolls.Add(die.GetComponent<Die>().Roll());
            }
            return rolls;
        }

        public void ResolveEvent6(Hero hero)
        {
            StartCoroutine(WaitForDiceRoll(hero));
        }

        //TODO: diceButton and doneRolling button should be displayed only to hero
        public IEnumerator WaitForDiceRoll(Hero hero)
        {
            diceButton.gameObject.SetActive(true);
            doneRollingButton.gameObject.SetActive(true);
            int roll = 0;
            while (true)
            {
                if(DiceButtonPressed())
                {
                    GameObject uiPanel = GameObject.Find("UI");
                    GameObject die = Instantiate(diePrefab);
                    die.GetComponent<RectTransform>().SetParent(uiPanel.transform);
                    die.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0);
                    roll = die.GetComponent<Die>().Roll();
                    Destroy(die, 2);
                    break;
                }
                else if(DoneRollingButtonPressed())
                {
                    break;
                }
                yield return null;
            }
            if (roll <= 0)
            {

            }
            else if (roll <= 4)
            {
                hero.TakeDamage(roll);
            }
            else
            {
                hero.Heal(roll);
            }
            diceButton.gameObject.SetActive(false);
            doneRollingButton.gameObject.SetActive(false);
        }

        public IEnumerator CallForHelp(CombatParty party, List<Hero> heroes, Monster monster, bool princePresent)
        {
            //TODO: each player has their own buttons
            JoinButton join = GameObject.Find("CallForHelpDisplay").GetComponentInChildren<JoinButton>();
            IgnoreButton ignore = GameObject.Find("CallForHelpDisplay").GetComponentInChildren<IgnoreButton>();
            foreach(Hero hero in heroes)
            {
                combatStatusText.text = party.GetLeader().getHeroKind() + " is fighting " + monster.getMonsterName();
                while(true)
                {
                    if(join.Pressed)
                    {
                        party.AddHero(hero);
                        join.Pressed = false;
                        join.gameObject.SetActive(false);
                        ignore.gameObject.SetActive(false);
                        break;
                    }
                    else if(ignore.Pressed)
                    {
                        ignore.Pressed = false;
                        join.gameObject.SetActive(false);
                        ignore.gameObject.SetActive(false);
                        break;
                    }
                    yield return null;
                }
            }
            Combat combat = new Combat(party, monster, princePresent);
            StartCombatRound(combat);
        }
    }
}


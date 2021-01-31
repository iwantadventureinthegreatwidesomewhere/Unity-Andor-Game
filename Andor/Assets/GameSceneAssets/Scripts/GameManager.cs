using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.Runtime.InteropServices;

namespace Scripts{
    public class GameManager : MonoBehaviour
    {
        PhotonView PV;

        public static GameManager GM;
        //turn order
        public List<int> turnOrder; //each int is a player id

        public List<int> nextTurnOrder = new List<int>();
        public int turnTick; 

        public string username;

        public int maxMessages = 25;
        public GameObject chatPanel, textObject;
        public InputField chatbox;

        public Color player;
        public Color info;

        private bool SkralDefeated = false;
        public bool HerbInCastle { get; set; }

        [SerializeField]
        public GameObject princePrefab;

        [SerializeField]
        public GameObject herbPrefab;

        GameObject herb;

        GameObject prince;

        public bool witchFound = false;
        public bool gameEnd = false;

        [SerializeField]
        List<Message> messageList = new List<Message>();

        //Current gamestates will vary depend on how online works
        public enum gameState{
        PLAYING, NEWDAY, VICTORY, GAMEOVER, NEWGAME
        
        } 
        private gameState state;

        [SerializeField]
        public GameObject CastleHealth;
        //Monster prefabs for reference
        [SerializeField]
        GameObject[] prefabsMonster; //0 = Gor, 1 = Skral, 2 = Troll, 3 = Wardrak

        [SerializeField]
        public GameObject[] prefabsHeroes; //0 = Archer, 1 = Dwarf, 2 = Warrior, 3 = Wizard 

        [SerializeField]
        public GameObject[] prefabsTTtokens; //0 = Archer, 1 = Dwarf, 2 = Warrior, 3 = Wizard

        [SerializeField]
        public GameObject wellPrefab;

        [SerializeField]
        public GameObject fogPrefab;

        [SerializeField]
        GameObject eventPrefab;

        [SerializeField]
        GameObject farmerPrefab;

        [SerializeField]
        GameObject merchantPrefab;

        [SerializeField]
        GameObject witchPrefab;

        [SerializeField]
        Material skralTowerMaterial;

        public static Game game;
        
        Castle castle;

        GameDifficulty difficulty;

        [SerializeField]
        public static BoardManager Board;

        [SerializeField]
        List<GameObject> monsterList;
        
        [SerializeField]
        List<GameObject> wellList;

        [SerializeField]
        List<GameObject> FogList;

        [SerializeField]
        List<GameObject> EventList;

        [SerializeField]
        List<EventKind> EventsApplied= new List<EventKind>();

        [SerializeField]
        List<GameObject> FarmerList;

        [SerializeField]
        List<GameObject> MerchantList;

        [SerializeField]
        List<GameObject> WitchList;

        [SerializeField]
        public List<GameObject> HeroList;

        [SerializeField]
        public List<GameObject> HeroTTList;

        [SerializeField]
        List<Event> EventCards=new List<Event>();

        [SerializeField]
        GameObject monsterNameUI;

        [SerializeField]
        GameObject monsterWPUI;

        [SerializeField]
        GameObject monsterSPUI;

        Instantiator instantiator;

        public Event ActiveEvent { get; set; }

        // Start is called before the first frame update
        void Start()
        {   
            GM = GetComponent<GameManager>();
            Board = GetComponent<BoardManager>();
            PV = GetComponent<PhotonView>();  
            username = PlayerPrefs.GetString("MyUsername");
            instantiator = GameObject.Find("God").GetComponent<Instantiator>();
            


            state = gameState.NEWGAME;
            Game newGame = new Game(Legend.Legend2);
            newGame.initializeLegend();
            game = newGame;
            difficulty = game.getDifficulty();
            NewGame();

            //machine will update gameState in a while loop 
            //and switch case
            /*
                while(true){

                    switch(state){
                        case gameState.NEWGAME:
                            newGame();
                            break;
                        case gameState.NEWDAY:
                            newDay();
                            break;
                        case gameState.PLAYING:
                            playing;
                            break;
                        case gameState.GAMEOVER:
                            gameOver();
                            break;
                        case gameState.VICTORY:
                            victory(); 
                            break;
                    }    
                }
            */

            // below for test, reveal all the fogs
            /*
            foreach (GameObject fogObject in FogList)
            {
                fogObject.GetComponent<FogController>().getFog().reveal();
            }
            */
            foreach (EventKind eventkind in System.Enum.GetValues(typeof(EventKind)))
            {
                if (eventkind != EventKind.EventNull)
                {
                    //Debug.Log(eventkind);
                    Event newEvent = new Event(eventkind);
                    EventCards.Add(newEvent);
                }      
            }
            // shuffle EventCards
            shuffleEventCards();

            GameObject.Find("Narrator").GetComponent<NarratorController>().show(false);


        }

        //sync event card shuffle
        

        public void shuffleEventCards(){
           List<Event> toReadd= new List<Event>();
            foreach(Event e in EventCards)
            {

                if (e.getEventKind() == EventKind.Event1)
                {
                    toReadd.Add(e); 
                }
                if (e.getEventKind() == EventKind.Event8)
                {
                    toReadd.Add(e);
                }
                if (e.getEventKind() == EventKind.Event10)
                {
                    toReadd.Add(e);
                }
                if (e.getEventKind() == EventKind.Event16)
                {
                    toReadd.Add(e);
                }
                if (e.getEventKind() == EventKind.Event18)
                {
                    toReadd.Add(e);
                }
                if (e.getEventKind() == EventKind.Event20)
                {
                    toReadd.Add(e);
                }
                if (e.getEventKind() == EventKind.Event23)
                {
                    toReadd.Add(e);
                }
                if (e.getEventKind() == EventKind.Event27)
                {
                    toReadd.Add(e);
                }
                if (e.getEventKind() == EventKind.Event33)
                {
                    toReadd.Add(e);
                }
                if (e.getEventKind() == EventKind.Event34)
                {
                    toReadd.Add(e);
                }
                if (e.getEventKind() == EventKind.Event6)
                {
                    toReadd.Add(e);
                }
            }
            foreach (Event e in toReadd)
            {
                EventCards.Remove(e);
                EventCards.Add(e);
            }
        }

        

        public void nextTurn(){
            turnTick++;

            turnTick = turnTick%turnOrder.Count;


        }


        //needed for safer castle setup
        bool firstSetUp = true;
        public void SetTurnOrder(){
            //find hero with lowest rank
            int lowerBound = 0;
            while(true)
            {   
                int lowest = 90;
                
                HeroController next = null;
                foreach(GameObject h in HeroList){
                    int hRank = h.GetComponent<HeroController>().getCurrentPosition().GetComponent<Node>().getRank();
                    if(hRank >  lowerBound){    
                        if(hRank <= lowest){
                            lowest = hRank;
                            next = h.GetComponent<HeroController>();
                        }
                    }
                }
                lowerBound = lowest;
                turnOrder.Add(next.GetHeroID());
                if(turnOrder.Count == HeroList.Count){
                    break;
                }
            }

            foreach(int id in turnOrder){
                GameObject tt = GameObject.Find(GetHeroTTByPID(id));
                
                tt.GetComponent<TimeTrackController>().ResetTurn();
            }
            if(firstSetUp)
            {
                castleSetUp();
            }
            
        }
        

        public string GetHeroByPID(int pid){
            if(pid == 0){
                return "Archer(Clone)";
            } else if(pid == 1){
                return "Dwarf(Clone)";
            } else if(pid == 2){
                return "Warrior(Clone)";
            } else {
                return "Wizard(Clone)";
            }

        }

        public string GetHeroTTByPID(int pid){
            if(pid == 0){
                return "ArcherTimetrack(Clone)";
            } else if(pid == 1){
                return "DwarfTimetrack(Clone)";
            } else if(pid == 2){
                return "WarriorTimetrack(Clone)";
            } else {
                return "WizardTimetrack(Clone)";
            }

        }


       

        void NewGame(){
            //get current legend in play
            //this will need to be set differently
            
            int i = 0;
            foreach(GameObject node in Board.getAllNodes()){
                node.GetComponent<Node>().setRegion(game.getRegionByRank(i));
                i++;
            }
            //set up rietburg castle health


            foreach(GameObject n in Board.getAllNodes()){
                foreach(TileUnit m in n.GetComponent<Node>().GetRegion().getTileUnits()){
                    //instantiate monsters and fog with all attributes

                    if(m.GetType() == typeof(Fog)){
                        GameObject fog = Instantiate(fogPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                        fog.GetComponent<FogController>().setNode(n);
                        fog.GetComponent<FogController>().setFog((Fog) m);
                        FogList.Add(fog);
                        // plan to make fog a child gameobject of region, so that
                        // it is easier to check the relation between fogs and regions
                        // test below one line
                        /*
                        fog.transform.parent = n.transform;
                        fog.transform.localPosition = new Vector3(0,0, 0);
                        */

                    } else if(m.GetType() == typeof(Monster)){
                        if(m.GetMonsterKind() == MonsterKind.Gor && !m.isHidden()){
                            GameObject mon = Instantiate(prefabsMonster[0], new Vector3(0, 0, 0), Quaternion.identity);
                            mon.GetComponent<MonsterController>().setNode(n);
                            mon.GetComponent<MonsterController>().setMonster((Monster) m);
                            monsterList.Add(mon);
                            mon.GetComponent<MonsterController>().setUIElements(monsterNameUI, monsterWPUI, monsterSPUI);

                        } else if(m.GetMonsterKind() == MonsterKind.Skral && !m.isHidden()){
                            GameObject mon = Instantiate(prefabsMonster[1], new Vector3(0, 0, 0), Quaternion.identity);
                            mon.GetComponent<MonsterController>().setNode(n);
                            mon.GetComponent<MonsterController>().setMonster((Monster) m);
                            monsterList.Add(mon);
                            mon.GetComponent<MonsterController>().setUIElements(monsterNameUI, monsterWPUI, monsterSPUI);

                        } else if(m.GetMonsterKind() == MonsterKind.Troll && !m.isHidden()){
                            GameObject mon = Instantiate(prefabsMonster[2], new Vector3(0, 0, 0), Quaternion.identity);
                            mon.GetComponent<MonsterController>().setNode(n);
                            mon.GetComponent<MonsterController>().setMonster((Monster) m);
                            monsterList.Add(mon);
                            mon.GetComponent<MonsterController>().setUIElements(monsterNameUI, monsterWPUI, monsterSPUI);

                        } else if(m.GetMonsterKind() == MonsterKind.Wardrak && !m.isHidden()){
                            GameObject mon = Instantiate(prefabsMonster[3], new Vector3(0, 0, 0), Quaternion.identity);
                            mon.GetComponent<MonsterController>().setNode(n);
                            mon.GetComponent<MonsterController>().setMonster((Monster) m);
                            monsterList.Add(mon);
                            mon.GetComponent<MonsterController>().setUIElements(monsterNameUI, monsterWPUI, monsterSPUI);
                        }


                    } else if(m.GetType() == typeof(Farmer)){
                        //set up farmers
                        GameObject farmer = Instantiate(farmerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                        farmer.GetComponent<FarmerController>().setNode(n);
                        farmer.GetComponent<FarmerController>().setFarmer((Farmer) m);
                        FarmerList.Add(farmer);

                    }  else if(m.GetType() == typeof(Well)){
                        //set up wells
                        GameObject well = Instantiate(wellPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                        well.GetComponent<WellController>().setNode(n);
                        well.GetComponent<WellController>().setWell((Well) m);
                        wellList.Add(well);

                    } else if(m.GetType() == typeof(Merchant)){
                        //set up merchants
                        GameObject merchant = Instantiate(merchantPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                        merchant.GetComponent<MerchantController>().setNode(n);
                        merchant.GetComponent<MerchantController>().setMerchant((Merchant) m);
                        MerchantList.Add(merchant);
                    } /*else if(m.GetType() == typeof(Castle)){
                        castle = (Castle) m;
                        CastleHealth.GetComponent<UnityEngine.UI.Text>().text = castle.getNumGoldenShields().ToString();

                    }*/
                }
            }

            
            


            //divide starting gold and wineskin
                //will require networking
            
            



            state = gameState.NEWDAY;

        }

        void castleSetUp(){
            castle = new Castle(game.getRegionByRank(0), HeroList.Count);
            CastleHealth.GetComponent<UnityEngine.UI.Text>().text = castle.getNumGoldenShields().ToString();
        }

        public void attackCastle(){
            castle.removeGoldenShield();
            CastleHealth.GetComponent<UnityEngine.UI.Text>().text = castle.getNumGoldenShields().ToString();
        }


        void playing(){
            //each player gets a turn and when they end it is passed to next player
            while(turnOrder.Count != 0){
                





            }
            //check for victory of failure or keep playing
        }


        void gameOver(){
            //disable all controls inform players of failure


        }

        void victory(){
            //disable all controls inform players of success

        }

        public void updateFog()
        {
            List<GameObject> objectToRemove = new List<GameObject>();
            foreach (GameObject fogGameObject in FogList.ToArray())
            {
                Fog fog = fogGameObject.GetComponent<FogController>().getFog();
                // fog is revealed but not activated
                if (fog.isRevealed() && fog.getFogKind() != FogKind.None)
                {
                    if (fogGameObject.transform.childCount != 0) continue;        // already have text
                    if (fog.getFogKind() == FogKind.TwoWP || fog.getFogKind() == FogKind.ThreeWP || fog.getFogKind() == FogKind.Event || fog.getFogKind() == FogKind.SP || fog.getFogKind() == FogKind.Gold || fog.getFogKind() == FogKind.Wineskin)
                    {
                        GameObject child = new GameObject();
                        child.transform.parent = fogGameObject.transform;
                        TextMeshPro mText = child.AddComponent<TextMeshPro>();
                        mText.fontSize = 5;
                        if (fog.getFogKind() == FogKind.TwoWP)
                        {
                            mText.text = "2WP";
                            mText.fontSize = 4;
                        }
                        if (fog.getFogKind() == FogKind.ThreeWP)
                        {
                            mText.text = "3WP";
                            mText.fontSize = 4;
                        }
                        if (fog.getFogKind() == FogKind.SP)
                        {
                            mText.text = "SP";
                        }
                        if (fog.getFogKind() == FogKind.Gold)
                        {
                            mText.text = "G";
                        }
                        if (fog.getFogKind() == FogKind.Event)
                        {
                            mText.text = "Event";
                            mText.fontSize = 3;
                        }
                        if (fog.getFogKind() == FogKind.Wineskin)
                        {
                            mText.text = "wine\nskin";
                            mText.fontSize = 3;
                        }
                        mText.alignment = TextAlignmentOptions.Center;
                        child.transform.localPosition = new Vector3(0, 1, 0);
                        child.transform.Rotate(90, 0, 0);
                        child.transform.localScale = Vector3.one;
                    }
                    else
                    {

                        if (fog.getFogKind() == FogKind.Monster)
                        {
                            GameObject mon = Instantiate(prefabsMonster[0], new Vector3(0, 0, 0), Quaternion.identity);
                            mon.GetComponent<MonsterController>().setNode(fogGameObject.GetComponent<FogController>().getCurrentPosition());
                            Monster m=null;
                            int counterForMonster = 0;
                            foreach( TileUnit u in fog.getRegion().getTileUnits())
                            {
                                if (u.GetType() == typeof(Monster))
                                {
                                    counterForMonster++;
                                    m = (Monster)u;
                                }
                            }
                            mon.GetComponent<MonsterController>().setMonster(m);
                            monsterList.Add(mon);
                            mon.GetComponent<MonsterController>().setUIElements(monsterNameUI, monsterWPUI, monsterSPUI);
                            objectToRemove.Add(fogGameObject);
                            fog.getRegion().getTileUnits().Remove(fog);
                            if (counterForMonster != 1)
                            {
                                // if the region already has a monster, then the one from the fog moves.
                                mon.GetComponent<MonsterController>().monsterMove(); 
                            }
                        }
                        if (fog.getFogKind() == FogKind.Witch)
                        {
                            GameObject witch = Instantiate(witchPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                            witch.GetComponent<WitchController>().setNode(fogGameObject.GetComponent<FogController>().getCurrentPosition());
                            Witch w = null;
                            foreach (TileUnit u in fog.getRegion().getTileUnits())
                            {
                                if (u.GetType() == typeof(Witch))
                                {
                                    w = (Witch)u;
                                }
                            }
                            witch.GetComponent<WitchController>().setWitch(w);
                            WitchList.Add(witch);
                            objectToRemove.Add(fogGameObject);
                            fog.getRegion().getTileUnits().Remove(fog);
                        }
                    }
                }
                else if (fog.isRevealed() && fog.getFogKind() == FogKind.None)
                {
                    // already activated
                    objectToRemove.Add(fogGameObject);
                    fog.getRegion().getTileUnits().Remove(fog);
                }
            }
            for (int i=0;i<objectToRemove.Count;i++)
            {
                FogList.Remove(objectToRemove[i]);
                Destroy(objectToRemove[i]);
            }
            
        }

        public void updateEvent(EventKind ek,bool add, Vector3 position, string description, string effect)
        {
            if (add)
            {
                GameObject eventToken = Instantiate(eventPrefab,position, Quaternion.identity);
                EventList.Add(eventToken);
                eventToken.GetComponent<EventController>().setEventKind(ek);
            }
            else
            {
                GameObject toDestroy = null;
                foreach(GameObject eventToken in EventList)
                {
                    if (eventToken.GetComponent<EventController>().getEventKind() ==ek)
                    {
                        toDestroy = eventToken;
                    }
                }
                if (toDestroy != null)
                {
                    EventList.Remove(toDestroy);
                    Destroy(toDestroy);
                }
            }
        }

        public void updateFarmer(int index)
        {
            GameObject farmer = FarmerList[index];
            FarmerList.RemoveAt(index);
            Destroy(farmer);
        }

        public void updateWell(int index)
        {
            GameObject well = wellList[index];
            wellList.RemoveAt(index);
            Destroy(well);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (chatbox.text != "")
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    sendMessageSync("" + username + ": " + chatbox.text);
                    chatbox.text = "";
                }
            }
            else {
                if (!chatbox.isFocused && Input.GetKeyDown(KeyCode.Return))
                    chatbox.ActivateInputField();
            }

            if (!chatbox.isFocused)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    sendMessageToChat("You pressed the space key!", Message.MessageType.info);
            }

            //Destroy monsters that are dead
            for (int i = 0; i < monsterList.Count; i++)
            {
                GameObject monster = monsterList[i];
                if (monster.GetComponent<MonsterController>().GetMonster().IsDead())
                {
                    monsterList.Remove(monster);
                    Destroy(monster);
                    i--;
                }
            }

            if(playerSentMessage){
                instantiator.sendChatMess(messageSync);
                playerSentMessage = false;
            }

            if(!pause){    
                if(turnChange){
                    nextTurn();
                    turnChange = false;
                }

                if(wellChange){
                    instantiator.updateWell(heroDrinker);
                    wellChange = false;
                    heroDrinker = -1;
                }

                if(pickingUp){
                    instantiator.updateFarmerPickUp(pickUpId);
                    pickingUp = false;
                    pickUpId = -1;
                }

                if(dropping){
                    instantiator.updateFarmerDrop(droppingId);
                    dropping = false;
                    droppingId = -1;
                }

                if(playerEnding){
                    instantiator.updatePlayerEnding(playerIdEnding);
                    playerIdEnding  = -1;
                    playerEnding = false;
                }

                if(numOfPlayersFinished == HeroList.Count){
                    instantiator.newday();
                    numOfPlayersFinished = 0;

                    //newday
                }
            }
            if(castle.getNumGoldenShields() <= 0)
            {
                //TODO: lose the game
                GameObject GameResultUI = GameObject.Find("GameResult").transform.GetChild(0).gameObject;
                GameObject DUI = null;
                string toDisplay = "The Legend ended badly:\n\t\tThe castle was not defended";
                for (int i = 0; i < GameResultUI.transform.childCount; i++)
                {
                    if (GameResultUI.transform.GetChild(i).transform.name == "Description")
                    {
                        DUI = GameResultUI.transform.GetChild(i).gameObject;
                    }
                }
                DUI.GetComponent<Text>().text = toDisplay;
                GameResultUI.SetActive(true);
            }
        }

        //for chat updates
        public string messageSync;

        public bool playerSentMessage;

        public void sendMessageSync(string mess){
            PV.RPC("messSync", RpcTarget.AllBuffered, mess);
        }

        [PunRPC]
        void messSync(string mess){
            messageSync = mess;
            playerSentMessage = true;

        }

        //for end day button
        public int numOfPlayersFinished = 0;
        public bool pause = false;
        public int playerIdEnding;
        public bool playerEnding = false;
        public void endDay(int id){
            PV.RPC("endDaySync", RpcTarget.AllBuffered, id);

        }

        [PunRPC]
        void endDaySync(int id){
            playerIdEnding = id;
            playerEnding  = true;
            numOfPlayersFinished = numOfPlayersFinished + 1;
        }


        //for end turn button
        bool turnChange = false;
        public void endTurn(){
            PV.RPC("turnChanged", RpcTarget.AllBuffered);
        }

        [PunRPC]
        void turnChanged(){
            turnChange = true;
        }

        // for well drink button
        public bool wellChange = false;
        public int heroDrinker;
        public void wellChangeCall(int id){
            PV.RPC("wellChanged", RpcTarget.AllBuffered, id);

        }

        [PunRPC]
        public void wellChanged(int id){
            wellChange = true;
            heroDrinker = id;
        }

        //for farmer sync
        //pick up farmer
        bool pickingUp = false;
        int pickUpId;
        public void farmerPickedUpBy(int pid){
            PV.RPC("farmerPickedUpBySync", RpcTarget.AllBuffered, pid);
        }

        [PunRPC]
        void farmerPickedUpBySync(int pid){
            pickUpId = pid;
            pickingUp = true;
        }

        //drop farmer
        bool dropping = false;
        int droppingId;
        public void farmerDroppedBy(int pid){
            PV.RPC("farmerDroppedBySync", RpcTarget.AllBuffered, pid);
        }

        [PunRPC]
        void farmerDroppedBySync(int pid){
            droppingId = pid;
            dropping = true;
        }

        public Castle getCastle(){
            return castle;
        }

        public GameObject getCastleHealth(){
            return CastleHealth;
        }
        public List<GameObject> getWellList(){
            return wellList;
        }

        public List<GameObject> getFarmerList(){
            return FarmerList;
        }

        public List<GameObject> getMonsterList()
        {
            return monsterList;
        }

        public List<GameObject> getFogList()
        {
            return FogList;
        }

        public List<GameObject> getHeroList()
        {
            return HeroList;
        }

        public List<Event> getEventCards()
        {
            return EventCards;
        }

        public List<GameObject> getMerchantList()
        {
            return MerchantList;
        }

        public void addEventsApplied(EventKind pEK)
        {
            EventsApplied.Add(pEK);
        }

        public void removeEventsApplied(EventKind pEK)
        {
            EventsApplied.Remove(pEK);
        }

        public List<EventKind> getEventsApplied()
        {
            return EventsApplied;
        }

        public GameDifficulty getDifficulty()
        {
            return difficulty;
        }

        public void setSkralDefeated(bool defeated)
        {
            SkralDefeated = defeated;
        }

        public bool isSkralDefeated()
        {
            return SkralDefeated;
        }

        public void updateHerb(bool add, Vector3 position)
        {
            if (add)
            {
                herb = Instantiate(herbPrefab, position, Quaternion.identity);
            }
            else
            {
                Destroy(herb);
            }
        }
        public void createNewMonster(GameObject n,Monster m)
        {
            GameObject mon = null;
            if (m.GetMonsterKind() == MonsterKind.Gor)
            {
                mon = Instantiate(prefabsMonster[0], new Vector3(0, 0, 0), Quaternion.identity);
            }
            if (m.GetMonsterKind() == MonsterKind.Skral)
            {
                mon = Instantiate(prefabsMonster[1], new Vector3(0, 0, 0), Quaternion.identity);
                if (m.onTower())
                {
                    mon.GetComponent<MeshRenderer>().material = skralTowerMaterial;
                }
            }
            if (m.GetMonsterKind() == MonsterKind.Troll)
            {
                mon = Instantiate(prefabsMonster[2], new Vector3(0, 0, 0), Quaternion.identity);
            }
            if (m.GetMonsterKind() == MonsterKind.Wardrak)
            {
                mon = Instantiate(prefabsMonster[3], new Vector3(0, 0, 0), Quaternion.identity);
            }
            mon.GetComponent<MonsterController>().setNode(n);
            mon.GetComponent<MonsterController>().setMonster(m);
            monsterList.Add(mon);
            mon.GetComponent<MonsterController>().setUIElements(monsterNameUI, monsterWPUI, monsterSPUI);
        }
        public void createNewFarmer(GameObject n, Farmer f)
        {
            GameObject farmer = Instantiate(farmerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            farmer.GetComponent<FarmerController>().setNode(n);
            farmer.GetComponent<FarmerController>().setFarmer(f);
            FarmerList.Add(farmer);
        }

        public void createPrince()
        {
            prince = Instantiate(princePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            prince.GetComponent<ThoraldController>().SetNode(GameObject.Find("R (72)"));
        }

        public GameObject getPrince()
        {
            return prince;
        }

        public void sendMessageToChat(string text, Message.MessageType messageType) {

            if (messageList.Count >= maxMessages)
            {
                Destroy(messageList[0].textObject.gameObject);
                messageList.Remove(messageList[0]);
            }
            Message newMessage = new Message();
            newMessage.text = text;

            GameObject newText = Instantiate(textObject, chatPanel.transform);

            newMessage.textObject = newText.GetComponent<Text>();

            newMessage.textObject.text = newMessage.text;
            newMessage.textObject.color = MessageTypeColor(messageType);
            messageList.Add(newMessage);
        }

        Color MessageTypeColor(Message.MessageType messageType)
        {
            Color color = info;

            switch (messageType)
            {
                case Message.MessageType.playerMessage:
                    color = player;
                    break;
            }

            return color;

        }
    }

    [System.Serializable]
    public class Message
    {
        public string text;
        public Text textObject;
        public MessageType messageType;

        public enum MessageType
        { 
            playerMessage,
            info
        }
    }
}

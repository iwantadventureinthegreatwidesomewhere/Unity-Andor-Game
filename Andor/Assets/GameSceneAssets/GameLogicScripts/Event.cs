using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace Scripts
{
	public class Event : Hideable
	{
		private EventKind eventKind;
		private bool hidden;

        GameObject EventUI = GameObject.Find("EventParent").transform.GetChild(0).gameObject;
        GameObject DUI;
        GameObject EUI;
        GameObject okButton;
        GameObject okButtonEvent16;
        GameObject removeButton;
        GameObject backButton;
        bool onHold = false;

        public Event(EventKind eventKind)
		{
			hidden = true;
			this.eventKind = eventKind;
            initializeUI();
        }

		public void hide()
		{
			hidden = true;
		}

		public bool isHidden()
		{
			return hidden;
		}

		public void reveal()
		{
			hidden = false;
		}

		public EventKind getEventKind()
        {
			return this.eventKind;
        }

		public void setEventKind(EventKind ek)
		{
			this.eventKind = ek;
		}

        public void applyEventEffect()
        {
            if (eventKind != EventKind.Event16) {
                showUI(this, false);
            }
        }
        //Called by Eventbutton once button is pressed
        public void ApplyEffect() { 
            string description = this.getEventKind().getDescription();
            string effect = this.getEventKind().getEffect();
            //GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(ek, false, position, description, effect);
            //9
            if (eventKind == EventKind.Event9)
            {
                //effect = "On this day, no hero is allowed to use a 10th hour. Place this card above the overtime area of the time track. At the end of the day, it is removed from the game.";
                Vector3 position = new Vector3(6.6f, 0.0f, 4.6f);
                GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(eventKind,true,position,description,effect);
                GameObject.Find("GameManager").GetComponent<GameManager>().addEventsApplied(eventKind);
                for (int pid = 0; pid < 4; pid++)
                {
                    GameObject tt = GameObject.Find(GameObject.Find("GameManager").GetComponent<GameManager>().GetHeroTTByPID(pid));
                    if (tt == null)
                        continue;
                    else
                    {
                        tt.GetComponent<TimeTrackController>().maxTime = 9;
                    }
                }
            }
            //24
            if (eventKind == EventKind.Event24)
            {
                //effect = "Any hero who is not on a forest space, in the mine (space 71), in the tavern (space 72), or in the castle (space 0) loses 2 willpower points.";
                List<GameObject> heroList = GameObject.Find("GameManager").GetComponent<GameManager>().HeroList;
                foreach (GameObject hero in heroList)
                {
                    int rank = hero.GetComponent<HeroController>().getCurrentPosition().GetComponent<Node>().getRank();
                    if (rank != 71 || rank != 72 || rank != 0 || rank < 47 || (rank > 60 && rank != 62 && rank != 63))
                    {
                        Hero toLoseWP = hero.GetComponent<HeroController>().getHero();
                        toLoseWP.TakeDamage(2);
                        //toLoseSP.setWillpowerPoints(toLoseSP.getWillpowerPoints() - 2);
                    }
                }
            }
            //19
            if (eventKind == EventKind.Event19)
            {
                //effect = "On this day, the 9th and 10th hours will each cost 3 willpower points instead of 2.\nPlace this card above the overtime area of the time track. At the end of the day, it is removed from the game.";
                Vector3 position = new Vector3(6.6f, 0.0f, 4.6f);
                GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(eventKind, true, position, description, effect);
                GameObject.Find("GameManager").GetComponent<GameManager>().addEventsApplied(eventKind);
            }
            //30
            if (eventKind == EventKind.Event30)
            {
                //effect = "Place a wineskin on the tavern space (72). A hero who enters space 72 or is already standing there can collect the wineskin and place it on the small storage space on his hero board. If more than one hero is standing there, the hero with the lowest rank gets the wineskin.";
                Vector3 position = new Vector3(-4.39f, 0.0f, -0.97f);
                GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(eventKind, true, position, description, effect);
                Region space72 = GameObject.Find("R (72)").GetComponent<Node>().GetRegion();
                space72.addItem(new Wineskin(ItemWeight.Light));
                Hero smallestRank = null;
                int rank = 34; //Highest hero rank
                foreach(TileUnit t in space72.getTileUnits())
                {
                    if(typeof(Hero) == t.GetType())
                    {
                        Hero h = (Hero)t;
                        if (h.getHeroRank() <= rank)
                        {
                            rank = h.getHeroRank();
                            smallestRank = h;
                        }
                    }
                }
                if (smallestRank != null)
                {
                    smallestRank.pickUpItemOnGround(space72, new Wineskin(ItemWeight.Light));
                    GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(eventKind, false, position, description, effect);
                }
            }
            //33
            if (eventKind == EventKind.Event33)
            {
                //effect = "One of the heroes immediately loses 1 strength point. You can decide as a group which hero that will be. If no hero has more than 1 point, nothing happens.";
                // TO-DO: after vote feature been done
            }
            //29
            if (eventKind == EventKind.Event29)
            {
                //effect = "Now place a shield on space 57. A hero who enters space 57 or is already standing there can collect the shield and place it on the large storage space on his hero board. If more than one hero is standing there, the hero with the lowest rank gets the shield.";
                Vector3 position = new Vector3(3.99f, 0.0f, 3.33f);
                GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(eventKind, true, position, description, effect);
                Region space57 = GameObject.Find("R (57)").GetComponent<Node>().GetRegion();
                space57.addItem(new Shield(ItemWeight.Heavy));
                Hero smallestRank = null;
                int rank = 34; //Highest hero rank
                foreach (TileUnit t in space57.getTileUnits())
                {
                    if (typeof(Hero) == t.GetType())
                    {
                        Hero h = (Hero)t;
                        if (h.getHeroRank() <= rank)
                        {
                            rank = h.getHeroRank();
                            smallestRank = h;
                        }
                    }
                }
                if (smallestRank != null)
                {
                    smallestRank.pickUpItemOnGround(space57, new Shield(ItemWeight.Heavy));
                    GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(eventKind, false, position, description, effect);
                }
            }
            //26
            if (eventKind == EventKind.Event26)
            {
                //effect = "On this day, the 8th hour costs no willpower points.\nPlace this card above the overtime area of the time track. At the end of the day, it is removed from the game.";
                Vector3 position = new Vector3(6.6f, 0.0f, 4.6f);
                GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(eventKind, true, position, description, effect);
                GameObject.Find("GameManager").GetComponent<GameManager>().addEventsApplied(eventKind);
            }
            //21
            if (eventKind == EventKind.Event21)
            {
                //effect = "A hero who enters space 22, 23, 24 25 or is already standing there will immediately lose 4 willpower points. If more than one hero is standing there,  the one with the highest rank loses the points.\nPlace this card next to space 24 until it is triggered. Then it is removed from the game.";
                Vector3 position = new Vector3(-7.07f, 0.1f, -1.1f);
                GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(eventKind, true, position, description, effect);
                List<GameObject> heroList = GameObject.Find("GameManager").GetComponent<GameManager>().HeroList;
                Hero highest = null;
                int highestRank = -1;
                foreach (GameObject hero in heroList)
                {
                    int rank = hero.GetComponent<HeroController>().getCurrentPosition().GetComponent<Node>().getRank();
                    if (rank == 22 || rank == 23 || rank == 24 || rank == 25)
                    {
                        if (rank > highestRank)
                        {
                            highest= hero.GetComponent<HeroController>().getHero();
                            highestRank = rank;
                        }
                    }
                }
                if (highest == null)
                {
                    //no hero in 22,23,24,25
                    GameObject.Find("R (24)").GetComponent<Node>().GetRegion().addEvent(eventKind); 
                }else
                {
                    highest.TakeDamage(4);
                    //highest.setWillpowerPoints(highest.getWillpowerPoints() - 4);
                    GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(eventKind, false, position, description, effect);
                }
            }
            //1
            if (eventKind == EventKind.Event1)
            {
                //effect = "Each hero may now purchase any article from the equipment board (except the witch's brew) in exchange for 3 willpower points.";
                Region mine = GameObject.Find("R (71)").GetComponent<Node>().GetRegion();
                Merchant Garz=null;
                foreach(TileUnit t in mine.getTileUnits())
                {
                    if(t.GetType() == typeof(Merchant))
                    {
                        Garz = (Merchant)t;
                    }
                }
                Garz.allowUseWP();
                //TO-DO: already add a flag to tell whether can use WP at the mine merchant, need to build trading system using wp in script Merchant and UI
            }
            //32
            if (eventKind == EventKind.Event32)
            {
                //effect = "Every hero whose time marker is presently in the sunrise box loses 2 willpower points.";
                for (int pid = 0; pid < 4; pid++)
                {
                    GameObject tt = GameObject.Find(GameObject.Find("GameManager").GetComponent<GameManager>().GetHeroTTByPID(pid));
                    if (tt == null)
                        continue;
                    else if(tt.GetComponent<TimeTrackController>().timeTrack == GameObject.Find("sunriseBox")){
                        GameObject.Find(GameObject.Find("GameManager").GetComponent<GameManager>().GetHeroByPID(pid)).GetComponent<HeroController>().getHero().TakeDamage(2);
                    }
                }
            }
            //20
            if (eventKind == EventKind.Event20)
            {
                // effect = "One farmer token on the game board that has not yet been taken to the castle must be removed from the game. The group can prevent that by paying gold and/or willpower points:\nfor 2 heroes, 2 gold/WP\nfor 3 heroes, 3 gold/WP\nfor 4 heroes, 4 gold/WP.";
                List<GameObject> farmerList = GameObject.Find("GameManager").GetComponent<GameManager>().getFarmerList();
                //Debug.Log(farmerList.Count);
                int index = -1;
                //GameObject toRemove = null;
                foreach (GameObject farmer in farmerList)
                {
                    if (!farmer.GetComponent<FarmerController>().getFarmer().beGuided())
                    {
                        //Debug.Log("Not guided");
                        index = farmerList.IndexOf(farmer);
                        //toRemove = farmer;
                        farmer.GetComponent<FarmerController>().getFarmer().Die();
                        break;
                    }
                }
                if (index != -1)
                {
                    GameObject.Find("GameManager").GetComponent<GameManager>().updateFarmer(index);
                }
                // To-Do: need vote. everyone vote for the one who pay, there is an option that is giveup, when vote for this, then farmer is killed
            }
            //7
            if (eventKind == EventKind.Event7)
            {
                //effect = "The hero with the lowest rank rolls one of his hero dice. The group loses the rolled number of willpower points.";
                List<GameObject> heroList = GameObject.Find("GameManager").GetComponent<GameManager>().HeroList;
                Hero smallestRank = null;
                int smallest = 100;
                int roll = Random.Range(1, 7);
                foreach(GameObject hero in heroList)
                {
                    if (hero.GetComponent<HeroController>().getHero().getRank() < smallest)
                    {
                        smallestRank = hero.GetComponent<HeroController>().getHero();
                        smallest = hero.GetComponent<HeroController>().getHero().getRank();
                    }
                }
                foreach(GameObject hero in heroList)
                {
                    hero.GetComponent<HeroController>().getHero().TakeDamage(roll);
                }
            }
            //25
            if (eventKind == EventKind.Event25)
            {
                //effect = "Any hero with fewer than 6 willpower points rolls a hero die and gets the rolled number of willpower points.";
                List<GameObject> heroList = GameObject.Find("GameManager").GetComponent<GameManager>().HeroList;
                foreach (GameObject hero in heroList)
                {
                    Hero h = hero.GetComponent<HeroController>().getHero();
                    if (h.getWillpowerPoints() < 6)
                    {
                        int roll = Random.Range(1, 7);
                        h.Heal(roll);
                    }
                }
            }
            //34
            if (eventKind == EventKind.Event34)
            {
                //effect = "One of the heroes can now purchase 10 willpower points in exchange for 2 strength points. You can decide as group which hero will be.";
                //To-Do: vote feature
            }
            //18
            if (eventKind == EventKind.Event18)
            {
                //effect = "The gor on the space with the lowest number now moves one space in the direction of the arrow. The group can prevent that by paying gold and/or willpower points:\nfor 2 heroes, 2 gold/WP\nfor 3 heroes, 4 gold/WP\nfor 4 heroes, 6 gold/WP.";
                // vote for one player to pay, otherwise gor moves.
                List<GameObject> monsters = GameObject.Find("GameManager").GetComponent<GameManager>().getMonsterList();
                int rank = 100;
                GameObject m = null;
                foreach (GameObject monster in monsters)
                {
                    if (monster.GetComponent<MonsterController>().getCurrentPosition().GetComponent<Node>().getRank() < rank)
                    {
                        rank = monster.GetComponent<MonsterController>().getCurrentPosition().GetComponent<Node>().getRank();
                        m = monster;
                    }
                }
                m.GetComponent<MonsterController>().monsterMove();
                //To-Do: vote
            }
            //2
            if (eventKind == EventKind.Event2)
            {
                //effect = "Each hero standing on a space with a number between 0 and 20 now loses 3 willpower points.";
                List<GameObject> heroList = GameObject.Find("GameManager").GetComponent<GameManager>().HeroList;
                foreach (GameObject hero in heroList)
                {
                    int rank = hero.GetComponent<HeroController>().getCurrentPosition().GetComponent<Node>().getRank();
                    if (rank >0&&rank<20)
                    {
                        Hero toLoseSP = hero.GetComponent<HeroController>().getHero();
                        toLoseSP.setWillpowerPoints(toLoseSP.getWillpowerPoints() - 3);
                    }
                }
            }
            //16
            if (eventKind == EventKind.Event16)
            {
                //effect = "The hero with the highest rank is allowed to take a look at the top card on the event card deck. Then he gets to decide whether to remove the card from the game or to place it back on the deck.";
                DUI.GetComponent<Text>().text = EventKind.Event16.getDescription();
                EUI.GetComponent<Text>().text = EventKind.Event16.getEffect();
                okButtonEvent16.SetActive(true);
                DUI.SetActive(true);
                EUI.SetActive(true);
                EventUI.SetActive(true);
                List <GameObject> heroList = GameObject.Find("GameManager").GetComponent<GameManager>().HeroList;
                int rank = 0;
                Hero toSee = null;
                // find the highest rank hero
                foreach (GameObject hero in heroList)
                {
                    Hero h = hero.GetComponent<HeroController>().getHero();
                    if (h.getRank() > rank)
                    {
                        rank = h.getRank();
                        toSee = h;
                    }
                }
                // To-Do: only reveal for the player with the highest rank.
            }
            //22
            if (eventKind == EventKind.Event22)
            {
                //effect = "The well token on space 45 is removed from the game.";
                List<GameObject> wellList = GameObject.Find("GameManager").GetComponent<GameManager>().getWellList();
                GameObject toRemove = null;
                int index = -1;
                foreach (GameObject well in wellList)
                {
                    Debug.Log(well.GetComponent<WellController>().getCurrentPosition().GetComponent<Node>().getRank());
                    if (well.GetComponent<WellController>().getCurrentPosition().GetComponent<Node>().getRank() == 45)
                    {
                        toRemove = well;
                        index = wellList.IndexOf(toRemove);
                    }
                }
                List<TileUnit> list = toRemove.GetComponent<WellController>().getCurrentPosition().GetComponent<Node>().GetRegion().getTileUnits();
                TileUnit toRemoveT = null;
                foreach (TileUnit t in list)
                {
                    if (t.GetType() == typeof(Well))
                    {
                        toRemoveT = t;
                    }
                }
                list.Remove(toRemoveT);
                GameObject.Find("GameManager").GetComponent<GameManager>().updateWell(index);
            }
            //11
            if (eventKind == EventKind.Event11)
            {
                //effect = "On this day, each creature has one extra strength point.\nPlace this card next to the creature display. At the end of the day, it is removed from the game.";
                Vector3 position = new Vector3(1.4f, 0.1f, -3.69f);
                GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(eventKind, true, position, description, effect);
                List<GameObject> MonsterList = GameObject.Find("GameManager").GetComponent<GameManager>().getMonsterList();
                foreach (GameObject monster in MonsterList)
                {
                    Monster m = monster.GetComponent<MonsterController>().GetMonster();
                    m.setSP(m.getSP() + 1);
                }
                GameObject.Find("GameManager").GetComponent<GameManager>().addEventsApplied(eventKind);
            }
            //28
            if (eventKind == EventKind.Event28)
            {
                //effect = "Every hero whose time marker is presently in the sunrise box gets 2 willpower points.";
                for (int pid = 0; pid < 4; pid++)
                {
                    GameObject tt = GameObject.Find(GameObject.Find("GameManager").GetComponent<GameManager>().GetHeroTTByPID(pid));
                    if (tt == null)
                        continue;
                    else if (tt.GetComponent<TimeTrackController>().timeTrack == GameObject.Find("sunriseBox"))
                    {
                        GameObject.Find(GameObject.Find("GameManager").GetComponent<GameManager>().GetHeroByPID(pid)).GetComponent<HeroController>().getHero().Heal(2);
                    }
                }
            }
            //3
            if (eventKind == EventKind.Event3)
            {
                //effect = "A hero who enters the Tree of Songs space or is already standing there gets 1 strength point. If more than one hero is standing there, the hero with the highest rank gets the strength point\nNow place this card on space 57 until a hero has gotten the strength point. Then remove it from the game.";
                Vector3 position = new Vector3(4f, 0.1f, 2.9f);
                GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(eventKind, true, position, description, effect);
                Region space57 = GameObject.Find("R (57)").GetComponent<Node>().GetRegion();
                Hero smallestRank = null;
                int rank = 34; //Highest hero rank
                foreach (TileUnit t in space57.getTileUnits())
                {
                    if (typeof(Hero) == t.GetType())
                    {
                        Hero h = (Hero)t;
                        if (h.getHeroRank() <= rank)
                        {
                            rank = h.getHeroRank();
                            smallestRank = h;
                        }
                    }
                }
                if (smallestRank ==null)
                {
                    //No hero on this space
                    space57.addEvent(EventKind.Event3);
                }
                else
                {
                    smallestRank.setStrengthPoints(smallestRank.getStrengthPoints() + 1);
                    GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(eventKind, false, position, description, effect);
                }
            }
            //8
            if (eventKind == EventKind.Event8)
            {
                //effect = "A hero who enters the Tree of Songs space or is already standing there can buy 2 strength points there for just 2 gold.\nPlace this card on space 57 until a hero has made the purchase. Then remove it from the game.";
                Vector3 position = new Vector3(4f, 0.1f, 3.21f);
                GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(eventKind, true, position, description, effect);
                Region space57 = GameObject.Find("R (57)").GetComponent<Node>().GetRegion();
                space57.addEvent(EventKind.Event8);
                // To-Do: in EventButton, the  OnYesButtonPressed() method, finish trading
            }
            //17
            if (eventKind == EventKind.Event17)
            {
                //effect = "Each hero with more than 12 willpower points immediately reduce his point total to 12.";
                List<GameObject> heroList = GameObject.Find("GameManager").GetComponent<GameManager>().HeroList;
                foreach (GameObject hero in heroList)
                {
                    Hero h = hero.GetComponent<HeroController>().getHero();
                    if (h.getWillpowerPoints() > 12)
                    {
                        h.setWillpowerPoints(12);
                    }
                }
            }
            //6
            if (eventKind == EventKind.Event6)
            {
                //effect = "The hero with the lowest rank gets to decide whether he wants to roll one of his hero dice. If he rolls 1, 2,3 or 4, he loses his rolled number of willpower points. If he rolls 5 or 6, he wins that number of willpower points.";
                List<GameObject> heroList = GameObject.Find("GameManager").GetComponent<GameManager>().HeroList;
                int lowestRank = 100;
                Hero lowest = null;
                foreach(GameObject hero in heroList)
                {
                    Hero h = hero.GetComponent<HeroController>().getHero();
                    if(h.getRank() < lowestRank)
                    {
                        lowest = h;
                        lowestRank = h.getRank();
                    }
                }
                CombatManager.instance.ResolveEvent6(lowest);
            }
            //27
            if (eventKind == EventKind.Event27)
            {
                //effect = "The creature standing on the space with the highest number will now move one space along the arrow . The group can prevent that by paying gold and/or willpower points:\nfor 2 heroes, 2 gold/WP\nfor 3 heroes, 3 gold/WP\nfor 4 heroes, 4 gold/WP.";
                // vote for one player to pay, otherwise creature moves.
                List<GameObject> monsters = GameObject.Find("GameManager").GetComponent<GameManager>().getMonsterList();
                int rank = 0;
                GameObject m = null;
                foreach (GameObject monster in monsters)
                {
                    if (monster.GetComponent<MonsterController>().getCurrentPosition().GetComponent<Node>().getRank() > rank)
                    {
                        rank = monster.GetComponent<MonsterController>().getCurrentPosition().GetComponent<Node>().getRank();
                        m = monster;
                    }
                }
                m.GetComponent<MonsterController>().monsterMove();
                //To-Do: vote
            }
            //15
            if (eventKind == EventKind.Event15)
            {
                //effect = "The well token on space 35 is removed from the game.";
                List<GameObject> wellList=GameObject.Find("GameManager").GetComponent<GameManager>().getWellList();
                GameObject toRemove = null;
                int index = -1;
                foreach(GameObject well in wellList)
                {
                    Debug.Log(well.GetComponent<WellController>().getCurrentPosition().GetComponent<Node>().getRank());
                    if (well.GetComponent<WellController>().getCurrentPosition().GetComponent<Node>().getRank() == 35)
                    {
                        toRemove = well;
                        index = wellList.IndexOf(toRemove);
                    }
                }
                List<TileUnit> list = toRemove.GetComponent<WellController>().getCurrentPosition().GetComponent<Node>().GetRegion().getTileUnits();
                TileUnit toRemoveT = null;
                foreach(TileUnit t in list)
                {
                    if (t.GetType() == typeof(Well))
                    {
                        toRemoveT = t;
                    }
                }
                list.Remove(toRemoveT);
                GameObject.Find("GameManager").GetComponent<GameManager>().updateWell(index);
            }
            //14
            if (eventKind == EventKind.Event14)
            {
                //effect = "The dwarf and the warrior immediately get 3 willpower points each.";
                List<GameObject> heroList = GameObject.Find("GameManager").GetComponent<GameManager>().HeroList;
                foreach (GameObject hero in heroList)
                {
                    Hero h = hero.GetComponent<HeroController>().getHero();
                    if (h.getHeroKind() == HeroKind.Dwarf || h.getHeroKind() == HeroKind.Warrior)
                    {
                        h.Heal(3);
                        //h.setWillpowerPoints(h.getWillpowerPoints() + 3);
                    }
                }
            }
            //13
            if (eventKind == EventKind.Event13)
            {
                //effect = "Each hero who has fewer than 10 willpower points can immediately raise his total to 10.";
                List<GameObject> heroList = GameObject.Find("GameManager").GetComponent<GameManager>().HeroList;
                foreach (GameObject hero in heroList)
                {
                    Hero h = hero.GetComponent<HeroController>().getHero();
                    if (h.getWillpowerPoints() < 10)
                    {
                        h.setWillpowerPoints(10);
                    }
                }
            }
            //10
            if (eventKind == EventKind.Event10)
            {
                //effect = "Each hero can now purchase 3 willpower points in exchange of 1 gold.";
                List<GameObject> MerchantList = GameObject.Find("GameManager").GetComponent<GameManager>().getMerchantList();
                foreach (GameObject merchant in MerchantList)
                {
                    merchant.GetComponent<MerchantController>().getMerchant().allowBuyWP();
                }
                //To-Do: in buying system, modify allowBuyWP(), to add new item which is WP in the inventory.
            }
            //23
            if (eventKind == EventKind.Event23)
            {
                //effect = "Up to two heroes with 6 or fewer strength points can each add 1 strength point to what they already have. You can decide as a group which heroes those will be.";
                //To-Do: vote
            }
            //31
            if (eventKind == EventKind.Event31)
            {
                // effect = "Any hero who is not on a foreset space, in the mine (space 71), in the tavern (space 72), or in the castle (space 0) loses2 willpower points.";
                List<GameObject> heroList = GameObject.Find("GameManager").GetComponent<GameManager>().HeroList;
                foreach (GameObject hero in heroList)
                {
                    int rank = hero.GetComponent<HeroController>().getCurrentPosition().GetComponent<Node>().getRank();
                    if (rank != 71 || rank != 72 || rank != 0 || rank < 47 || (rank > 60 && rank != 62 && rank != 63))
                    {
                        Hero toLoseWP = hero.GetComponent<HeroController>().getHero();
                        toLoseWP.TakeDamage(2);
                        //toLoseSP.setWillpowerPoints(toLoseSP.getWillpowerPoints() - 2);
                    }
                }
            }
            //4
            if (eventKind == EventKind.Event4)
            {
                //effect = "Each hero who is now standing on a space bordering the river gets a wineskin.";
                List<GameObject> heroList = GameObject.Find("GameManager").GetComponent<GameManager>().HeroList;
                foreach (GameObject hero in heroList)
                {
                    int rank = hero.GetComponent<HeroController>().getCurrentPosition().GetComponent<Node>().getRank();
                    if ((rank>=8&&rank<=16&&rank!=10&&rank!=14)||(rank>=26&&rank<=33)||(rank>=38&&rank<=49&&rank!=43&&rank!=45)||rank==56||rank==63||rank==64)
                    {
                        hero.GetComponent<HeroController>().getHero().addItem(new Wineskin(ItemWeight.Light));
                    }
                }
            }
            //5
            if (eventKind == EventKind.Event5)
            {
                //effect = "Each hero standing on a space with a number between 37 and 70 now loses 3 willpower points.";
                List<GameObject> heroList = GameObject.Find("GameManager").GetComponent<GameManager>().HeroList;
                foreach (GameObject hero in heroList)
                {
                    int rank = hero.GetComponent<HeroController>().getCurrentPosition().GetComponent<Node>().getRank();
                    if (rank>37&&rank<70) { 
                        Hero toLoseWP = hero.GetComponent<HeroController>().getHero();
                        toLoseWP.TakeDamage(3);
                        //toLoseWP.setWillpowerPoints(toLoseWP.getWillpowerPoints() - 3);
                    }
                }
            }
            //12
            if (eventKind == EventKind.Event12)
            {
                //effect = "The wizard and the archer each immediately get 3 willpower points.";
                List<GameObject> heroList = GameObject.Find("GameManager").GetComponent<GameManager>().HeroList;
                foreach (GameObject hero in heroList)
                {
                    Hero h = hero.GetComponent<HeroController>().getHero();
                    if (h.getHeroKind() == HeroKind.Wizard || h.getHeroKind() == HeroKind.Archer)
                    {
                        h.Heal(3);
                        //h.setWillpowerPoints(h.getWillpowerPoints() + 3);
                    }
                }
            }
        }

        void initializeUI()
        {
            for (int i = 0; i < EventUI.transform.childCount; i++)
            {
                int indext = i;
                if (EventUI.transform.GetChild(i).transform.name == "Description")
                {
                    DUI = EventUI.transform.GetChild(i).gameObject;
                }
                if (EventUI.transform.GetChild(i).transform.name == "Effect")
                {
                    EUI = EventUI.transform.GetChild(i).gameObject;
                }
                if (EventUI.transform.GetChild(i).transform.name == "EventButton")
                {
                    okButton = EventUI.transform.GetChild(i).gameObject;
                }
                if (EventUI.transform.GetChild(i).transform.name == "RemoveEvent")
                {
                    removeButton = EventUI.transform.GetChild(i).gameObject;
                }
                if (EventUI.transform.GetChild(i).transform.name == "PlaceBack")
                {
                    backButton = EventUI.transform.GetChild(i).gameObject;
                }
                if (EventUI.transform.GetChild(i).transform.name == "EventButtonFor16")
                {
                    okButtonEvent16 = EventUI.transform.GetChild(i).gameObject;
                }
            }
        }

        public void showUI(Event pEvent, bool allowRemove)
        {
            Event e = pEvent;
            EventKind ek = eventKind;
            string description = e.getEventKind().getDescription();
            string effect = e.getEventKind().getEffect();
            initializeUI();
            DUI.GetComponent<Text>().text = description;
            EUI.GetComponent<Text>().text = effect;
            if (allowRemove)
            {
                removeButton.SetActive(true);
                backButton.SetActive(true);
            }else
            {
                okButton.SetActive(true);
            }
            DUI.SetActive(true);
            EUI.SetActive(true);
            EventUI.SetActive(true);
            GameObject.FindObjectOfType<GameManager>().ActiveEvent = this;
        }
	}
}

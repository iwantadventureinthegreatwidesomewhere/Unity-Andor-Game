using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Scripts{
    public class NewDayUpdate : MonoBehaviour
    {
        void Awake() {
            Debug.Log("SetUp new day");
            GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
            TimeTrackManager TTM = GameObject.Find("GameManager").GetComponent<TimeTrackManager>();
            TTM.firstToEnd = true;
            GM.pause = true;
            //set the new turn order
            GM.turnOrder.Clear();
            foreach(int i in GM.nextTurnOrder){
                HeroController h = GameObject.Find(GM.GetHeroByPID(i)).GetComponent<HeroController>();
                h.hasEndedDay = false;
                TimeTrackController tt = GameObject.Find(GM.GetHeroTTByPID(i)).GetComponent<TimeTrackController>();
                tt.ResetTurn();
                GM.turnOrder.Add(i);
            }
            GM.nextTurnOrder.Clear();

            GM.turnTick = 0;

            //monster move
            foreach(GameObject m in GM.getMonsterList()){
                if(m.active == true){
                    m.GetComponent<MonsterController>().monsterMove();
                }
                
            }

            //remove event effect
            List<EventKind> appliedEvent = GameObject.Find("GameManager").GetComponent<GameManager>().getEventsApplied();
            if (appliedEvent.Contains(EventKind.Event9))
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(EventKind.Event9, false,new Vector3(0,0,0), "", "");
                for (int pid = 0; pid < 4; pid++)
                {
                    GameObject tt = GameObject.Find(GameObject.Find("GameManager").GetComponent<GameManager>().GetHeroTTByPID(pid));
                    if (tt == null)
                        continue;
                    else
                    {
                        tt.GetComponent<TimeTrackController>().maxTime = 10;
                    }
                }
                appliedEvent.Remove(EventKind.Event9);
            }
            if (appliedEvent.Contains(EventKind.Event19))
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().getEventsApplied().Remove(EventKind.Event19);
                GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(EventKind.Event19, false, new Vector3(0, 0, 0), "", "");
            }
            if (appliedEvent.Contains(EventKind.Event26))
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().getEventsApplied().Remove(EventKind.Event26);
                GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(EventKind.Event26, false, new Vector3(0, 0, 0), "", "");
            }
            if (appliedEvent.Contains(EventKind.Event11))
            {
                List<GameObject> MonsterList = GameObject.Find("GameManager").GetComponent<GameManager>().getMonsterList();
                foreach (GameObject monster in MonsterList)
                {
                    Monster m = monster.GetComponent<MonsterController>().GetMonster();
                    m.setSP(m.getSP() - 1);
                }
                GameObject.Find("GameManager").GetComponent<GameManager>().getEventsApplied().Remove(EventKind.Event11);
                GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(EventKind.Event11, false, new Vector3(0, 0, 0), "", "");
            }

            //narrator
            GameObject.Find("Narrator").GetComponent<NarratorController>().advance();
            StoryPoint storyPoint = GameObject.Find("Narrator").GetComponent<NarratorController>().getStoryPoint();
            if (storyPoint == StoryPoint.C || storyPoint == StoryPoint.G)
            {
                GameObject.Find("Narrator").GetComponent<NarratorController>().show(false);
            }
            if (GameObject.Find("Narrator").GetComponent<NarratorController>().getCurrentStoryPoint() == GameObject.Find("Narrator").GetComponent<NarratorController>().getRunestone())
            {
                if (GameObject.Find("GameManager").GetComponent<GameManager>().getDifficulty() == GameDifficulty.Easy)
                {
                    GameObject.Find("Narrator").GetComponent<NarratorController>().show(true);
                }else if (GameObject.Find("GameManager").GetComponent<GameManager>().witchFound == true)
                {
                    GameObject.Find("Narrator").GetComponent<NarratorController>().show(true);
                }
            }
            if (storyPoint == StoryPoint.C)
            {
                //skral on tower
                int TowerPosition = UnityEngine.Random.Range(1, 7);
                TowerPosition += 50;
                string towerString = "R (" + TowerPosition + ")";
                GameObject tower = GameObject.Find(towerString);
                Monster SkralOnTower = new Monster(tower.GetComponent<Node>().GetRegion(), MonsterKind.Skral, true);
                GameObject.Find("GameManager").GetComponent<GameManager>().createNewMonster(tower, SkralOnTower);
                //below normal monster
                GameObject node29 = GameObject.Find("R (29)");
                Monster Skral29 = new Monster(node29.GetComponent<Node>().GetRegion(), MonsterKind.Skral, false);
                GameObject.Find("GameManager").GetComponent<GameManager>().createNewMonster(node29, Skral29);
                GameObject node27 = GameObject.Find("R (27)");
                Monster gor27=new Monster(node27.GetComponent<Node>().GetRegion(), MonsterKind.Gor, false);
                GameObject.Find("GameManager").GetComponent<GameManager>().createNewMonster(node27, gor27);
                GameObject node31 = GameObject.Find("R (31)");
                Monster gor31 = new Monster(node31.GetComponent<Node>().GetRegion(), MonsterKind.Gor, false);
                GameObject.Find("GameManager").GetComponent<GameManager>().createNewMonster(node31, gor31);
                // below farmer
                GameObject node28 = GameObject.Find("R (28)");
                Farmer farmer28= new Farmer(node28.GetComponent<Node>().GetRegion());
                GameObject.Find("GameManager").GetComponent<GameManager>().createNewFarmer(node28, farmer28);
                // prince Thorald
                GameObject.Find("GameManager").GetComponent<GameManager>().createPrince();
            }else if (storyPoint == StoryPoint.G)
            {
                //Prince Thorald removed\nWardraks on spaces 26 and 27.
                GameObject.Find("GameManager").GetComponent<GameManager>().getPrince().SetActive(false);
                //below new wardraks
                GameObject node26 = GameObject.Find("R (26)");
                Monster wardrak26 = new Monster(node26.GetComponent<Node>().GetRegion(), MonsterKind.Wardrak, false);
                GameObject.Find("GameManager").GetComponent<GameManager>().createNewMonster(node26, wardrak26);
                GameObject node27 = GameObject.Find("R (27)");
                Monster wardrak27 = new Monster(node27.GetComponent<Node>().GetRegion(), MonsterKind.Wardrak, false);
                GameObject.Find("GameManager").GetComponent<GameManager>().createNewMonster(node27, wardrak27);
            }else if(storyPoint== GameObject.Find("Narrator").GetComponent<NarratorController>().getRunestone())
            {
                GameObject node43 = GameObject.Find("R (43)");
                Monster gor43 = new Monster(node43.GetComponent<Node>().GetRegion(), MonsterKind.Gor, false);
                GameObject.Find("GameManager").GetComponent<GameManager>().createNewMonster(node43, gor43);
                GameObject node39 = GameObject.Find("R (39)");
                Monster skral39 = new Monster(node43.GetComponent<Node>().GetRegion(), MonsterKind.Skral, false);
                GameObject.Find("GameManager").GetComponent<GameManager>().createNewMonster(node39, skral39);
                if (GameObject.Find("GameManager").GetComponent<GameManager>().getDifficulty() == GameDifficulty.Hard&&GameObject.Find("GameManager").GetComponent<GameManager>().witchFound == true)
                {
                    // hard mode only when witch found
                    GameObject node32 = GameObject.Find("R (32)");
                    Monster gor32 = new Monster(node32.GetComponent<Node>().GetRegion(), MonsterKind.Gor, false);
                    GameObject.Find("GameManager").GetComponent<GameManager>().createNewMonster(node32, gor32);
                }
                // rune stone
                Runestone y1=new Runestone(ItemWeight.Light, GemColor.Yellow);
                Runestone y2 =new Runestone(ItemWeight.Light, GemColor.Yellow);
                Runestone b1 =new Runestone(ItemWeight.Light, GemColor.Blue);
                Runestone b2 =new Runestone(ItemWeight.Light, GemColor.Blue);
                Runestone g1 =new Runestone(ItemWeight.Light, GemColor.Green);
                Runestone g2 =new Runestone(ItemWeight.Light, GemColor.Green);
                List<Runestone> list = new List<Runestone>() { y1, y2, b1, b2, g1, g2 };
                for(int i = 0; i < 6; i++)
                {
                    int region = UnityEngine.Random.Range(11, 67);
                    Debug.Log(region);
                    string nodeString = "R (" + region + ")";
                    GameObject node = GameObject.Find(nodeString);
                    node.GetComponent<Node>().GetRegion().addItem(list[i]);
                }
            }
            if(storyPoint!=StoryPoint.A&&storyPoint!=StoryPoint.C&&storyPoint!=StoryPoint.G&&storyPoint != GameObject.Find("Narrator").GetComponent<NarratorController>().getRunestone())
            {
                // event card
                List<Event> eventCards = GM.GetComponent<GameManager>().getEventCards();
                Event top = eventCards[0];
                top.applyEventEffect();
                eventCards.RemoveAt(0);
            }

            GM.pause = false;
            Destroy(this);
        }
    
    }
}
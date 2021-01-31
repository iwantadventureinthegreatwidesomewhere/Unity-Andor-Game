using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class NarratorController : MonoBehaviour
    {
        [SerializeField]
        List<StoryPoint> storyPoints = new List<StoryPoint>() { StoryPoint.A, StoryPoint.B, StoryPoint.C, StoryPoint.D, StoryPoint.E, StoryPoint.F, StoryPoint.G, StoryPoint.H, StoryPoint.I, StoryPoint.J, StoryPoint.K, StoryPoint.L, StoryPoint.M, StoryPoint.N };
        
        [SerializeField]
        int currentStoryPointIndex=0;

        [SerializeField]
        StoryPoint currentStoryPoint = StoryPoint.A;

        StoryPoint runestone;

        GameObject NarratorUI;
        GameObject DUI;
        GameObject EUI;
        GameObject TUI;
        GameObject okD;
        GameObject okE;
        GameObject okT;
        GameDifficulty gd;

        void Awake()
        {
            NarratorUI = GameObject.Find("NarratorUI").transform.GetChild(0).gameObject;
            gd = GameObject.Find("GameManager").GetComponent<GameManager>().getDifficulty();
            initializeUI();
            int runestoneIndex = UnityEngine.Random.Range(1, 7);
            while (runestoneIndex == 1 || runestoneIndex == 3)
            {
                //make sure won't happen at A and C
                runestoneIndex = UnityEngine.Random.Range(1, 7);
            }
            runestone = storyPoints[runestoneIndex-1];
        }

        public void advance()
        {
            if(GameObject.Find("GameManager").GetComponent<GameManager>().gameEnd)
                return;
            currentStoryPointIndex++;
            currentStoryPoint = storyPoints[currentStoryPointIndex];
            setPosition();
            checkEndGame();
            
        }

        public void checkEndGame()
        {
            if (currentStoryPoint == StoryPoint.N)
            {
                //TODO: end the game
                GameObject.Find("GameManager").GetComponent<GameManager>().gameEnd = true;
                GameManager gm = FindObjectOfType<GameManager>();
                if (gm.isSkralDefeated() && gm.HerbInCastle)
                {
                    //Heroes win
                    GameObject GameResultUI = GameObject.Find("GameResult").transform.GetChild(0).gameObject;
                    GameObject DUI = null;
                    string toDisplay = "Congrats! The legend ended well:\n\t\tWith their combined powers, the heroes were able to take the skral's stronghold. The medicinal herb did its work as well, and king Brandur soon felt better!";
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
                else
                {
                    string toDisplay = "";
                    //Heroes lose
                    if (!gm.isSkralDefeated() && gm.HerbInCastle)
                    {
                        toDisplay = "The Legend ended badly:\n\t\tThe skral on the tower was not defeated";
                    }
                    else if (gm.isSkralDefeated() && !gm.HerbInCastle)
                    {
                        toDisplay = "The Legend ended badly:\n\t\tThe medicinal herb was not in the castle space";
                    }
                    else
                    {
                        toDisplay = "The Legend ended badly:\n\t\tThe skral on the tower was not defeated\n\t\tThe medicinal herb was not in the castle space";
                    }
                    GameObject GameResultUI = GameObject.Find("GameResult").transform.GetChild(0).gameObject;
                    GameObject DUI = null;
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
        }

        public void jumpToN()
        {
            currentStoryPointIndex = 13;
            currentStoryPoint = StoryPoint.N;
            setPosition();
            checkEndGame();
        }

        public void setPosition()
        {
            if(currentStoryPoint== StoryPoint.A)
            {
                this.transform.position = new Vector3(7.22f, -0.2f, -4.65f);
            }
            if (currentStoryPoint == StoryPoint.B)
            {
                this.transform.position = new Vector3(7.22f, -0.2f, -3.91f);
            }
            if (currentStoryPoint == StoryPoint.C)
            {
                this.transform.position = new Vector3(7.22f, -0.2f, -3.17f);
            }
            if (currentStoryPoint == StoryPoint.D)
            {
                this.transform.position = new Vector3(7.22f, -0.2f, -2.43f);
            }
            if (currentStoryPoint == StoryPoint.E)
            {
                this.transform.position = new Vector3(7.22f, -0.2f, -1.69f);
            }
            if (currentStoryPoint == StoryPoint.F)
            {
                this.transform.position = new Vector3(7.22f, -0.2f, -1.06f);
            }
            if (currentStoryPoint == StoryPoint.G)
            {
                this.transform.position = new Vector3(7.22f, -0.2f, -0.37f);
            }
            if (currentStoryPoint == StoryPoint.H)
            {
                this.transform.position = new Vector3(7.22f, -0.2f, 0.37f);
            }
            if (currentStoryPoint == StoryPoint.I)
            {
                this.transform.position = new Vector3(7.22f, -0.2f, 1.1f);
            }
            if (currentStoryPoint == StoryPoint.J)
            {
                this.transform.position = new Vector3(7.22f, -0.2f, 1.8f);
            }
            if (currentStoryPoint == StoryPoint.K)
            {
                this.transform.position = new Vector3(7.22f, -0.2f, 2.48f);
            }
            if (currentStoryPoint == StoryPoint.L)
            {
                this.transform.position = new Vector3(7.22f, -0.2f, 3.23f);
            }
            if (currentStoryPoint == StoryPoint.M)
            {
                this.transform.position = new Vector3(7.22f, -0.2f, 3.95f);
            }
            if (currentStoryPoint == StoryPoint.N)
            {
                this.transform.position = new Vector3(7.22f, -0.2f, 4.7f);
            }
        }

        public StoryPoint getCurrentStoryPoint()
        {
            return currentStoryPoint;
        }

        public StoryPoint getRunestone()
        {
            return runestone;
        }

        void initializeUI()
        {
            for (int i = 0; i < NarratorUI.transform.childCount; i++)
            {
                int indext = i;
                if (NarratorUI.transform.GetChild(i).transform.name == "Description")
                {
                    DUI = NarratorUI.transform.GetChild(i).gameObject;
                }
                if (NarratorUI.transform.GetChild(i).transform.name == "Effect")
                {
                    EUI = NarratorUI.transform.GetChild(i).gameObject;
                }
                if (NarratorUI.transform.GetChild(i).transform.name == "Task")
                {
                    TUI = NarratorUI.transform.GetChild(i).gameObject;
                }
                if (NarratorUI.transform.GetChild(i).transform.name == "OkDescription")
                {
                    okD = NarratorUI.transform.GetChild(i).gameObject;
                }
                if (NarratorUI.transform.GetChild(i).transform.name == "OkEffect")
                {
                    okE = NarratorUI.transform.GetChild(i).gameObject;
                }
                if (NarratorUI.transform.GetChild(i).transform.name == "OkTask")
                {
                    okT = NarratorUI.transform.GetChild(i).gameObject;
                }
            }
        }

        public void show(bool runestoneCard)
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().gameEnd) { return; }
            LegendCard card = null;
            if (!runestoneCard)
            {
                if (currentStoryPoint == StoryPoint.A)
                {
                    card = new LegendCard(LegendCardKind.A, gd);

                }
                else if (currentStoryPoint == StoryPoint.C)
                {
                    card = new LegendCard(LegendCardKind.C, gd);
                }
                else if (currentStoryPoint == StoryPoint.G)
                {
                    card = new LegendCard(LegendCardKind.G, gd);
                }
            }
            else
            {
                card = new LegendCard(LegendCardKind.R, gd);
            }
            DUI.GetComponent<Text>().text = card.getDescription();
            EUI.GetComponent<Text>().text = card.getEffect();
            TUI.GetComponent<Text>().text = card.getTask();
            DUI.SetActive(true);
            okD.SetActive(true);
            NarratorUI.SetActive(true);
        }

        public StoryPoint getStoryPoint()
        {
            return currentStoryPoint;
        }
    }
}
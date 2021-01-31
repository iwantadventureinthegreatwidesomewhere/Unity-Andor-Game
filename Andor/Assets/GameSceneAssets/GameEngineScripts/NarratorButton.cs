using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class NarratorButton : MonoBehaviour
    {
        public bool Pressed { get; set; }
        GameObject NarratorUI;
        GameObject DUI;
        GameObject EUI;
        GameObject TUI;
        GameObject okD;
        GameObject okE;
        GameObject okT;

        public void OnOkDescriptionPressed()
        {
            initializeUI();
            deactive();
            EUI.SetActive(true);
            okE.SetActive(true);
            NarratorUI.SetActive(true);
        }

        public void OnOkEffectPressed()
        {
            initializeUI();
            deactive();
            if (TUI.GetComponent<Text>().text == "")
            {
                EUI.SetActive(false);
                okE.SetActive(false);
                NarratorUI.SetActive(false);
                //apply event effect
                List<Event> eventCards = GameObject.Find("GameManager").GetComponent<GameManager>().getEventCards();
                Event top = eventCards[0];
                top.applyEventEffect();
                eventCards.RemoveAt(0);
            }
            else
            {
                TUI.SetActive(true);
                okT.SetActive(true);
                NarratorUI.SetActive(true);
            }
        }

        public void OnOkTaskPressed()
        {
            initializeUI();
            deactive();
            NarratorUI.SetActive(false);
            if (GameObject.Find("Narrator").GetComponent<NarratorController>().getCurrentStoryPoint() != StoryPoint.A) {
                //apply event effect
                List<Event> eventCards = GameObject.Find("GameManager").GetComponent<GameManager>().getEventCards();
                Event top = eventCards[0];
                top.applyEventEffect();
                eventCards.RemoveAt(0);
            }
        }

        void deactive()
        {
            initializeUI();
            DUI.SetActive(false);
            EUI.SetActive(false);
            TUI.SetActive(false);
            okD.SetActive(false);
            okE.SetActive(false);
            okT.SetActive(false);
        }

        void initializeUI()
        {
            NarratorUI = GameObject.Find("NarratorUI").transform.GetChild(0).gameObject;
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
    }
}

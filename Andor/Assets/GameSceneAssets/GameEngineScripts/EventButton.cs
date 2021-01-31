using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Scripts
{
    public class EventButton : MonoBehaviour
    {
        public bool Pressed { get; set; }

        public void OnButtonPressed()
        {
            deactive();
            GameObject.Find("EventParent").transform.GetChild(0).gameObject.SetActive(false);
            //When OK button pressed, apply the effect
            GameManager gm = FindObjectOfType<GameManager>();
            bool shield = false;
            foreach(GameObject g in gm.HeroList)
            {
                Hero hero = g.GetComponent<HeroController>().getHero();
                if(hero.UsedShield)
                {
                    hero.UsedShield = false;
                    shield = true;
                }
            }
            //If shield was used, don't apply effect
            if(!shield)
            {
                if (gm.ActiveEvent!=null)
                    gm.ActiveEvent.ApplyEffect();
            }
            gm.ActiveEvent = null;
        }

        public void OnOKButtonForEvent16Pressed()
        {
            deactive();
            List<Event> eventCards = GameObject.Find("GameManager").GetComponent<GameManager>().getEventCards();
            Event e = eventCards[0];
            // let the player decide
            e.showUI(e,true);
        }

        public void OnRemoveButtonPressed()
        {
            deactive();
            GameObject.Find("GameManager").GetComponent<GameManager>().getEventCards().RemoveAt(0);
            GameObject.Find("EventParent").transform.GetChild(0).gameObject.SetActive(false);
            Pressed = false;
        }

        public void OnYesButtonPressed()
        {
            deactive();
            GameObject.Find("EventParent").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("R (57)").GetComponent<Node>().GetRegion().removeEvent(EventKind.Event8);
            GameObject.Find("GameManager").GetComponent<GameManager>().updateEvent(EventKind.Event8, false, new Vector3(0,0,0), "", "");
            // To-Do: track which player hit the yes button. Finish the trading
        }

        void deactive()
        {
            GameObject EventUI= GameObject.Find("EventParent").transform.GetChild(0).gameObject; 
            for (int i = 0; i < EventUI.transform.childCount; i++)
            {
                int indext = i;
                if (EventUI.transform.GetChild(i).transform.name == "EventButton")
                {
                    EventUI.transform.GetChild(i).gameObject.SetActive(false);
                }
                if (EventUI.transform.GetChild(i).transform.name == "RemoveEvent")
                {
                    EventUI.transform.GetChild(i).gameObject.SetActive(false);
                }
                if (EventUI.transform.GetChild(i).transform.name == "PlaceBack")
                {
                    EventUI.transform.GetChild(i).gameObject.SetActive(false);
                }
                if (EventUI.transform.GetChild(i).transform.name == "EventYes")
                {
                    EventUI.transform.GetChild(i).gameObject.SetActive(false);
                }
                if (EventUI.transform.GetChild(i).transform.name == "EventNo")
                {
                    EventUI.transform.GetChild(i).gameObject.SetActive(false);
                }
                if (EventUI.transform.GetChild(i).transform.name == "EventButtonFor16")
                {
                    EventUI.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }
}

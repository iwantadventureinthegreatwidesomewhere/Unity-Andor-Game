using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class EventController : MonoBehaviour
    {
        EventKind aEventKind;

        public void setEventKind(EventKind pEventKind)
        {
            aEventKind = pEventKind;
        }

        public EventKind getEventKind()
        {
            return aEventKind;
        }

        void OnMouseDown()
        {
            //Do something
            GameObject EventUI = GameObject.Find("EventParent").transform.GetChild(0).gameObject;
            GameObject DUI = null;
            GameObject EUI = null;
            GameObject okButton=null;
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
                if (EventUI.transform.GetChild(i).transform.name == "EventButton")
                {
                    okButton = EventUI.transform.GetChild(i).gameObject;
                }
            }    
            DUI.GetComponent<Text>().text = aEventKind.getDescription();
            EUI.GetComponent<Text>().text =aEventKind.getEffect() ;
            okButton.SetActive(true);
            EventUI.SetActive(true);
        }
    }
}

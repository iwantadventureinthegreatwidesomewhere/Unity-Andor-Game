using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class WitchController : MonoBehaviour
    {
        [SerializeField]
        GameObject currentPosition;

        Witch witch;

        private void Start()
        {
            int cost = FindObjectOfType<GameManager>().HeroList.Count + 1;
            GameObject panel = GameObject.Find("WitchCanvas").transform.GetChild(0).gameObject;
            panel.SetActive(true);
            GameObject.Find("WitchsBrew").GetComponentInChildren<Text>().text += "\nCost: " + cost;
            panel.SetActive(false);
        }

        public void setPosition()
        {
            Vector3 pos = currentPosition.transform.position;
            pos.z -= 0.5f;
            transform.position = pos;

        }

        public void setNode(GameObject pos)
        {
            currentPosition = pos;
            setPosition();
        }

        public void setWitch(Witch pWitch)
        {
            witch = pWitch;
        }

        private void OnMouseDown()
        {
            Hero hero = FindObjectOfType<HeroController>().getHero();
            if (hero.getRank() == currentPosition.GetComponent<Node>().getRank())
            {
                GameObject panel = GameObject.Find("WitchCanvas").transform.GetChild(0).gameObject;
                panel.SetActive(true);
                GameObject.Find("WitchsBrew").GetComponent<ItemIcon>().SetItem(new WitchsBrew(ItemWeight.Light));
            }
        }
    }
}

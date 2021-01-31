using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts{
    public class MerchantController : MonoBehaviour
    {
        [SerializeField]
        GameObject currentPosition;

        Merchant merchant;
        public void setPosition(){
            Vector3 pos = currentPosition.transform.position;
            pos.z -= 0.5f;
            transform.position = pos;
        }

        public void setNode(GameObject pos){
            currentPosition = pos;
            setPosition(); 
        } 

        public void setMerchant(Merchant pMerchant){
            merchant = pMerchant;
        }

        public Merchant getMerchant()
        {
            return merchant;
        }

        private void OnMouseDown()
        {
            Hero hero = FindObjectOfType<HeroController>().getHero();
            if (hero.getRank() == currentPosition.GetComponent<Node>().getRank())
            {
                GameObject panel = GameObject.Find("MerchantCanvas").transform.GetChild(0).gameObject;
                panel.SetActive(true);
                GameObject.Find("Wineskin").GetComponent<ItemIcon>().SetItem(new Wineskin(ItemWeight.Light));
                GameObject.Find("Helm").GetComponent<ItemIcon>().SetItem(new Helm(ItemWeight.Light));
                GameObject.Find("Falcon").GetComponent<ItemIcon>().SetItem(new Falcon(ItemWeight.Heavy));
                GameObject.Find("Bow").GetComponent<ItemIcon>().SetItem(new Bow(ItemWeight.Heavy));
                GameObject.Find("Telescope").GetComponent<ItemIcon>().SetItem(new Telescope(ItemWeight.Light));
                GameObject.Find("Shield").GetComponent<ItemIcon>().SetItem(new Shield(ItemWeight.Heavy));
                GameObject.Find("SP").GetComponent<ItemIcon>().merchant = merchant;
            }
        }
    }
}
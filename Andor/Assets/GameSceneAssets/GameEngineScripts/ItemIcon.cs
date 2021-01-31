using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class ItemIcon : MonoBehaviour
    {
        public List<Sprite> sprites;
        public Item item;
        private Sprite sprite;
        public bool inShop;
        public Merchant merchant;
        public bool wp;
        public bool sp;

        public bool IsEmpty()
        {
            return item == null;
        }

        public void SetItem(Item item)
        {
            this.item = item;
            if(item == null)
            {
                SetSprite("default");
                return;
            }
            System.Type type = item.GetType();
            if(type == typeof(Wineskin))
            {
                if(((Wineskin)item).getItemFullness() == ItemFullness.Full)
                {
                    SetSprite("wineskin_full");
                }
                else
                {
                    SetSprite("wineskin_half");
                }
            }
            else if(type == typeof(Shield))
            {
                if(((Shield)item).getItemDurability() == ItemDurability.New)
                {
                    SetSprite("shield_new");
                }
                else
                {
                    SetSprite("shield_used");
                }
            }
            else if(type == typeof(Bow))
            {
                SetSprite("bow");
            }
            else if(type == typeof(Helm))
            {
                SetSprite("helm");
            }
            else if (type == typeof(Telescope))
            {
                SetSprite("telescope");
            }
            else if (type == typeof(Falcon))
            {
                SetSprite("falcon");
            }
            else if (type == typeof(WitchsBrew))
            {
                if (((WitchsBrew)item).getItemFullness() == ItemFullness.Full)
                {
                    SetSprite("brew_full");
                }
                else
                {
                    SetSprite("brew_half");
                }
            }
            else if (type == typeof(MedicinalHerb))
            {
                int strength = ((MedicinalHerb)item).getStrength();
                SetSprite("herb_" + strength);
            }
            else if (type == typeof(Runestone))
            {
                string colour = ((Runestone)item).getColor().ToString();
                SetSprite("runestone_" + colour);
            }
        }

        private void SetSprite(string name)
        {
            sprite = sprites.Find(s => s.name.Equals(name));
            GetComponent<Image>().sprite = sprite;
        }

        public void OnButtonPressed()
        {
            if (inShop)
            {
                Hero hero = FindObjectOfType<HeroController>().getHero();
                if(item == null && sp)
                {
                    if (hero.getHeroKind() == HeroKind.Dwarf && merchant.isMine && hero.getGold() >= 1)
                    {
                        hero.setStrengthPoints(hero.getStrengthPoints() + 1);
                        hero.IncrementGold(-1);
                    }
                    else if(hero.getGold() >= 2)
                    {
                        hero.setStrengthPoints(hero.getStrengthPoints() + 1);
                        hero.IncrementGold(-2);
                    }
                }
                //Buy from witch
                else if (item.GetType() == typeof(WitchsBrew))
                {
                    int cost = FindObjectOfType<GameManager>().HeroList.Count + 1;
                    //Archer discount
                    if (hero.getHeroKind() == HeroKind.Archer && hero.getGold() >= cost - 1)
                    {
                        hero.addItem(new WitchsBrew(ItemWeight.Light));
                        hero.IncrementGold(-1 * (cost - 1));
                    }
                    else if (hero.getGold() >= cost)
                    {
                        hero.addItem(new WitchsBrew(ItemWeight.Light));
                        hero.IncrementGold(-1 * cost);
                    }
                }
                else if (hero.getGold() >= 2)
                {
                    hero.addItem(item);
                    hero.IncrementGold(-2);
                }
            }
            else if (item == null)
            {
                return;
            }
            else if(item.GetType().IsSubclassOf(typeof(UsableItem)))
            {
                ((UsableItem) item).Use();
            }
        }
    }
}


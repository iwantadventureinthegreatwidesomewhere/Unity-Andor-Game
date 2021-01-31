using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class Die : MonoBehaviour
    {
        public static int[] blackSides = { 6, 6, 8, 10, 10, 12 };

        public Sprite[] sides;
        public bool isBlack;
        DieValue dieValue = new DieValue(0);

        public int Roll()
        {
            int roll = Random.Range(0, 6);
            GetComponent<Image>().sprite = sides[roll];
            if(isBlack)
            {
                dieValue.value = blackSides[roll];
                return blackSides[roll];
            }
            dieValue.value = roll + 1;
            return roll + 1;
        }

        //Flip die to opposite face when clicked
        //Use for wizard power
        public void OnButtonPressed()
        {
            //TODO: find whether current hero used brew
            Hero hero = FindObjectOfType<HeroController>().getHero();
            if(hero.UsedBrew)
            {
                hero.UsedBrew = false;
                dieValue.value *= 2;
                GetComponent<Image>().color = Color.red;
            }
            //TODO: find whether current hero is wizard
            else if(hero.getHeroKind() == HeroKind.Wizard && !hero.UsedHeroPower)
            {
                hero.UsedHeroPower = true;
                int face = 0;
                for(int i = 0; i < 6; i++)
                {
                    if(GetComponent<Image>().sprite.Equals(sides[i]))
                    {
                        face = i;
                        break;
                    }
                }
                GetComponent<Image>().sprite = sides[5 - face];
                if(isBlack)
                {
                    dieValue.value = blackSides[5 - face];
                }
                else
                {
                    dieValue.value = 6 - face;
                }
            }
        }

        public DieValue GetValue()
        {
            return dieValue;
        }
    }
}


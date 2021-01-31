using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class CombatParty : IEnumerable<Hero>
    {
        Hero partyLeader;
        List<Hero> heroes;

        public CombatParty(Hero hero)
        {
            partyLeader = hero;
            hero.InCombat = true;
            heroes = new List<Hero> { hero };
        }

        #region Enumerator

        public IEnumerator<Hero> GetEnumerator()
        {
            return heroes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public void AddHero(Hero hero)
        {
            heroes.Add(hero);
            hero.InCombat = true;
        }

        public void RemoveHero(Hero hero)
        {
            heroes.Remove(hero);
            hero.InCombat = false;
            if (hero.Equals(partyLeader) && !IsEmpty())
            {
                partyLeader = heroes[0];
            }
        }

        public Hero GetLeader()
        {
            return partyLeader;
        }

        public bool IsEmpty()
        {
            return heroes.Count == 0;
        }
    }
}

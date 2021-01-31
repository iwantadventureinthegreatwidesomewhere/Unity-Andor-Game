using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts
{
    public abstract class Combatant : TileUnit
    {
        protected int maxStrength;
        protected int maxWillpower;
        protected int numRegularDice;
        protected int numBlackDice;

        protected int strengthPoints;
        protected int willpowerPoints;

        protected int diceRemaining; //For archer/bow
        protected int blackDiceRemaining;

        protected Combatant()
        {
            
        }

        protected Combatant(Region region) : base(region)
        {
            
        }

        public void TakeDamage(int damage)
        {
            willpowerPoints -= damage;
            if (willpowerPoints < 1)
            {
                willpowerPoints = 1;
            }
            //When you drop below an hp threshold, you may lose dice
            UpdateDice();
        }

        public bool IsDead()
        {
            return willpowerPoints <= 0;
        }

        //Reset dice after attacking with bow
        public void ArcherResetDice()
        {
            diceRemaining = numRegularDice;
            blackDiceRemaining = numBlackDice;
        }

        //public abstract void Initialize();
        public abstract int CalculateBattleValue();
        public abstract void Die();
        protected abstract void UpdateDice();

        #region Die-rolling methods

        public int RollRegularDie()
        {
            return Random.Range(1, 7);
        }

        public int RollBlackDie()
        {
            return Scripts.Die.blackSides[Random.Range(0,6)];
        }

        //The one where you take the highest value rolled
        protected int HeroRollMethod()
        {
            CombatManager combatManager = CombatManager.instance;
            List<int> rolls;
            if(GetMonsterKind() == MonsterKind.NotMonster)
            {
                rolls = combatManager.HeroRoll(numRegularDice, numBlackDice);
            }
            else
            {
                rolls = combatManager.MonsterRoll(numRegularDice, numBlackDice);
            }
            combatManager.DoneRolling = true;
            return strengthPoints + rolls.Max();
        }

        protected int HeroRollMethod(List<DieValue> values)
        {
            CombatManager combatManager = CombatManager.instance;
            List<int> rolls = new List<int>();
            foreach(DieValue v in values)
            {
                rolls.Add(v.value);
            }
            combatManager.DoneRolling = true;
            return strengthPoints + rolls.Max();
        }

        //The one where you use the sum of identical rolls
        protected int HelmRollMethod()
        {
            CombatManager combatManager = CombatManager.instance;
            List<int> rolls;
            if (GetMonsterKind() == MonsterKind.NotMonster)
            {
                rolls = combatManager.HeroRoll(numRegularDice, numBlackDice);
            }
            else
            {
                rolls = combatManager.MonsterRoll(numRegularDice, numBlackDice);
            }
            int sumIdentical = rolls.GroupBy(i => i)
                .Select(grp => grp.Sum())
                .Max();
            combatManager.DoneRolling = true;
            return strengthPoints + Math.Max(rolls.Max(), sumIdentical);
        }

        protected int HelmRollMethod(List<DieValue> values)
        {
            CombatManager combatManager = CombatManager.instance;
            List<int> rolls = new List<int>();
            foreach (DieValue v in values)
            {
                rolls.Add(v.value);
            }
            int sumIdentical = rolls.GroupBy(i => i)
                .Select(grp => grp.Sum())
                .Max();
            combatManager.DoneRolling = true;
            return strengthPoints + Math.Max(rolls.Max(), sumIdentical);
        }

        //The one where you keep rolling until you decide to stop
        protected int ArcherRollMethod()
        {
            CombatManager combatManager = CombatManager.instance;
            combatManager.DisplayDoneRollingButton(true);
            List<int> rolls;
            //Prioritize rolling black dice
            int black;
            int regular;
            if(blackDiceRemaining > 0)
            {
                black = 1;
                regular = 0;
                blackDiceRemaining--;
            }
            else
            {
                black = 0;
                regular = 1;
                diceRemaining--;
            }
            if (GetMonsterKind() == MonsterKind.NotMonster)
            {
                rolls = combatManager.HeroRoll(regular, black);
            }
            else
            {
                rolls = combatManager.MonsterRoll(regular, black);
            }
            //Finished if you are out of dice
            if(blackDiceRemaining == 0 && diceRemaining == 0)
            {
                combatManager.DoneRolling = true;
                combatManager.DisplayDoneRollingButton(false);
            }
            return strengthPoints + rolls[0];
        }

        protected int ArcherRollMethod(List<DieValue> values)
        {
            CombatManager combatManager = CombatManager.instance;
            List<int> rolls = new List<int>();
            foreach (DieValue v in values)
            {
                rolls.Add(v.value);
            }
            combatManager.DoneRolling = true;
            return strengthPoints + rolls[rolls.Count - 1];
        }

        #endregion
    }
}


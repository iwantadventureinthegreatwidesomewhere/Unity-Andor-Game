using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
	public class Monster : Combatant, Hideable
	{
		private MonsterKind monsterKind;
		private int rewardValue;

		private bool isOnTower;
		private bool hidden;

        public Monster(Region region, MonsterKind monsterKind, bool isOnTower) : base(region)
		{
            this.monsterKind = monsterKind;
            this.isOnTower = isOnTower;
            SetStats();
            
            strengthPoints = maxStrength;
            willpowerPoints = maxWillpower;
            hidden = false;
		}

        //Set monster's stats based on monsterKind
        private void SetStats()
        {
            if(isOnTower)
            {
                //TODO: boss monster - set stats according to Legend
                //TODO: need to know #players
                int numPlayers = GameObject.Find("GameManager").GetComponent<GameManager>().getHeroList().Count;
                if (GameObject.Find("GameManager").GetComponent<GameManager>().getDifficulty() == GameDifficulty.Easy)
                {
                    maxWillpower = 6;
                    maxStrength = (numPlayers-1) * 10;
                    numRegularDice = 2;
                    rewardValue = 4;
                    return;
                }
                else
                {
                    maxWillpower = 6;
                    maxStrength = numPlayers * 10;
                    numRegularDice = 2;
                    rewardValue = 4;
                    return;
                }
            }

            switch (monsterKind)
            {
                case MonsterKind.Gor:
                    maxStrength = 2;
                    maxWillpower = 4;
                    numRegularDice = 2;
                    rewardValue = 2;
                    break;
                case MonsterKind.Skral:
                    maxStrength = 6;
                    maxWillpower = 6;
                    numRegularDice = 2;
                    rewardValue = 4;
                    break;
                case MonsterKind.Troll:
                    maxStrength = 14;
                    maxWillpower = 12;
                    numRegularDice = 3;
                    rewardValue = 6;
                    break;
                case MonsterKind.Wardrak:
                    maxStrength = 10;
                    maxWillpower = 7;
                    numBlackDice = 2;
                    rewardValue = 6;
                    break;
            }
        }

		public void hide()
		{
			hidden = true;
		}

		public override bool isHidden()
		{
			return hidden;
		}

		public void reveal()
		{
			hidden = false;
		}

        public override MonsterKind GetMonsterKind(){
            return monsterKind;
        }

        public int GetRewardValue()
        {
            return rewardValue;
        }

        public override int CalculateBattleValue()
        {
            return HelmRollMethod();
        }
        //Method called when monster is defeated in battle
        public override void Die()
        {
            //Increment narrator
            //Destroy monster
            //End battle
            //Split rewards
        }
        //Method called when monster takes damage / gets healed
        protected override void UpdateDice()
        {
            if (willpowerPoints < maxWillpower / 2)
            {
                switch (monsterKind)
                {
                    case MonsterKind.Troll:
                        numRegularDice = 2;
                        break;
                    case MonsterKind.Wardrak:
                        numBlackDice = 1;
                        break;
                }
            }
            else
            {
                switch (monsterKind)
                {
                    case MonsterKind.Troll:
                        numRegularDice = 3;
                        break;
                    case MonsterKind.Wardrak:
                        numBlackDice = 2;
                        break;
                }
            }
        }
        //Method called when a battle ends and the monster is not defeated
        public void Recover()
        {
            strengthPoints = maxStrength;
            willpowerPoints = maxWillpower;
            UpdateDice();
        }

        public string getMonsterName(){
            string name = "";
            switch(monsterKind){
                case MonsterKind.Skral:
                    name ="Skral";
                    break;
                case MonsterKind.Gor:
                    name = "Gor";
                    break;
                case MonsterKind.Wardrak:
                    name = "Wardrak";
                    break;
                case MonsterKind.Troll:
                    name = "Troll";
                    break;
            }
            return name;
        }

        public int getWP(){
            return willpowerPoints;
        }

        public int getSP(){
            return strengthPoints;
        }

        public void setSP(int pSP)
        {
            strengthPoints = pSP;
        }

        public bool onTower()
        {
            return isOnTower;
        }
    }
}

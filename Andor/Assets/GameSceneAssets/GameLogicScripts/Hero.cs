using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts
{
	public class Hero : Combatant
	{
		private Game game;
		private HeroKind heroKind;
		private int heroRank;
		private HeroController controller;
		//private TimeTrackToken timeTrackToken;

		private Item[] lightItems;
		private Item heavyItem;
		private Helm helm;
		private List<Farmer> farmers;

		private int gold;
		private bool dayEnded;
		private bool hasHerb;

		public bool UsedHeroPower { get; set; } //Wizard ability
		public int FreeMoveSpaces { get; set; } //For wineskin, herb
        public bool InCombat { get; set; }
        public bool UsedShield { get; set; }
        public bool UsedBrew { get; set; }
        public int StrengthBoost { get; set; } //For increased battle value from herb

		public Hero(Game game, HeroKind heroKind, HeroController controller) : base()
		{
			this.game = game;
			this.heroKind = heroKind;
			this.controller = controller;
			heroRank = startingHeroRank(heroKind);

			gold = -1;
			maxStrength = 14;
			maxWillpower = 20;
			strengthPoints = -1;
			willpowerPoints = -1;

			//TODO: For testing - don't forget to remove later I commented it out for you cause i need to test online
			/*
			strengthPoints = 1;
			willpowerPoints = 7;
			UpdateDice();
			lightItems = new Item[3];
			addItem(new Wineskin(ItemWeight.Light));
			addItem(new Runestone(ItemWeight.Light, GemColor.Blue));
            */
		}

        public bool setupHero()
		{
            if(game != null)
			{
				region = game.getRegionByRank(startingRegionRank(heroKind));
                region.addTileUnit(this);
				lightItems = new Item[3];
				farmers = new List<Farmer>();

				gold = 0;
				strengthPoints = 1;
				willpowerPoints = 7;

				UpdateDice();
			}

			return false;
		}

        public HeroKind getHeroKind()
		{
			return heroKind;
		}

		public void setRank(int pRank){
			heroRank = pRank;
		}
		public int getRank(){
			return heroRank;
		}
		public Item[] getLightItems()
		{
			return lightItems;
		}

		public Item getHeavyItem()
		{
			return heavyItem;
		}


        public void addItem(Item item)
		{
            //TODO: Ask if player wants to replace an item if inventory full
			if(item.GetType() == typeof(Helm))
            {
				helm = (Helm) item;
				controller.UpdateItemIcons(item, 0);
			}
            else if(item.getItemWeight() == ItemWeight.Light)
            {
				for(int i = 0; i < 3; i++)
				{
					if (lightItems[i] == null)
					{
						lightItems[i] = item;
						controller.UpdateItemIcons(item, i + 2);
						break;
					}
                }
			}
            else
            {
				heavyItem = item;
				controller.UpdateItemIcons(item, 1);
			}
            //If you have all 3 runestones, get black die
            if(lightItems.All(i => i != null && i.GetType() == typeof(Runestone)))
            {
                if(lightItems.Any(i => ((Runestone) i).getColor() == GemColor.Blue)
                    && lightItems.Any(i => ((Runestone)i).getColor() == GemColor.Green)
                    && lightItems.Any(i => ((Runestone)i).getColor() == GemColor.Yellow))
                {
					numRegularDice--;
					numBlackDice++;
                }
            }
            //TODO: Check this when dropping/trading items too
            else if(numBlackDice > 0)
            {
				numBlackDice--;
				numRegularDice++;
            }
			item.SetOwner(this);
		}

        //Removes item and returns status
        public bool DiscardItem(Item item)
		{
            if(item.GetType() == typeof(Helm) && helm.Equals(item))
			{
				helm = null;
				controller.UpdateItemIcons(null, 0);
				return true;
			}else if(item.getItemWeight() == ItemWeight.Heavy && heavyItem.Equals(item))
			{
				heavyItem = null;
				controller.UpdateItemIcons(null, 1);
				return true;
			}else if(item.getItemWeight() == ItemWeight.Light)
			{
				for (int i = 0; i < 3; i++)
				{
					if (lightItems[i] == null)
						continue;
					if (lightItems[i].Equals(item))
					{
						lightItems[i] = null;
						controller.UpdateItemIcons(null, i + 2);
						return true;
					}
				}
			}

			return false;
		}

        public void UpdateItemState(Item item)
        {
			if (item.GetType() == typeof(Helm))
			{
				controller.UpdateItemIcons(item, 0);

			}
			else if (item.getItemWeight() == ItemWeight.Light)
			{
				for (int i = 0; i < 3; i++)
				{
					if (lightItems[i] == null)
						continue;
					if (lightItems[i].Equals(item))
					{
						controller.UpdateItemIcons(item, i + 2);
						break;
					}
				}
			}
			else
			{
				heavyItem = null;
				controller.UpdateItemIcons(item, 1);
			}
		}

        public bool IsCarrying(Item item)
        {
			if (item.GetType() == typeof(Helm))
			{
				return item.Equals(helm);
			}
			if (item.getItemWeight() == ItemWeight.Heavy)
			{
				return item.Equals(heavyItem);
			}
			return lightItems.Contains(item);
        }
        //TakeDamage method already exists in Combatant script
		/*public void TakeDamage(int damage)
        {
			willpowerPoints= willpowerPoints - damage;
		}*/

		public bool addFarmer(Farmer farmer)
		{
			if (!farmers.Contains(farmer))
			{
				farmers.Add(farmer);
				return true;
			}

			return false;
		}
		
		
		public void removeFarmers() {
			farmers.Clear();
		}

		public List<Farmer> getFarmers()
		{
			return farmers;
		}

		public int getGold()
		{
			return gold;
		}

		public void setGold(int gold)
		{
			if (gold >= 0)
			{
				this.gold = gold;
			}
		}

        public void IncrementGold(int gold)
        {
			this.gold += gold;
            if(this.gold < 0)
            {
				this.gold = 0;
            }
        }

		public int getStrengthPoints()
		{
			return strengthPoints;
		}

		public void setStrengthPoints(int strengthPoints)
		{
			if (strengthPoints >= 0)
			{
				this.strengthPoints = strengthPoints;
			}
		}

		public int getWillpowerPoints()
		{
			return willpowerPoints;
		}

		public void setWillpowerPoints(int willpowerPoints)
		{
			if (willpowerPoints >= 0)
			{
				this.willpowerPoints = willpowerPoints;
			}
			UpdateDice();
		}

		private int startingHeroRank(HeroKind heroKind)
		{
            if(game.getLegend() == Legend.Legend2)
			{
                if(heroKind == HeroKind.Archer)
				{
					return 25;
				}
                else if(heroKind == HeroKind.Dwarf)
				{
					return 7;
				}
                else if(heroKind == HeroKind.Warrior)
                {
					return 14;
                }
                else if(heroKind == HeroKind.Wizard)
				{
					return 34;
				}
			}

			return -1;
		}

		private int startingRegionRank(HeroKind heroKind)
		{
			if (game.getLegend() == Legend.Legend2)
			{
				if (heroKind == HeroKind.Archer)
				{
					return 25;
				}
				else if (heroKind == HeroKind.Dwarf)
				{
					return 7;
				}
				else if (heroKind == HeroKind.Warrior)
				{
					return 14;
				}
				else if (heroKind == HeroKind.Wizard)
				{
					return 34;
				}
			}

			return -1;
		}

		public override int CalculateBattleValue()
		{
			if (heroKind == HeroKind.Archer || IsCarrying(new Bow(ItemWeight.Heavy)))
			{
				return ArcherRollMethod();
			}
			else if (IsCarrying(new Helm(ItemWeight.Light)))
			{
				return HelmRollMethod();
			}
			else
			{
				return HeroRollMethod();
			}
		}

		public int CalculateBattleValue(List<DieValue> heroRolls)
		{
			if (heroKind == HeroKind.Archer || IsCarrying(new Bow(ItemWeight.Heavy)))
			{
				return ArcherRollMethod(heroRolls);
			}
			else if(IsCarrying(new Helm(ItemWeight.Light)))
			{
				return HelmRollMethod(heroRolls);
			}
			else
			{
				return HeroRollMethod(heroRolls);
			}
		}
		//Method called when hero reaches 0 wp during combat
		public override void Die()
		{
			if (strengthPoints > 1)
			{
				strengthPoints--;
			}
			willpowerPoints = 3;
		}

        public void Heal(int amount)
        {
			willpowerPoints += amount;
            if(willpowerPoints > maxWillpower)
            {
				willpowerPoints = maxWillpower;
            }
			UpdateDice();
        }

		//Method called when hero takes damage or gets healed
		//Changes numRegularDice based on the hero's wp
        //TODO: deal with black dice
		protected override void UpdateDice()
		{
			if (willpowerPoints <= 6)
			{
				switch (heroKind)
				{
					case HeroKind.Archer:
						numRegularDice = 3;
						break;
					case HeroKind.Dwarf:
						numRegularDice = 1;
						break;
					case HeroKind.Warrior:
						numRegularDice = 2;
						break;
				}
			}
			else if (willpowerPoints >= 14)
			{
				switch (heroKind)
				{
					case HeroKind.Archer:
						numRegularDice = 5;
						break;
					case HeroKind.Dwarf:
						numRegularDice = 3;
						break;
					case HeroKind.Warrior:
						numRegularDice = 4;
						break;
				}
			}
			else
			{
				switch (heroKind)
				{
					case HeroKind.Archer:
						numRegularDice = 4;
						break;
					case HeroKind.Dwarf:
						numRegularDice = 2;
						break;
					case HeroKind.Warrior:
						numRegularDice = 3;
						break;
					case HeroKind.Wizard:
						numRegularDice = 1;
						break;
				}
			}
			ArcherResetDice();
		}

		public void AdvanceTimeTrack()
		{
			controller.TimeTrackTick();
		}

        public bool HasTimeLeft()
        {
			return controller.HasTimeLeft();
        }

		public bool dropGold(int gold)
        {
            if (this.gold >= gold)
            {
				this.gold -= gold;
				region.setGold(region.getGold() + gold);
				return true;
            }
			return false;		// no enough gold on hero to drop
        }

		public bool pickUpGold(int gold)
        {
            if (region.getGold() > gold)
            {
				region.setGold(region.getGold()-gold);
				this.gold += gold;
				return true;
			}
			return false;		// no enough gold on ground to pick
        }

		public void pickUpAllItemOnGround(Region region)
        {
			if (region.hasItem())
			{
				List<Item> itemsOnGround = region.getItems();
				foreach (Item i in itemsOnGround)
				{
					addItem(i);
				}
				itemsOnGround.Clear();
			}
		}

		public void pickUpItemOnGround(Region region, Item item)
		{
			if (region.hasItem())
			{
				List<Item> itemsOnGround = region.getItems();
				Item toPick = null;
				foreach (Item i in itemsOnGround)
				{
                    if (i.GetType() == item.GetType())
                    {
						toPick = i;
						addItem(i);
                    }
				}
				itemsOnGround.Remove(toPick);
			}
		}

        public void RevealAdjacentFog()
        {
			controller.RevealAdjacentFog();
        }

		public int getHeroRank()
        {
			return heroRank;
        }

		public void setHerb(bool herb)
        {
			hasHerb = herb;
        }

		public bool getHerb()
        {
			return hasHerb;
        }
	}
}

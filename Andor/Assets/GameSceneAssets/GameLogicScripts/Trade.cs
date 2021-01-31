using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
	public class Trade
	{
		private TradeStatus tradeStatus;
		private Hero hero1;
		private Hero hero2;
		private int goldToHero1;
		private List<Item> itemsToHero1;
		private int goldToHero2;
		private List<Item> itemsToHero2;
		private Falcon falcon;

        public Trade(Hero hero1, Hero hero2, int goldToHero1, List<Item> itemsToHero1, int goldToHero2, List<Item> itemsToHero2, Falcon falcon)
		{
			this.hero1 = hero1;
			this.hero2 = hero2;
			this.goldToHero1 = goldToHero1;
			this.itemsToHero1 = itemsToHero1;
			this.goldToHero2 = goldToHero2;
			this.itemsToHero2 = itemsToHero2;
			this.falcon = falcon;
			tradeStatus = TradeStatus.Hero2;
		}

        public bool CounterTrade(Hero sender, int goldToSend, List<Item> itemsToSend, int goldToReceive, List<Item> itemsToReceive)
		{
            if(sender == hero1 && tradeStatus == TradeStatus.Hero1)
			{
				this.goldToHero1 = goldToReceive;
				this.itemsToHero1 = itemsToReceive;
				this.goldToHero2 = goldToSend;
				this.itemsToHero2 = itemsToSend;
				tradeStatus = TradeStatus.Hero2;
				return true;
			}
			else if(sender == hero2 && tradeStatus == TradeStatus.Hero2)
			{
				this.goldToHero1 = goldToSend;
				this.itemsToHero1 = itemsToSend;
				this.goldToHero2 = goldToReceive;
				this.itemsToHero2 = itemsToReceive;
				tradeStatus = TradeStatus.Hero1;
				return true;
			}
			else
			{
				return false;
			}
		}

        public bool AcceptTrade(Hero sender)
		{
			if (sender == hero1 && tradeStatus == TradeStatus.Hero1)
			{
				tradeStatus = TradeStatus.Final;
				return true;
			}
			else if (sender == hero2 && tradeStatus == TradeStatus.Hero2)
			{
				tradeStatus = TradeStatus.Final;
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool DeclineTrade(Hero sender)
		{
			if (sender == hero1 && tradeStatus == TradeStatus.Hero1)
			{
				tradeStatus = TradeStatus.Cancelled;
				return true;
			}
			else if (sender == hero2 && tradeStatus == TradeStatus.Hero2)
			{
				tradeStatus = TradeStatus.Cancelled;
				return true;
			}
			else
			{
				return false;
			}
		}

        public bool ExecuteExchange()
		{
            //checks that both heroes still have the gold and items to be traded
            if(hero1.getGold() < goldToHero2
                || hero2.getGold() < goldToHero1
                || falcon.getUsedToday())
			{
				return false;
			}

            foreach(Item i in itemsToHero2)
			{
				if (!hero1.IsCarrying(i))
				{
					return false;
				}
			}

            foreach(Item i in itemsToHero1)
			{
				if (!hero2.IsCarrying(i))
				{
					return false;
				}
			}

            //executes the trade
            if(tradeStatus == TradeStatus.Final)
			{
				//removes items
				hero1.setGold(hero1.getGold() - goldToHero2);
				hero2.setGold(hero2.getGold() - goldToHero1);

				foreach (Item i in itemsToHero2)
				{
					hero1.DiscardItem(i);
				}

                foreach(Item i in itemsToHero1)
				{
					hero2.DiscardItem(i);
				}

                //adds items
				hero1.IncrementGold(goldToHero1);
				hero2.IncrementGold(goldToHero2);

                foreach(Item i in itemsToHero1)
				{
					hero1.addItem(i);
				}

                foreach(Item i in itemsToHero2)
				{
					hero2.addItem(i);
				}

				falcon.setUsedToday(true);

				return true;
			}
			else
			{
				return false;
			}
		}

		public Hero getHero1()
		{
			return hero1;
		}

        public Hero getHero2()
		{
			return hero2;
		}

        public int getGoldToHero1()
		{
			return goldToHero1;
		}

        public List<Item> getItemsToHero1()
		{
			return itemsToHero1;
		}

        public int getGoldToHero2()
		{
			return goldToHero2;
		}

        public List<Item> getItemsToHero2()
		{
			return itemsToHero2;
		}

		public Falcon getFalcon()
		{
			return falcon;
		}
	}

	public enum TradeStatus
	{
        Hero1, Hero2, Final, Cancelled
	}
}

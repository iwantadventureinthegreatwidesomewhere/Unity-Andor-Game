using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{

	public static class TradeManager
	{
		public static List<Trade> activeTrades = new List<Trade>();
        //histical trades = past trades
		public static List<Trade> historicalTrades = new List<Trade>();

		//returns success status of action
		public static bool ProposeTrade(Hero sender, Hero recipient, int goldToSend, List<Item> itemsToSend, int goldToReceive, List<Item> itemsToReceive, Falcon falcon)
		{
			if (falcon != null && falcon.getUsedToday())
			{
				//falcon was already used today
				return false;
			}

			Trade trade = new Trade(sender, recipient, goldToReceive, itemsToReceive, goldToSend, itemsToSend, falcon);
			activeTrades.Add(trade);

			return true;
		}

		//returns success status of action
		public static bool CounterTrade(Trade trade, Hero sender, int goldToSend, List<Item> itemsToSend, int goldToReceive, List<Item> itemsToReceive)
		{
			bool status = trade.CounterTrade(sender, goldToSend, itemsToSend, goldToReceive, itemsToReceive);
			return status;
		}

		//returns success status of action
		public static bool AcceptTrade(Trade trade, Hero sender)
		{
			bool status = trade.AcceptTrade(sender);

			if (status)
			{
				trade.ExecuteExchange();
				activeTrades.Remove(trade);
				historicalTrades.Add(trade);
			}

			return status;
		}

        //returns success status of action
        public static bool DeclineTrade(Trade trade, Hero sender)
		{
			bool status = trade.DeclineTrade(sender);

			if (status)
			{
				activeTrades.Remove(trade);
				historicalTrades.Add(trade);
			}

			return status;
		}

        //returns list of active trades
        public static List<Trade> GetHerosActiveTrades(Hero hero)
		{
			List<Trade> heroTrades = new List<Trade>();

            foreach(Trade trade in activeTrades)
			{
                if(trade.getHero1() == hero || trade.getHero2() == hero)
				{
					heroTrades.Add(trade);
				}
			}

			return heroTrades;
		}
	}
}

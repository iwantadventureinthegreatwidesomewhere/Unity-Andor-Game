using System;
using System.Collections;
using System.Collections.Generic;
//using System.Security.Policy;
using UnityEngine;

namespace Scripts
{
	public class Region
	{
		private int rank;
		private List<TileUnit> tileUnits;
		private int gold;
		private List<Item> itemsOnGround;
		private List<EventKind> events=new List<EventKind>();
		private bool hasHerb;

        public Region(int rank) 
		{
			this.rank = rank;
			tileUnits = new List<TileUnit>();
			itemsOnGround = new List<Item>();
		}

        public int getRank()
		{
			return rank;
		}

		public void setGold(int gold)
        {
			this.gold = gold; 
        }

		public int getGold()
        {
			return this.gold;
        }
		
        public bool addTileUnit(TileUnit tileUnit)
		{
			if (!tileUnits.Contains(tileUnit))
			{
				tileUnits.Add(tileUnit);
				return true;
			}

			return false;
		}

        public List<TileUnit> getTileUnits()
		{
			return tileUnits;
		}

        public bool removeTileUnit(TileUnit tileUnit)
		{
			if (tileUnits.Contains(tileUnit))
			{
				tileUnits.Remove(tileUnit);
				return true;
			}

			return false;
		}

		public void addItem(Item pItem)
        {
			itemsOnGround.Add(pItem); 
        }

		public bool hasItem()
        {
			if (itemsOnGround.Count == 0)
				return false;
			else
				return true;
        }

		public List<Item> getItems()
        {
			return itemsOnGround;
        }

		public void addEvent(EventKind pEK)
        {
			events.Add(pEK);
        }

		public List<EventKind> getEvents()
        {
			return events;
        }

		public void removeEvent(EventKind pEK)
        {
			events.Remove(pEK);
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

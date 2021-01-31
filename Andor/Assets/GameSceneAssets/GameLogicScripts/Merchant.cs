using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
	public class Merchant : TileUnit
	{
		// price is always2, so changed the dictionary to a list
		//private Dictionary<Item, int> itemsForSale;
		private List<ItemForSale> itemsForSale;
		public bool isMine;
		private bool canUseWP=false;
		private bool canBuyWP = false;
        public Merchant(Region region) : base(region)
		{
			//itemsForSale = new Dictionary<Item, int>();
			itemsForSale = new List<ItemForSale>();
			initializeItems(); 
            if (region.getRank() == 71)
            {
				isMine = true;
            }
            else
            {
				isMine = false;
            }
		}

		// commented since it is simply List.Add
		/*
		public bool addItemForSale(ItemForSale itemForSale)
		{

				itemsForSale.Add(itemForSale);
	
		}
		*/

		// can not check the input item since it is an instance
		// use a newly created enum class ItemForSale
		/*
        public bool buyItemForSale(Hero hero, Item item)
		{
			if (hero != null && item != null && itemsForSale.ContainsKey(item))
			{
				int price = itemsForSale[item];

                if(hero.getGold() >= price)
				{
					itemsForSale.Remove(item);
					hero.setGold(hero.getGold() - price);
					hero.addItem(item);
					return true;
				}
			}

			return false;
		}
		*/
		public bool buyItemForSale(Hero hero, ItemForSale item)
		{
			if (hero != null && itemsForSale.Contains(item))
			{
				int price = 2;
				if (isMine && hero.getHeroKind() == HeroKind.Dwarf && item == ItemForSale.SP) price = 1; // dwarf get a discount
				if (hero.getGold() >= price)
				{
					Item newItem=new Item(ItemWeight.Light);        //ItemWeight.Light as placehoder for initialization
					if (item== ItemForSale.SP)
                    {
						hero.setGold(hero.getGold() - price);
						hero.setStrengthPoints(hero.getStrengthPoints() + 1);
						return true; 
                    }
                    else
                    {
						if (item == ItemForSale.Wineskin) newItem = new Wineskin(ItemWeight.Light);
						if (item == ItemForSale.Falcon) newItem = new Falcon(ItemWeight.Heavy);
						if (item == ItemForSale.Telescope) newItem = new Telescope(ItemWeight.Light);
						if (item == ItemForSale.Helm) newItem = new Helm(ItemWeight.Light);
						if (item == ItemForSale.Shield) newItem = new Shield(ItemWeight.Heavy);
						if (item == ItemForSale.Bow) newItem = new Bow(ItemWeight.Heavy);
					}
					hero.setGold(hero.getGold() - price);
					hero.addItem(newItem);
					return true;
				}
			}
			return false;
		}

		public List<ItemForSale> getItemsForSale()
		{
			return itemsForSale;
		}

		public void initializeItems()
        {
			itemsForSale.Add(ItemForSale.Wineskin);
			itemsForSale.Add(ItemForSale.Helm);
			itemsForSale.Add(ItemForSale.Falcon);
			itemsForSale.Add(ItemForSale.Bow);
			itemsForSale.Add(ItemForSale.Shield);
			itemsForSale.Add(ItemForSale.Telescope);
			itemsForSale.Add(ItemForSale.SP);
		}

		public void allowUseWP()
        {
			canUseWP = true;
        }

		public void allowBuyWP()
        {
			canBuyWP = true;
        }
	}
}

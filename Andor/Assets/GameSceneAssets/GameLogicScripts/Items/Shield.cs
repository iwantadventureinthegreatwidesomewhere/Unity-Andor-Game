using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
	public class Shield : UsableItem
	{
		private ItemDurability itemDurability;

        public Shield(ItemWeight itemWeight) : base(itemWeight)
		{
			itemDurability = ItemDurability.New;
		}

		public override void Use()
		{
            if(owner.InCombat)
            {
				owner.UsedShield = true;
            }
            else if(GameObject.FindObjectOfType<GameManager>().ActiveEvent != null)
            {
				owner.UsedShield = true;
            }
			if (itemDurability == ItemDurability.New)
			{
				itemDurability = ItemDurability.Used;
				owner.UpdateItemState(this);
			}
			else
			{
				owner.DiscardItem(this);
			}
		}

		public override bool Equals(object obj)
		{
			if (obj.GetType() == null)
				return false;
			if (obj.GetType() != this.GetType())
				return false;
			return ((Shield)obj).itemDurability == itemDurability;
		}

		//TODO: override hashcode
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public ItemDurability getItemDurability()
		{
			return itemDurability;
		}

        public void setItemDurability(ItemDurability itemDurability)
		{
			this.itemDurability = itemDurability;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
	public class WitchsBrew : UsableItem
	{
		private ItemFullness itemFullness;

		public WitchsBrew(ItemWeight itemWeight) : base(itemWeight)
		{
			itemFullness = ItemFullness.Full;
		}

		public override void Use()
		{
			if(owner.IsCarrying(new Helm(ItemWeight.Light)) || UsedThisTurn)
            {
				return;
            }
			owner.UsedBrew = true;
			if (itemFullness == ItemFullness.Full)
			{
				itemFullness = ItemFullness.Half;
				UsedThisTurn = true;
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
			return ((WitchsBrew)obj).itemFullness == itemFullness;
		}

        //TODO: override hashcode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public ItemFullness getItemFullness()
        {
			return itemFullness;
        }
    }
}
